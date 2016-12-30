using MvvmNano.Forms;
using MvvmNano;

namespace MvvmNanoDemo
{
    public class App : MvvmNanoApplication
    {
        protected override void OnStart()
        {
            base.OnStart();

            SetUpDependencies();

            AddSiteToDetailPages<WelcomeViewModel>(new MasterDetailData("Welcome"));
            AddSiteToDetailPages<AboutViewModel>(new MasterDetailData("About"));

            SetUpMainPage<LoginViewModel>();
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

