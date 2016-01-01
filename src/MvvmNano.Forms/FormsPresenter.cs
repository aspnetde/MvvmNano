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

        public FormsPresenter(Application application)
        {
            _application = application;
        }

        public async Task ShowViewModelAsync<TViewModel>(object parameter) where TViewModel : IViewModel
        {
            Type viewModelType = typeof(TViewModel);
            string pageName = viewModelType.Name.Replace("ViewModel", "Page");

            Type pageType = _application.GetType().GetTypeInfo().Assembly.DefinedTypes
                .Select(t => t.AsType())
                .FirstOrDefault(t => t.Name == pageName);

            var viewModel = Activator.CreateInstance(viewModelType) as IViewModel;
            viewModel.Initialize(parameter);

            var page = Activator.CreateInstance(pageType) as Page;
            ((IView)page).SetViewModel(viewModel);

            await _application.MainPage.Navigation.PushAsync(page, true);
        }
    }
}

