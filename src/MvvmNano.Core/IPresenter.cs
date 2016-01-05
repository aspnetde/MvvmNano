namespace MvvmNano
{
    public interface IPresenter
    {
        void NavigateToViewModel<TViewModel>();
        void NavigateToViewModel<TViewModel, TNavigationParameter>(TNavigationParameter parameter);
    }
}

