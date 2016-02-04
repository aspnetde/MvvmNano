
| Build Status  | NuGet Package |
| ------------- | ------------- |
| [![Build status](https://ci.appveyor.com/api/projects/status/7ts0fqo0vp8fb718?svg=true)](https://ci.appveyor.com/project/ThomasBandt/mvvm-nano)  | [![NuGet version](https://badge.fury.io/nu/MvvmNano.Forms.svg)](https://badge.fury.io/nu/MvvmNano.Forms)  |


# MvvmNano
The small and smart MVVM framework made with ❤ for Xamarin.Forms. Easy to learn, easy to use and easy to extend.

## How it works

- Each Page has it's own View Model. They are tied by a **naming convention**: SettingsPage <-> SettingsViewModel.
- Each Page knows it's View Model, but no View Model knows its Page.
- Therefore **navigation works from View Model to View Model** without involving the Page (aka View).
- You can pass whatever complex object you want as a parameter to the View Model you're navigating to.
- Want to show some pages modally or render them in a completely different way? There's a central Presenter which enables you to **decide per View Model how to present it**.
- There is **Dependency Injection baked in**. Just register your dependencies and they are injected in your View Model's constructor. So each View Model becomes **easily testable**.
- Whether you set up your Pages in **code or XAML, both are supported**.
- **Both Pages and View Models are disposable** and the framework makes sure they actually are disposed whenever a Page is being popped (or dismissed). This makes [house-keeping](https://thomasbandt.com/xamarinios-memory-pitfalls) a no-brainer.

## Great. Where can I download it?

Just fetch it [from NuGet](https://www.nuget.org/packages/MvvmNano.Forms/):

    Install-Package MvvmNano.Forms
