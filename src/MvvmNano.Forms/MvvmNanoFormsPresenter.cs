 
using System;
using System.Linq;
using System.Reflection;
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

        private readonly Type[] _availableViewTypes;

        /// <summary>
        /// A read-only reference to our Xamarin.Forms Application instance
        /// </summary>
        protected readonly MvvmNanoApplication Application;

        /// <summary>
        /// Provides the Current Page, which is shown on top of all other
        /// (either modally, on the navigation stack, or just the app's 
        /// main page).
        /// </summary>
        public new Page CurrentPage
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
                BetterMasterDetailPage masterDetailPage = currentPage as BetterMasterDetailPage;
                if (masterDetailPage != null)
                    return masterDetailPage.Detail.Navigation.NavigationStack.LastOrDefault<Page>();

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
                throw new ArgumentNullException("application");

            Application = application;

            _availableViewTypes = Application
                .GetType()
                .GetTypeInfo()
                .Assembly
                .DefinedTypes
                .Select(t => t.AsType())
                .ToArray();
        }

        public string GetViewNameByViewModel(Type viewModelType)
        {
            return viewModelType.Name.Replace("ViewModel", "Page");
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
            OpenPage<TViewModel>(viewFor as Page);
        }

        public void SetDetail(MasterDetailData data)
        {

            typeof(MvvmNanoFormsPresenter).GetRuntimeMethod("NavigateToViewModel", parameters: new Type[0]).MakeGenericMethod(data.ViewModel).Invoke(this, (object[])null);
        }

        /// <summary>
        /// Navigates to a Page and automatically creates a new instance of
        /// the corresponding View Model, without passing any parameter.
        /// </summary>
        public void NavigateToViewModel<TViewModel>()
        {
            MvvmNanoViewModel viewModel = MvvmNanoFormsPresenter.CreateViewModel<TViewModel>() as MvvmNanoViewModel;
            if (viewModel == null)
                throw new MvvmNanoFormsPresenterException(typeof(TViewModel).ToString() + " is not a MvvmNanoViewModel (without parameter).");
            viewModel.Initialize();
            IView viewFor = this.CreateViewFor<TViewModel>();
            viewFor.SetViewModel((IViewModel)viewModel);
            this.OpenPage<TViewModel>(viewFor as Page);
        }

        /// <summary>
        /// Creates a View of the given type.
        /// </summary>
        public IView CreateViewFor<TViewModel>()
        {
            string viewName = typeof(TViewModel).Name.Replace(VIEW_MODEL_SUFFIX, VIEW_SUFFIX);
            Type pageType = _availableViewTypes
                .FirstOrDefault(t => t.Name == viewName);

            var view = Activator.CreateInstance(pageType) as IView;

            if (view == null)
                throw new MvvmNanoFormsPresenterException(viewName + " could not be found. Does it implement IView?");

            if (!(view is Page))
                throw new MvvmNanoFormsPresenterException(viewName + " is not a Xamarin.Forms Page.");

            return view;
        }

        /// <summary>
        /// This method is called whenever a Page should be shown. The default
        /// implementation pushes the Page to the navigation stack. Override it
        /// to implement your own navigation magic (for modals etc.).
        /// </summary>
        protected virtual void OpenPage(Page page)
        {
            if (page == null)
                throw new ArgumentNullException("page");

            Device.BeginInvokeOnMainThread(async () =>
                await CurrentPage.Navigation.PushAsync(page, true)
            );
        }

        private void OpenPage<TViewModel>(Page page)
        {
            if (Application.MasterPage != null && Application.MasterDetails.FirstOrDefault<MasterDetailData>((o => o.ViewModel == typeof(TViewModel))) != null)
                Application.MasterPage.SetDetail(page);
            else
                this.OpenPage(page);
        }

        private static IViewModel CreateViewModel<TViewModel>()
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>() as IViewModel;
            if (viewModel == null)
                throw new MvvmNanoFormsPresenterException(typeof(TViewModel) + " does not implement IViewModel.");

            return viewModel;
        }

        private static IViewModel<TNavigationParameter> CreateViewModel<TViewModel, TNavigationParameter>()
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>() as IViewModel<TNavigationParameter>;
            if (viewModel == null)
                throw new MvvmNanoFormsPresenterException(typeof(TViewModel) + " does not implement IViewModel<" + typeof(TNavigationParameter).Name + ">");

            return viewModel;
        }
    }
}

