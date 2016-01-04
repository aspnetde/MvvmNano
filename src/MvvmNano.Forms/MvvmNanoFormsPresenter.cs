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

        public async Task ShowViewModelAsync<TViewModel>(object parameter) where TViewModel : IViewModel
        {
            Type viewModelType = typeof(TViewModel);

            IViewModel viewModel = CreateViewModel<TViewModel>(viewModelType);
            viewModel.Initialize(parameter);

            IView view = CreateView(viewModelType);
            view.SetViewModel(viewModel);

            await OpenPageAsync(view as Page);
        }

        private static IViewModel CreateViewModel<TViewModel>(Type viewModelType)
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>() as IViewModel;

            if (viewModel == null)
                throw new InvalidOperationException(viewModelType + " could not be created.");

            return viewModel;
        }

        private IView CreateView(Type viewModelType)
        {
            string viewName = viewModelType.Name.Replace("ViewModel", "Page");
            Type pageType = _availableViewTypes
                .FirstOrDefault(t => t.Name == viewName);

            var view = Activator.CreateInstance(pageType) as IView;

            if (view == null)
                throw new InvalidOperationException(viewName + " could not be found.");

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

