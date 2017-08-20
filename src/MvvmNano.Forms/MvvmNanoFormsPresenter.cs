using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MvvmNano.Forms.Internals;
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
        private Type[] _availableViewTypes;

        /// <summary>
        /// A read-only reference to our Xamarin.Forms Application instance
        /// </summary>
        protected Application Application { get; private set; }

        /// <summary>
        /// Provides the Current Page, which is shown on top of all other
        /// (either modally, on the navigation stack, or just the app's 
        /// main page).
        /// </summary>
        public Page CurrentPage
        {
            get 
            {
                Page GetCurrentPage()
                {
                    Page modalPage = Application.MainPage.Navigation.ModalStack.LastOrDefault();

                    if (modalPage != null)
                    {
                        return modalPage;
                    }

                    Page topPage = Application.MainPage.Navigation.NavigationStack.LastOrDefault();

                    return topPage ?? Application.MainPage;
                }

                var currentPage = GetCurrentPage();
                return GetCurrentChildPage(currentPage);
            }
        }

        private Page GetCurrentChildPage(Page page)
        { 
            var tabbedPage = page as TabbedPage;
            if (tabbedPage != null)
            {
                return GetCurrentChildPage(tabbedPage.CurrentPage);
            }

            var masterPage = page as MvvmNanoMasterDetailPageBase;
            if (masterPage != null)
            {
                var navigation = (MvvmNanoNavigationPage)masterPage.Detail;
                return GetCurrentChildPage(navigation.CurrentPage);
            }

            return page;
        }

        /// <summary>
        /// Initializes a new instance of MvvmNanoFormsPresenter.
        /// </summary>
        /// <param name="application">Our current Xamarin.Forms application. 
        /// Can't be null as it is needed to present our Pages.</param>
        public MvvmNanoFormsPresenter(Application application)
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
        public void SetApplication(Application application)
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

        #region navigate with parameter

        /// <summary>
        /// Navigates to a Page and automatically creates a new instance of
        /// the corresponding View Model. Also passes some parameters of the
        /// given type.
        /// </summary>
        public void NavigateToViewModel<TViewModel, TNavigationParameter>(TNavigationParameter parameter)
        {
            IView view = CreateCompletePageWithParameter<TViewModel, TNavigationParameter>(parameter); 
            OpenPage(view as Page);
        } 

        /// <summary>
        /// Navigates to a Page and automatically creates a new instance of
        /// the corresponding View Model. Also passes some parameters of the
        /// given type.
        /// </summary>
        public Task NavigateToViewModelAsync<TViewModel, TNavigationParameter>(TNavigationParameter parameter)
        {
            IView view = CreateCompletePageWithParameter<TViewModel, TNavigationParameter>(parameter);

            return OpenPageAsync(view as Page);
        }

        /// <summary>
        /// Creates the Page, resolves the ViewModel and initalizes it with the given parameter.
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <typeparam name="TNavigationParameter"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private IView CreateCompletePageWithParameter<TViewModel, TNavigationParameter>(TNavigationParameter parameter)
        {
            var viewModel = CreateViewModel<TViewModel, TNavigationParameter>();
            viewModel.Initialize(parameter);

            IView view = CreateViewFor<TViewModel>();
            view.SetViewModel(viewModel);
            return view;
        }

        private IViewModel<TNavigationParameter> CreateViewModel<TViewModel, TNavigationParameter>()
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>() as IViewModel<TNavigationParameter>;
            if (viewModel == null)
            {
                throw new MvvmNanoFormsPresenterException($"{typeof(TViewModel)} does not implement IViewModel<{typeof(TNavigationParameter).Name}>");
            }
            return viewModel;
        }
        #endregion

        #region navigate without parameter

        /// <summary>
        /// Navigates to a Page and automatically creates a new instance of
        /// the corresponding View Model, without passing any parameter.
        /// </summary>
        public void NavigateToViewModel<TViewModel>()
        {
            var page = CreateCompletePage<TViewModel>();
            OpenPage(page);
        }

        /// <summary>
        /// Navigates to a Page and automatically creates a new instance of
        /// the corresponding View Model, without passing any parameter.
        /// </summary>
        public async Task NavigateToViewModelAsync<TViewModel>()
        {
            var page = CreateCompletePage<TViewModel>(); 
            await OpenPageAsync(page);
        }

        private Page CreateCompletePage<TViewModel>()
        {
            var viewModel = CreateViewModel<TViewModel>() as MvvmNanoViewModel;
            if (viewModel == null)
            {
                throw new MvvmNanoFormsPresenterException($"{typeof(TViewModel)} is not a MvvmNanoViewModel (without parameter).");
            }

            viewModel.Initialize();

            IView view = CreateViewFor<TViewModel>();
            view.SetViewModel(viewModel);

            var page = view as Page;
            return page;
        }

        private IViewModel CreateViewModel<TViewModel>()
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>() as IViewModel;
            if (viewModel == null) 
                throw new MvvmNanoFormsPresenterException($"{typeof(TViewModel)} does not implement IViewModel."); 

            return viewModel;
        }

        #endregion

        #region view creation

        /// <summary>
        /// Creates a View of the given type.
        /// </summary>
        public IView CreateViewFor<TViewModel>()
        {
            string viewName = ViewViewModelHelper.ViewNameFromViewModel(typeof(TViewModel));
            Type pageType = _availableViewTypes
                .FirstOrDefault(t => t.Name == viewName);

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

        /// <summary>
        /// Creates a View of the given type.
        /// </summary>
        public IView CreateViewFor(Type viewModelType)
        {
            string viewName = ViewViewModelHelper.ViewNameFromViewModel(viewModelType);
            Type pageType = _availableViewTypes
                .FirstOrDefault(t => t.Name == viewName);

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

        #endregion

        /// <summary>
        /// Trys to opent he page to be presented as another detail of the current master detail view.
        /// The page needs to be registered using <see cref="MvvmNanoMasterDetailPageBase.AddDetailData{TViewModel}"/>.
        /// </summary>
        /// <param name="currentPage">Currently presented page.</param>
        /// <param name="newPage">New page to be presented.</param>
        /// <returns>Wether or not the page is presented as detail.</returns>
        private bool TryOpenAsDetail(Page currentPage, Page newPage)
        {
            var parent = currentPage.Parent;
            if (!(parent is Page))
                return false;

            var masterDetailPage = parent as MvvmNanoMasterDetailPageBase; 
            if (masterDetailPage != null) 
            {
                var pageName = ViewViewModelHelper.ViewModelNameFromView(newPage.GetType());
                var detailData = masterDetailPage.MasterDetails.FirstOrDefault(x => x.ViewModelType.Name == pageName);
                if (detailData == null)
                    return false;
                masterDetailPage.SetDetail(newPage, detailData);
                return true; 
            } 
            //Try again with parent as current page.
            return TryOpenAsDetail((Page)parent, newPage);
        }

        /// <summary>
        /// This method is called whenever a Page should be shown. The default
        /// implementation pushes the Page to the navigation stack. Override it
        /// to implement your own navigation magic (for modals etc.).
        /// </summary>
        protected virtual void OpenPage(Page page)
        {
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            if (TryOpenAsDetail(CurrentPage, page))
                return;
             
            Device.BeginInvokeOnMainThread(async () => 
                await CurrentPage.Navigation.PushAsync(page, true)
            );
        }

        /// <summary>
        /// This method is called whenever a Page should be shown. The default
        /// implementation pushes the Page to the navigation stack. Override it
        /// to implement your own navigation magic (for modals etc.).
        /// </summary>
        protected virtual async Task OpenPageAsync(Page page)
        {
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            if (TryOpenAsDetail(CurrentPage, page))
                return;

            await CurrentPage.Navigation.PushAsync(page, true);
        }  
    }
}

