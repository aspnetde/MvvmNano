using Demo.ViewModels;
using MvvmNano;
using MvvmNano.Forms;
using Xamarin.Forms;

namespace Demo.Pages
{
    public class TabTwoPage : MvvmNanoContentPage<TabTwoViewModel>
    {
        public TabTwoPage()
        {
            SetViewModel(MvvmNanoIoC.Resolve<TabTwoViewModel>());
            Title = "Tab Two";
            Content = new Label
            {
                Text = "I'm the second tab child.",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
        }
    }
}