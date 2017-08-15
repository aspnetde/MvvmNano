using System.Threading.Tasks;
using Demo.Pages;
using Demo.ViewModels;
using MvvmNano.Forms;
using Xamarin.Forms;

namespace Demo
{
    public class DemoPresenter : MvvmNanoFormsPresenter
    {
        public DemoPresenter(MvvmNanoApplication app) : base(app)
        {
        }

        protected override void OpenPage(Page page)
        {
            if (page is LoginPage)
            {
                Application.MainPage = new MvvmNanoNavigationPage(page);
            }

            if (page is MasterPage)
            {
                Application.MainPage = page;
                NavigateToViewModel<WelcomeViewModel>();
            }
        }

        protected override Task OpenPageAsync(Page page)
        { 
            if (page is AboutPage)
            {
                return CurrentPage.Navigation.PushModalAsync(new MvvmNanoNavigationPage(page));
            }

            return base.OpenPageAsync(page);
        }
    }
}