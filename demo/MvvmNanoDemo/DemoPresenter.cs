using MvvmNano.Forms;
using Xamarin.Forms;

namespace MvvmNanoDemo
{
    public class DemoPresenter : MvvmNanoFormsPresenter
    {
        public DemoPresenter(Application app) : base(app)
        {
        }

        protected override void OpenPage(Page page)
        {
            if (page is AboutPage)
            {
                Device.BeginInvokeOnMainThread(async () => 
                    await CurrentPage.Navigation.PushModalAsync(new MvvmNanoNavigationPage(page)
                ));

                return;
            }

            if (page is WelcomePage)
            {
                Device.BeginInvokeOnMainThread(async () => 
                {
                    Application.MainPage = new MvvmNanoNavigationPage(page);
                    await CurrentPage.Navigation.PopToRootAsync(false);
                });

                return;
            }

            base.OpenPage(page);
        }
    }
}

