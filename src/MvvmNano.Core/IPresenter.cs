using System.Threading.Tasks;

namespace MvvmNano
{
    public interface IPresenter
    {
        Task ShowViewModelAsync<TViewModel, TParameter>(TParameter parameter);
    }
}

