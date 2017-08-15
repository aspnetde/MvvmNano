using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MvvmNano.Forms
{
    /// <summary>
    /// The entry point of your XF application
    /// </summary>
    public abstract class MvvmNanoApplication : Application
    { 
        protected override void OnStart()
        {
            base.OnStart();

            SetUpIoC();
            SetUpPresenter();
            SetUpMessenger(); 
        } 

        /// <summary>
        /// Provide the right IoC Container implementation, for example
        /// from package MvvmNano.Ninject (default)
        /// </summary>
        protected abstract IMvvmNanoIoCAdapter GetIoCAdapter();

        /// <summary>
        /// Calls MvvmNanoIoC.SetUp() and passes the result of SetUpIoCAdapter();
        /// </summary>
        protected virtual void SetUpIoC()
        {
            MvvmNanoIoC.SetUp(GetIoCAdapter());
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
            SetMainPage(GetPageFor<TViewModel>());
        }

        /// <summary>
        /// Sets up the main page for the given View Model type and parameter.
        /// </summary>
        protected void SetUpMainPage<TViewModel, TNavigationParameter>(TNavigationParameter navigationParameter) where TViewModel : MvvmNanoViewModel<TNavigationParameter>
        {
            SetMainPage(GetPageFor<TViewModel, TNavigationParameter>(navigationParameter));
        }

        private void SetMainPage(Page page)
        {
            if (page is MasterDetailPage)
                MainPage = page;
            else
                MainPage = new MvvmNanoNavigationPage(page); 
        }

        /// <summary>
        /// Creates a MvvmNanoContentPage for the given View Model type.
        /// </summary>
        private Page GetPageFor<TViewModel>() where TViewModel : MvvmNanoViewModel
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>();
            viewModel.Initialize();
            return ResolvePage(viewModel);
        }

        /// <summary>
        /// Creates a MvvmNanoContentPage for the given View Model type and parameter
        /// </summary>
        private Page GetPageFor<TViewModel, TNavigationParameter>(TNavigationParameter navigationParameter) where TViewModel : MvvmNanoViewModel<TNavigationParameter>
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>();
            viewModel.Initialize(navigationParameter); 
            return ResolvePage(viewModel);
        }

        /// <summary>
        /// Resolves the page that is associated with the given ViewModel.
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private Page ResolvePage<TViewModel>(TViewModel viewModel) where TViewModel : IViewModel
        {
            var view = MvvmNanoIoC.Resolve<IPresenter>().CreateViewFor<TViewModel>();

            view.SetViewModel(viewModel);

            return view as Page;
        } 
    }
}

