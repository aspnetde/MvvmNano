using MvvmNano.Forms;
using MvvmNano;

namespace MvvmNanoDemo
{
    public class App : MvvmNanoApplication
    {
        protected override void OnStart()
        {
            base.OnStart();

            SetUpMainPage();
            SetUpDependencies();
        }

        private void SetUpMainPage()
        {
            var viewModel = MvvmNanoIoC.Resolve<LoginViewModel>();
            viewModel.Initialize();

            var page = new LoginPage();
            page.SetViewModel(viewModel);

            MainPage = new MvvmNanoNavigationPage(page);
        }

        private static void SetUpDependencies()
        {
            MvvmNanoIoC.Register<IClubRepository, MockClubRepository>();
        }

        protected override void SetUpPresenter()
        {
            MvvmNanoIoC.RegisterAsSingleton<IPresenter>(
                new DemoPresenter(this)
            );
        }
    }
}

