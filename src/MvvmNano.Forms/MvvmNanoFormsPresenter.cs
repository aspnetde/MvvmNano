 
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MvvmNano.Forms
{
    /// <summary>
    /// The default Presenter implementation for Xamarin.Forms. Pushes each
    /// new Page to the navigation stack. Derive from this class to implement
    /// custom navigation for your View Models (and Pages).
    /// </summary>
    public class MvvmNanoFormsPresenter : IPresenter
    {
        private const string VIEW_MODEL_SUFFIX = "ViewModel";
        private const string VIEW_SUFFIX = "Page";

        private Type[] _availableViewTypes;

        /// <summary>
        /// A read-only reference to our Xamarin.Forms Application instance
        /// </summary>
        protected MvvmNanoApplication Application { get; private set; }

        /// <summary>
        /// Provides the Current Page, which is shown on top of all other
        /// (either modally, on the navigation stack, or just the app's 
        /// main page).
        /// </summary>
        public Page CurrentPage
        {
            get
            {
                Func<Page> getCurrentPage = () =>
                {
                    Page modalPage = Application.MainPage.Navigation
                        .ModalStack
                        .LastOrDefault();

                    if (modalPage != null)
                        return modalPage;

                    Page contentPage = Application.MainPage.Navigation
                        .NavigationStack
                        .LastOrDefault();

                    return contentPage ?? Application.MainPage;
                };

                Page currentPage = getCurrentPage();
                var tabbedPage = currentPage as TabbedPage;
                NanoMasterDetailPage masterDetailPage = currentPage as NanoMasterDetailPage;
                if (masterDetailPage != null)
                    return masterDetailPage.Detail.Navigation.NavigationStack.LastOrDefault();

                return tabbedPage != null
                    ? tabbedPage.CurrentPage
                        : currentPage;
            }
        }

        /// <summary>
        /// Initializes a new instance of MvvmNanoFormsPresenter.
        /// </summary>
        /// <param name="application">Our current Xamarin.Forms application. 
        /// Can't be null as it is needed to present our Pages.</param>
        public MvvmNanoFormsPresenter(MvvmNanoApplication application)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            SetApplication(application);
        }

        /// <summary>
        /// Replaces the application instance which we're using for navigation
        /// </summary>
        public void SetApplication(MvvmNanoApplication application)
        {
            Application = application;

            _availableViewTypes = Application
                .GetType()
                .GetTypeInfo()
                .Assembly
                .DefinedTypes
                .Select(t => t.AsType())
                .ToArray();
        }

        public void ChangeRootViewModel<TViewModel>() where TViewModel : MvvmNanoViewModel
        {
            var viewName = GetViewNameByViewModel(typeof(TViewModel));
            Type viewType = GetViewTypeByName(viewName); 
            if(viewType.GetTypeInfo().IsSubclassOf(typeof(NanoMasterDetailPage)))
                Application.SetUpMasterDetailPage<TViewModel>();    
            else
                Application.SetUpMainPage<TViewModel>();
        } 

        public string GetViewNameByViewModel(Type viewModelType)
        {
            return viewModelType.Name.Replace(VIEW_MODEL_SUFFIX, VIEW_SUFFIX);
        }
         
        public void SetPageRoot<TViewModel>() where TViewModel : MvvmNanoViewModel
        {
            Application.SetUpMainPage<TViewModel>();
        }
         
        public void SetMasterDetailPageRoot<TViewModel>() where TViewModel : MvvmNanoViewModel
        {
            Application.SetUpMasterDetailPage<TViewModel>();
        }

        /// <summary>
        /// Navigates to a Page and automatically creates a new instance of
        /// the corresponding View Model. Also passes some parameters of the
        /// given type.
        /// </summary>
        public void NavigateToViewModel<TViewModel, TNavigationParameter>(TNavigationParameter parameter)
        {
            IViewModel<TNavigationParameter> viewModel = CreateViewModel<TViewModel, TNavigationParameter>();
            viewModel.Initialize(parameter);
            IView viewFor = CreateViewFor<TViewModel>();
            viewFor.SetViewModel((IViewModel)viewModel);
            StartOpeningPage<TViewModel>(viewFor as Page);
        } 

        /// <summary>
        /// Navigates to a Page and automatically creates a new instance of
        /// the corresponding View Model. Also passes some parameters of the
        /// given type.
        /// </summary>
        public Task NavigateToViewModelAsync<TViewModel, TNavigationParameter>(TNavigationParameter parameter)
        {
            var viewModel = CreateViewModel<TViewModel, TNavigationParameter>();
            viewModel.Initialize(parameter);

            IView view = CreateViewFor<TViewModel>();
            view.SetViewModel(viewModel);

            return StartOpeningPage<TViewModel>(view as Page);
        }

        /// <summary>
        /// Opens the page a <see cref="MasterDetailData.ViewModelType"/> is referencing to.
        /// </summary>
        /// <param name="data"></param>
        public void SetDetail(MasterDetailData data)
        {
            typeof(MvvmNanoFormsPresenter).GetRuntimeMethod("NavigateToViewModel", parameters: new Type[0]).MakeGenericMethod(data.ViewModelType).Invoke(this, (object[])null);
        }

        /// <summary>
        /// Navigates to a Page and automatically creates a new instance of
        /// the corresponding View Model, without passing any parameter.
        /// </summary>
        public void NavigateToViewModel<TViewModel>()
        {
            MvvmNanoViewModel viewModel = MvvmNanoFormsPresenter.CreateViewModel<TViewModel>() as MvvmNanoViewModel;
            if (viewModel == null)
            {
                throw new MvvmNanoFormsPresenterException($"{typeof(TViewModel)} is not a MvvmNanoViewModel (without parameter).");
            }
            viewModel.Initialize();
            IView viewFor = this.CreateViewFor<TViewModel>();
            viewFor.SetViewModel(viewModel);
            StartOpeningPage<TViewModel>(viewFor as Page);
        }

        /// <summary>
        /// Navigates to a Page and automatically creates a new instance of
        /// the corresponding View Model, without passing any parameter.
        /// </summary>
        public Task NavigateToViewModelAsync<TViewModel>()
        {
            var viewModel = CreateViewModel<TViewModel>() as MvvmNanoViewModel;
            if (viewModel == null)
            {
                throw new MvvmNanoFormsPresenterException($"{typeof(TViewModel)} is not a MvvmNanoViewModel (without parameter).");
            }

            viewModel.Initialize();

            IView view = CreateViewFor<TViewModel>();
            view.SetViewModel(viewModel);

            return StartOpeningPage<TViewModel>(view as Page);
        }

        /// <summary>
        /// Creates a View of the given type.
        /// </summary>
        public IView CreateViewFor<TViewModel>()
        {
            string viewName = GetViewNameByViewModel(typeof(TViewModel));
            Type pageType = GetViewTypeByName(viewName);

            var view = Activator.CreateInstance(pageType) as IView;

            if (view == null)
            {
                throw new MvvmNanoFormsPresenterException($"{viewName} could not be found. Does it implement IView?");
            }

            if (!(view is Page))
            {
                throw new MvvmNanoFormsPresenterException($"{viewName} is not a Xamarin.Forms Page.");
            }

            return view;
        }

        private Type GetViewTypeByName(string viewName)
        {
            return _availableViewTypes
                .FirstOrDefault(t => t.Name == viewName);
        }

        /// <summary>
        /// This method is called whenever a Page should be shown. The default
        /// implementation pushes the Page to the navigation stack. Override it
        /// to implement your own navigation magic (for modals etc.).
        /// </summary>
        protected virtual Task OpenPageAsync(Page page)
        {
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return CurrentPage.Navigation.PushAsync(page, true);
        }

        /// <summary> 
        /// Makes sure that a detail page gets opened as detail page if a MasterDetailPage is set as MainPage.
        /// This method should be called bevore <see cref="OpenPageAsync"/>. 
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="page"></param>
        private async Task StartOpeningPage<TViewModel>(Page page)
        {
            if (Application.MasterPage != null && 
                Application.MasterDetails.FirstOrDefault(o => o.ViewModelType == typeof (TViewModel)) != null) //Check if the new page is a detail page
            {
                //Check if the current page is opened as modal page and close it if that is the case.
                if (Application.MasterPage.Detail.Navigation.ModalStack.Any()
                    && Application.MasterPage.Detail.Navigation.ModalStack.FirstOrDefault() == CurrentPage)
                {
                    await Application.MasterPage.Detail.Navigation.PopModalAsync();
                }
                Application.MasterPage.SetDetail(page);
            } 
            else
                await OpenPageAsync(page); 
        }

        private static IViewModel CreateViewModel<TViewModel>()
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>() as IViewModel;
            if (viewModel == null)
            {
                throw new MvvmNanoFormsPresenterException($"{typeof(TViewModel)} does not implement IViewModel.");
            }

            return viewModel;
        }

        private static IViewModel<TNavigationParameter> CreateViewModel<TViewModel, TNavigationParameter>()
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>() as IViewModel<TNavigationParameter>;
            if (viewModel == null)
            {
                throw new MvvmNanoFormsPresenterException($"{typeof(TViewModel)} does not implement IViewModel<{typeof(TNavigationParameter).Name}>");
            }

            return viewModel;
        } 
    }
} 
