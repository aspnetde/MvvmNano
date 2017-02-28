using MvvmNano.Forms;
using MvvmNano;
using MvvmNano.Ninject;
using MvvmNanoDemo.Data;
using MvvmNanoDemo.ViewModels;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MvvmNanoDemo
{
    public class App : MvvmNanoMasterDetailApplication
    {
        protected override void OnStart()
        {
            base.OnStart();

            SetUpDependencies();

            AddPageToDetailPages<WelcomeViewModel>(new MvvmNanoMasterDetailData("Welcome"));
            AddPageToDetailPages<SecondDetailViewModel>(new MvvmNanoMasterDetailData("Second Detail"));

            ((MvvmNanoApplication)this).SetUpMainPage<LoginViewModel>();
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

        protected override IMvvmNanoIoCAdapter GetIoCAdapter()
        {
            return new MvvmNanoNinjectAdapter();
        }
    }
}
