using System.Threading.Tasks;

namespace MvvmNano
{
    public interface IPresenter
    {
        Task ShowViewModelAsync<TViewModel>();
        Task ShowViewModelAsync<TViewModel, TNavigationParameter>(TNavigationParameter parameter);
    }
}

