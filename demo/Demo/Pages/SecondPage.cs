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

            Content = new Label
            {
                Text = "I'm the second page.",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
        }
    }
}