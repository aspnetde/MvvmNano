using Demo.Data;
using Demo.ViewModels;
using MvvmNano;
using MvvmNano.Ninject;

namespace Demo
{
    public partial class App 
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            base.OnStart();

            SetUpDependencies();

            //Chose which SetUpMainPage to use to show the different main page options supported by MvvmNano. 
            //Dont forget to comment out the other SetUpMainPage calls.

            //Content page
            //SetUpMainPage<LoginViewModel>();

            //Tabbed page
            SetUpMainPage<TabbedViewModel>();

            //Master page
            //SetUpMainPage<MasterViewModel>();
        }

        private static void SetUpDependencies()
        {
            MvvmNanoIoC.Register<IClubRepository, MockClubRepository>();
            MvvmNanoIoC.RegisterAsSingleton<IUserData, UserData>();
        }

        protected override void SetUpPresenter()
        {
            MvvmNanoIoC.RegisterAsSingleton<IPresenter>(new DemoPresenter(this));
        }

        protected override IMvvmNanoIoCAdapter GetIoCAdapter()
        {
            return new MvvmNanoNinjectAdapter();
        }
    }
}
