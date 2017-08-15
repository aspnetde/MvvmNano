using Demo.ViewModels;
using MvvmNano;
using MvvmNano.Forms;
using Xamarin.Forms;

namespace Demo.Pages
{
    public class TabOnePage : MvvmNanoContentPage<TabOneViewModel>
    {
        public TabOnePage()
        {
            SetViewModel(MvvmNanoIoC.Resolve<TabOneViewModel>());
            Title = "Tab One";
            Content = new Label
            {
                Text = "I'm the first tab child.",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
        }
    }
}