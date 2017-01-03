# MvvmNano
The small and smart MVVM framework made with ❤ for Xamarin.Forms.

| Build Status  | NuGet Package |
| ------------- | ------------- |
| [![Build status](https://ci.appveyor.com/api/projects/status/7ts0fqo0vp8fb718?svg=true)](https://ci.appveyor.com/project/ThomasBandt/mvvm-nano)  | [![NuGet version](https://badge.fury.io/nu/MvvmNano.Forms.svg)](https://badge.fury.io/nu/MvvmNano.Forms)  |

1. [Manifesto](#manifesto)
2. [Download](#download)
3. [Demo](#demo)
4. [Getting started](#getting-started)
5. [Data Binding](#data-binding)
6. [Navigation](#navigation)
7. [Dependency Injection](#di)
8. [Messaging](#messaging)
9. [Cleaning up](#cu)
10. [XAML Support](#xaml-support)

<div id='manifesto'/>
## Manifesto

1. Each View (aka Page) must have its own View Model.
2. Views know their View Models, but not vice versa: View Models never know their Views.
3. Therefore navigation works from View Model to View Model only, without involving the View.
4. When navigating, passing complex objects along must be possible.
5. There should be no limits in how to present Views.
6. View Models must be easily testable, so Dependency Injection is a basic prerequisite.
7. Both Views and View Models must be easy to [clean up](https://thomasbandt.com/xamarinios-memory-pitfalls).

<div id='download'/>
## Download

    Install-Package MvvmNano.Forms

<div id='demo'/>
## Demo

Just download this repo and take a look at the demo app which can be found within the /demo folder. Note: It's not using the NuGet packages.

<div id='getting-started'/>
## Getting started

### Preliminary remarks

- MvvmNano comes as three Portable Class Libraries (PCL) with profile 78 (MvvmNano.Core, MvvmNano.Ninject, and MvvmNano.Forms)
- MvvmNano.Forms references [Xamarin.Forms](https://www.nuget.org/packages/Xamarin.Forms/)
- MvvmNano.Ninject references [Portable.Ninject](https://www.nuget.org/packages/Portable.Ninject/)
- MvvmNano.Core does not have and external dependency

### Add the NuGet package

You can add MvvmNano easily via [NuGet](https://www.nuget.org/packages/MvvmNano.Forms/):

    Install-Package MvvmNano.Forms

> **Important:** Add it to your Xamarin.Forms library as well as to your native app projects, so NuGet can resolve the right assemblies of the dependencies Xamarin.Forms and Portable.Ninject on each target (for example PCL, Xamarin.iOS, Xamarin.Android).

If you want to use our default IoC container (Ninject Portable), also add MvvmNano.Ninject:
    
    Install-Package MvvmNano.Ninject

### Add your first View Model and its Page

Your View Model needs to inherit from `MvvmNanoViewModel<TNavigationParameter>` or `MvvmNanoViewModel`. Let's start with the latter and thereby without a parameter.

```cs
public class LoginViewModel : MvvmNanoViewModel
{
    // ...
}
```

Now add the Page. Note that by convention it needs to be named after your View Model, except for the ViewModel suffix (so `LoginViewModel` becomes `LoginPage`). You also need to inherit from `MvvmNanoContentPage<TViewModel>`.

```cs
public class LoginPage : MvvmNanoContentPage<LoginViewModel>
{
    // ...
}
```

### Set up your App class

Each Xamarin.Forms app has an entry point – a class called `App` which is derived from `Application`. Change that base class to `MvvmNanoApplication`.

Next you are asked to implement the method `GetIoCAdapter()` which is expected to return an implementation of `IMvvmNanoIoCAdapter`. Just go with our default choice (MvvmNano.Ninject, which uses [Portable.Ninject](https://www.nuget.org/packages/Portable.Ninject/)), or go [with your own](http://www.palmmedia.de/blog/2011/8/30/ioc-container-benchmark-performance-comparison).

You also want to tell your application the first Page and View Model which should be used when the app gets started for the first time. Put this setup inside of `OnStart()`, but don't forget to call `base.OnStart()`. This is important in order to set up the Presenter correctly (for more on that see below).

```cs
public class App : MvvmNanoApplication
{
    protected override void OnStart()
    {
        base.OnStart();

		SetUpMainPage<LoginViewModel>();
    }
    
    protected override IMvvmNanoIoCAdapter GetIoCAdapter()
	{
	    return new MvvmNanoNinjectAdapter();
	}
}
```

### That's it!

If you now build and run your app(s), you'll see your first Page which is running with it's View Model behind. Nothing spectacular so far, but the fun is just getting started.

<div id='data-binding'/>
## Data Binding

Xamarin.Forms comes with really powerful data binding features which you're fully able to leverage with MvvmNano, so we are not reinventing the wheel here.

### NotifyPropertyChanged()

MvvmNano View Models implement `INotifyPropertyChanged` and offer a small helper method called `NotifyPropertyChanged()` (without the leading I).

```cs
private string _username;
public string Username
{
    get { return _username; }
    set
    {
        _username = value;
        NotifyPropertyChanged();
        NotifyPropertyChanged("IsFormValid");
    }
}
```

As you can see, `NotifyPropertyChanged()` can be called with and without the name of the property it should be notifying about. If you leave it out, it will automatically use the name of the property you're calling it from.

(Scared from so much boilerplate code? Take a look at [Fody PropertyChanged](https://github.com/Fody/PropertyChanged).)

### BindToViewModel()

This is a small helper method baked in to `MvvmNanoContentPage`, which makes binding to your View Model a no-brainer when writing your views (pages) in code:

```cs
var nameEntry = new Entry
{
    Placeholder = "Your name"
};

BindToViewModel(nameEntry, Entry.TextProperty, x => x.Username);
```

### Commands

Xamarin.Forms supports `ICommand`, and so does MvvmNano.

View Model:

```cs
public MvvmNanoCommand LogInCommand
{
	get { return new MvvmNanoCommand(LogIn); }
}

private void LogIn()
{
	// ...
}
```
Page:

```cs
BindToViewModel(loginButton, Button.CommandProperty, x => x.LogInCommand);
```
### Commands with parameters

View Model:

```cs
public MvvmNanoCommand<string> LogInCommand
{
    get { return new MvvmNanoCommand<string>(LogIn); }
}

private void LogIn(string userName)
{
	// ...
}
```
Page:

```cs
BindToViewModel(loginButton, Button.CommandProperty, x => x.LogInCommand);
BindToViewModel(loginButton, Button.CommandParameterProperty, x => x.Username);
```

<div id='navigation'/>
## Navigation

Navigation works from View Model to View Model only, not involving the View aka Page directly. Instead all work is delegated to a central _Presenter_, which is responsible for creating the Page, its View Model and also passing a parameter, if specified.

> This way you can keep your application independent from the UI implementation – if you ever have to switch to Xamarin.iOS or Xamarin.Android, in parts or even completely, you don't have to throw your View Models away.

### Navigation without parameter

```cs
NavigateTo<AboutViewModel>();
```

Navigates to `AboutViewModel` without passing a parameter.

### Navigation with a parameter

Let's say you want to get a parameter of the type `Club` each time your View Model is being called. Then you have to derive from `MvvmNanoViewModel<TViewModel>` and make `TViewModel` `Club`.

```cs
public class ClubViewModel : MvvmNanoViewModel<Club>
{
    public override void Initialize(Club parameter)
    {
        // ...
    }
}
```

Overriding the `Initialize()` method will now make that `Club` being passed available after the View Model is being created.

To actually pass that parameter, navigate to your `ClubViewModel` from the calling View Model as follows:

```cs
NavigateTo<ClubViewModel, Club>(club);
```

### Opening Pages modally or in a completely customized fashion

The default presenter coming with MvvmNano will push a page to the existing navigation stack. But you are completely free to customize that, so you can define on a per-View Model basis how its view should be presented (maybe displayed modally or rendered in a completely different way).

A custom presenter could look like this:

```cs
public class DemoPresenter : MvvmNanoFormsPresenter
{
    public DemoPresenter(Application app) : base(app)
    {
    }

    protected override void OpenPage(Page page)
    {
        if (page is AboutPage)
        {
            Device.BeginInvokeOnMainThread(async () =>
                await CurrentPage.Navigation.PushModalAsync(new MvvmNanoNavigationPage(page)
            ));

            return;
        }

        base.OpenPage(page);
    }
}
```

In order to pass every navigation request through it, you have register it within your `App` class:

```cs
protected override void SetUpPresenter()
{
    MvvmNanoIoC.RegisterAsSingleton<IPresenter>(
        new DemoPresenter(this)
    );
}
```

<div id='di'/>
## Dependency Injection

Having a `Initialize()` or `Initialize(TNavigationParameter parameter)` method in your View Model comes with a benefit: the constructor is still free for parameters being automatically injected.

We're not inventing the wheel here neither, because the portable version of [Ninject](http://www.ninject.org/) does a fabolous job for us behind the scenes.

In front of it there is a small static helper class called `MvvmNanoIoC`, which provides the following methods for registering dependencies:

- `MvvmNanoIoC.Register<TInterface, TImplementation>()`
- `MvvmNanoIoC.RegisterAsSingleton<TInterface, TImplementation>()`
- `MvvmNanoIoC.RegisterAsSingleton<TInterface>(TInterface instance)`
- `MvvmNanoIoC.Resolve<TInterface>()`

### Sample: Registering a dependency

```cs
public class App : MvvmNanoApplication
{
    protected override void OnStart()
    {
        base.OnStart();

        SetUpDependencies();
    }

    private static void SetUpDependencies()
    {
        MvvmNanoIoC.Register<IClubRepository, MockClubRepository>();
    }
}
```

### Sample: Constructor Injection

```cs
public class WelcomeViewModel : MvvmNanoViewModel
{
    public List<Club> Clubs { get; private set; }

    public WelcomeViewModel(IClubRepository clubs)
    {
        Clubs = clubs.All();
    }
}
```

PS: Usually you won't need the `Resolve<TInterface>()` method, because constructor injection works out of the box.

### Using another IoC Container than Ninject

If you want to use another IoC Container, just implement `IMvvmNanoIoCAdapter` and return an instance of this implementation in your App's class `GetIoCAdapter()` method.

<div id='messaging'/>
## Messaging

This is very opinionated and certainly optional, but the official interface for messaging within Xamarin.Forms seems a bit odd. See more about it [here](https://thomasbandt.com/a-nicer-messaging-interface-for-xamarinforms).

The solution of `IMessenger` presented in that blog post comes with MvvmNano and is automatically registered in `MvvmNanoApplication`.

### Inject IPresenter to your View Model

```cs
public class MyViewModel : MvvmNanoViewModel
{
    private readonly IMessenger _messenger;

    public WelcomeViewModel(IMessenger messenger)
    {
        _messenger = messenger;
    }
}
```

### Define your message

```cs
public class AlbumCreatedMessage : IMessage
{
    public readonly Album Album;

    public AlbumCreatedMessage(Album album)
    {
        Album = album;
    }
}
```

### Send it around

```cs
var album = new Album
{
    Id = Guid.NewGuid(),
    Title = "Hello World"
};

_messenger.Send(new AlbumCreatedMessage(album));
```

### Subscribe to it

```cs
_messenger.Subscribe<AlbumCreatedMessage>(this, AlbumAdded);

private void AlbumAdded(object sender, AlbumCreatedMessage message)
{
    // Do something
}
```

### When you're done, unsubscribe

```cs
_messenger.Unsubscribe<AlbumCreatedMessage>(this);
```

<div id='cu'/>
## Cleaning up

Cleaning up your View Models _and_ your Views aka Pages is a must in order to prevent memory leaks. Read more about it [here](https://thomasbandt.com/xamarinios-memory-pitfalls). Unfortunately Xamarin doesn' think that way, so their whole Xamarin.Forms framework lacks `IDisposable` implementations.

MvvmNano fixes that. Both `MvvmNanoViewModel` and `MvvmNanoContentPage` implement `IDisposable`, so you can use the `Dispose()` method in both to detach event handlers, dispose "heavy resources" such as images etc.

> **Important:** In order to get that `Dispose()` method actually called, you must use `MvvmNanoNavigationPage` instead of the framework's default Navigationpage. It takes care of calling `Dispose()` at the right time whenever a Page is being removed from the stack.

<div id='xaml-support'/>
## XAML Support

XAML is fully supported, take a look at the demo or these snippets.

View Model:

```cs
public class ClubViewModel : MvvmNanoViewModel<Club>
{
    private string _name;
    public string Name
    {
        get { return _name; }
        private set { _name = value; NotifyPropertyChanged(); }
    }

    private string _country;
    public string Country
    {
        get { return _country; }
        private set { _country = value; NotifyPropertyChanged(); }
    }

    public override void Initialize(Club parameter)
    {
        Name = parameter.Name;
        Country = parameter.Country;
    }
}
```

Page:

```xaml
<?xml version="1.0" encoding="UTF-8"?>
<pages:MvvmNanoContentPage xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:pages="clr-namespace:MvvmNano.Forms"
    xmlns:vm="clr-namespace:MvvmNanoDemo"
    x:Class="MvvmNanoDemo.ClubPage"
    x:TypeArguments="vm:ClubViewModel"
    Title="{Binding Name}">
    <ContentPage.Content>
        <StackLayout>
            <Label Text="{Binding Country}" />
        </StackLayout>
    </ContentPage.Content>
</pages:MvvmNanoContentPage>
```
