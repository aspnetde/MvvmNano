using Demo.ViewModels;
using MvvmNano.Forms;
using Xamarin.Forms;

namespace Demo.Pages
{
    public class SecondPage : MvvmNanoContentPage<SecondViewModel>
    {
        public SecondPage()
        { 
            Title = "Page two";

            var priorButton = new Button
            {
                Text = "Go to first page." 
            };

            BindToViewModel(priorButton, Button.CommandProperty, x => x.ToPreviousPageCommand);

            var nextButton = new Button()
            {
                Text = "Go to third page." 
            };

            BindToViewModel(nextButton, Button.CommandProperty, x => x.ToNextPageCommand);

            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "I'm the second page.",
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.Center
                    },
                    priorButton,
                    nextButton
                }
            };
        }
    }
}