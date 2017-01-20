using MvvmNano.Forms;
using MvvmNanoDemo.ViewModels;
using Xamarin.Forms;

namespace MvvmNanoDemo.Pages
{
    public class LoginPage : MvvmNanoContentPage<LoginViewModel>
    {
        public LoginPage()
        {
            Title = "Login";
            Padding = 60;

            var nameEntry = new Entry
            {
                Placeholder = "Your name"
            };

            var passwordEntry = new Entry
            {
                Placeholder = "Your password",
                IsPassword = true
            };

            var loginButton = new Button
            {
                Text = "Log in"
            };

            var aboutButton = new Button
            {
                Text = "About this App"
            };

            BindToViewModel(nameEntry, Entry.TextProperty, x => x.Username);
            BindToViewModel(passwordEntry, Entry.TextProperty, x => x.Password);

            BindToViewModel(loginButton, Button.CommandProperty, x => x.LogInCommand);
            BindToViewModel(loginButton, VisualElement.IsEnabledProperty, x => x.IsFormValid);

            BindToViewModel(aboutButton, Button.CommandProperty, x => x.ShowAboutCommand);

            Content = new StackLayout
            {
                Children = 
                {
                    new StackLayout
                    {
                        Children = 
                        {
                            nameEntry,
                            passwordEntry,
                            loginButton,
                            aboutButton
                        }
                    }
                }
            };
        }
    }
}

