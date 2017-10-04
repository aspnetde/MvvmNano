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

            Content = new Label
            {
                Text = "I'm the first page.",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
        }
    }
}