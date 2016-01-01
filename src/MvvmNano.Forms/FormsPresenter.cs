using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MvvmNano;
using Xamarin.Forms;

namespace MvvmNano.Forms
{
    public class FormsPresenter : IPresenter
    {
        private readonly Application _application;

        private readonly Type[] _availableViewTypes;

        public FormsPresenter(Application application)
        {
            _application = application;

            _availableViewTypes = _application
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

            IViewModel viewModel = CreateViewModel(viewModelType);
            viewModel.Initialize(parameter);

            IView view = CreateView(viewModelType);
            view.SetViewModel(viewModel);

            await OpenPageAsync(view as Page);
        }

        private static IViewModel CreateViewModel(Type viewModelType)
        {
            return Activator.CreateInstance(viewModelType) as IViewModel;
        }

        private IView CreateView(Type viewModelType)
        {
            string viewName = viewModelType.Name.Replace("ViewModel", "Page");
            Type pageType = _availableViewTypes
                .FirstOrDefault(t => t.Name == viewName);

            var view = Activator.CreateInstance(pageType) as IView;
            if (view == null)
                throw new InvalidOperationException(viewName + " could not be found.");

            return view;
        }

        private Task OpenPageAsync(Page page)
        {
            if (page == null)
                throw new ArgumentNullException("page");

            return _application.MainPage.Navigation.PushAsync(page, true);
        }
    }
}

