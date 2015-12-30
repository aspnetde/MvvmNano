using System.Threading.Tasks;

namespace MvvmNano
{
    public interface IPresenter
    {
        Task ShowViewModelAsync<TViewModel>(object parameter) 
            where TViewModel : IViewModel;
    }
}

