using MvvmNano.Forms;
using MvvmNano;
using MvvmNano.Ninject;

namespace MvvmNanoDemo
{
    public class App : MvvmNanoMasterDetailApplication
    {
        protected override void OnStart()
        {
            base.OnStart();

            SetUpDependencies();

            AddPageToDetailPages<WelcomeViewModel>(new MasterDetailData("Welcome"));
            AddPageToDetailPages<AboutViewModel>(new MasterDetailData("About"));

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

            MvvmNanoIoC.RegisterAsSingleton<IUserData>(
                new UserData()
            );
        }

        protected override IMvvmNanoIoCAdapter SetUpIoCAdapter()
        {
            return new MvvmNanoNinjectAdapter();
        }
    }
}

