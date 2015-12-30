using System.Threading.Tasks;

namespace MvvmNano.Core
{
    public interface IPresenter
    {
        Task ShowViewModelAsync<TViewModel>(object parameter) 
            where TViewModel : IViewModel;
    }
}

