using Xamarin.Forms;

namespace MvvmNano.Forms
{
    /// <summary>
    /// The entry point of your XF application
    /// </summary>
    public class MvvmNanoApplication : Application
    {
        protected override void OnStart()
        {
            base.OnStart();

            SetUpPresenter();
            SetUpMessenger();
        }

        /// <summary>
        /// Registers MvvmNanoFormsPresenter. If you're using your own
        /// custom presenter, override this method for registration (but
        /// don't call base.SetUpPresenter()!).
        /// </summary>
        protected virtual void SetUpPresenter()
        {
            MvvmNanoIoC.RegisterAsSingleton<IPresenter>(new MvvmNanoFormsPresenter(this));
        }

        /// <summary>
        /// Registers MvvmNanoFormsMessenger. If you're using your own
        /// custom messenger, override this method for registration (but
        /// don't call base.SetUpMessenger()!).
        /// </summary>
        protected virtual void SetUpMessenger()
        {
            MvvmNanoIoC.RegisterAsSingleton<IMessenger, MvvmNanoFormsMessenger>();
        }

        /// <summary>
        /// Sets up the main page for the given View Model type.
        /// </summary>
        protected void SetUpMainPage<TViewModel>() where TViewModel : MvvmNanoViewModel
        {        
            MainPage = new MvvmNanoNavigationPage(GetPageFor<TViewModel>());
        }

        /// <summary>
        /// Creates a MvvmNanoContentPage for the given View Model type.
        /// </summary>
        public MvvmNanoContentPage<TViewModel> GetPageFor<TViewModel>() where TViewModel : MvvmNanoViewModel
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>();
            viewModel.Initialize();

            var page = MvvmNanoIoC
                .Resolve<IPresenter>()
                .CreateViewFor<TViewModel>() as MvvmNanoContentPage<TViewModel>;

            if (page == null)
            {
                throw new MvvmNanoException($"Could not create a MvvmNanoContentPage for View Model of type {typeof(TViewModel)}.");
            }

            page.SetViewModel(viewModel);

            return page;
        }

        /// <summary>
        /// reates a MvvmNanoContentPage for the given View Model type and parameter
        /// </summary>
        public MvvmNanoContentPage<TViewModel> GetPageFor<TViewModel, TNavigationParameter>(TNavigationParameter navigationParameter) where TViewModel : IViewModel<TNavigationParameter>
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>() as IViewModel<TNavigationParameter>;
            viewModel.Initialize(navigationParameter);

            var page = MvvmNanoIoC
                .Resolve<IPresenter>()
                .CreateViewFor<TViewModel>() as MvvmNanoContentPage<TViewModel>;

            if (page == null)
            {
                throw new MvvmNanoException($"Could not create a MvvmNanoContentPage for View Model of type {typeof(TViewModel)}.");
            }

            page.SetViewModel(viewModel);

            return page;
        }
    }
}

