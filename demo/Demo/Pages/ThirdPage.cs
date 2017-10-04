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

            Content = new Label
            {
                Text = "I'm the third page.",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
        }
    }
}