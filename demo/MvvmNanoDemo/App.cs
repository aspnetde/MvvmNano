using MvvmNano.Forms;
using MvvmNano;
using MvvmNano.Ninject;

namespace MvvmNanoDemo
{
    public class App : MvvmNanoApplication
    {
        protected override void OnStart()
        {
            base.OnStart();

            SetUpDependencies();

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

        protected override IMvvmNanoIoCAdapter SetUpIoCAdapter()
        {
            return new MvvmNanoNinjectAdapter();
        }
    }
}

