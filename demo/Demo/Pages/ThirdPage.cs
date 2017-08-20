using Demo.ViewModels;
using MvvmNano.Forms;
using Xamarin.Forms;

namespace Demo.Pages
{
    public class ThirdPage : MvvmNanoContentPage<ThirdViewModel>
    {
        public ThirdPage()
        {  
            Title = "Page three";

            var priorButton = new Button
            {
                Text = "Go to second page." 
            };

            BindToViewModel(priorButton, Button.CommandProperty, x => x.ToPreviousPageCommand);

            var nextButton = new Button()
            {
                Text = "Go to first page." 
            };

            BindToViewModel(nextButton, Button.CommandProperty, x => x.ToNextPageCommand);

            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "I'm the third page.",
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