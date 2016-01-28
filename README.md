[![Build status](https://ci.appveyor.com/api/projects/status/7ts0fqo0vp8fb718?svg=true)](https://ci.appveyor.com/project/ThomasBandt/mvvm-nano)

# MvvmNano
The small and smart MVVM framework made especially for Xamarin.Forms.

## Why?

Because I found all commonly used MVVM frameworks in the Xamarin space had their flaws and there must be a better, slimmer way to do that.

> To be exact: I really like [MvvmCross](https://github.com/MvvmCross/MvvmCross) for Xamarin.iOS and Xamarin.Android, but I think it's a bit too heavy-weight for Xamarin.Forms, which brings its own data-binding for example. [MVVM Light](https://mvvmlight.codeplex.com/)'s lack of a central documentation was also a show-stopper to me. And [FreshMvvm](https://github.com/rid00z/FreshMvvm) brings some concepts I strongly disagree with.

## What?

So, yes, this is a very opinionated framework. Take a look and decide for yourself, if it meets your requirements. This is in for you:

- Each Page has it's own View Model. They are tied by a **naming convention**: SettingsPage <-> SettingsViewModel.
- Each Page knows it's View Model, but no View Model knows its page.
- Therefore **navigation works from View Model to View Model** without involving the Page (aka View).
- You can pass whatever complex object you want as a parameter to the View Model you're navigating to you want, which is of course optional.
- Want to show some pages modally or render them in a completely different way? There's a central Presenter which enables you to **decide per View Model how to present it**.
- There is **Dependency Injection baked in**. Just register your dependencies and they are injected in your View Model's constructor. So each View Model becomes easily testable.
- Whether you set up your Pages in **code or XAML, both are supported**.
- **Both Pages and View Models are disposable** and the framework makes sure they actually are disposed whenever a Page is being popped (or dismissed). This gives you free access to clean up expensive resources or strong references.
