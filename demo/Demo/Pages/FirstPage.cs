using Demo.ViewModels;
using MvvmNano.Forms;
using Xamarin.Forms;

namespace Demo.Pages
{
    public class FirstPage : MvvmNanoContentPage<FirstViewModel>
    {
        public FirstPage()
        { 
            Title = "Page one";

            var priorButton = new Button
            {
                Text = "Go to third page."  
            };

            BindToViewModel(priorButton, Button.CommandProperty, x => x.ToPreviousPageCommand);

            var nextButton = new Button()
            {
                Text = "Go to second page." 
            };

            BindToViewModel(nextButton, Button.CommandProperty, x => x.ToNextPageCommand);

            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "I'm the first page.",
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