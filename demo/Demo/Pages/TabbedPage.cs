using Demo.ViewModels;
using MvvmNano;
using MvvmNano.Forms;
using Xamarin.Forms;

namespace Demo.Pages
{
    public class TabbedPage : MvvmNanoTabbedPage<MvvmNanoViewModel>
    {
        public TabbedPage()
        {
            AddChild<FirstViewModel, FirstPage>();
            AddChild<SecondViewModel, SecondPage>();
            AddChild<ThirdViewModel, ThirdPage>();
            CurrentPage = Children[0];
            Title = "Tabbed Page";
        }

        private void AddChild<TViewModel, TPage>()
            where TViewModel : MvvmNanoViewModel
            where TPage : Page, IView , new()
        {
            var page = new TPage();
            page.SetViewModel(MvvmNanoIoC.Resolve<TViewModel>());
            Children.Add(page);
        }
    }
}