using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MvvmNano;
using Xamarin.Forms;

namespace MvvmNano.Forms
{
    public class MvvmNanoFormsPresenter : IPresenter
    {
        private readonly Type[] _availableViewTypes;

        protected readonly Application Application;

        public MvvmNanoFormsPresenter(Application application)
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

        public async Task ShowViewModelAsync<TViewModel, TParameter>(TParameter parameter)
        {
            Type viewModelType = typeof(TViewModel);

            var viewModel = CreateViewModel<TViewModel, TParameter>(viewModelType);
            viewModel.Initialize(parameter);

            IView view = CreateView(viewModelType);
            view.SetViewModel(viewModel);

            await OpenPageAsync(view as Page);
        }

        public async Task ShowViewModelAsync<TViewModel>()
        {
            Type viewModelType = typeof(TViewModel);

            var viewModel = CreateViewModel<TViewModel>(viewModelType) as MvvmNanoViewModel;
            if (viewModel == null)
                throw new InvalidOperationException(viewModelType + " is not a MvvmNanoViewModel.");
            
            viewModel.Initialize();

            IView view = CreateView(viewModelType);
            view.SetViewModel(viewModel);

            await OpenPageAsync(view as Page);
        }

        private static IViewModel CreateViewModel<TViewModel>(Type viewModelType)
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>() as IViewModel;
            if (viewModel == null)
                throw new InvalidOperationException(viewModelType + " does not implement IViewModel.");

            return viewModel;
        }

        private static IViewModel<TParameter> CreateViewModel<TViewModel, TParameter>(Type viewModelType)
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>() as IViewModel<TParameter>;
            if (viewModel == null)
                throw new InvalidOperationException(viewModelType + " does not implement IViewModel<" + typeof(TParameter).Name + ">");

            return viewModel;
        }

        private IView CreateView(Type viewModelType)
        {
            string viewName = viewModelType.Name.Replace("ViewModel", "Page");
            Type pageType = _availableViewTypes
                .FirstOrDefault(t => t.Name == viewName);

            var view = Activator.CreateInstance(pageType) as IView;

            if (view == null)
                throw new InvalidOperationException(viewName + " could not be found. Does it implement IView?");

            if (!(view is Page))
                throw new InvalidOperationException(viewName + " is not a Xamarin.Forms Page.");

            return view;
        }

        protected virtual Task OpenPageAsync(Page page)
        {
            if (page == null)
                throw new ArgumentNullException("page");

            return Application.MainPage.Navigation.PushAsync(page, true);
        }
    }
}

