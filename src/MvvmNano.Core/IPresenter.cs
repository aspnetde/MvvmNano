using System.Threading.Tasks;

namespace MvvmNano
{
    /// <summary>
    /// The central point of navigation, which is responsible for creating 
    /// the Views along with their View Models and pushing those views to 
    /// the navigation stack.
    /// </summary>
    public interface IPresenter
    {
        /// <summary>
        /// Changes the root page to the view of the given View model.
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        void ChangeRootViewModel<TViewModel>() where TViewModel : MvvmNanoViewModel;

        /// <summary>
        /// Navigates to the View Model of the given type.
        /// </summary>
        void NavigateToViewModel<TViewModel>();

        /// <summary>
        /// Navigates to the View Model of the given type.
        /// </summary>
        Task NavigateToViewModelAsync<TViewModel>();

        /// <summary>
        /// Navigates to the View Model of the given type and passes a parameter.
        /// </summary>
        void NavigateToViewModel<TViewModel, TNavigationParameter>(TNavigationParameter parameter);

        /// <summary>
        /// Navigates to the View Model of the given type and passes a parameter.
        /// </summary>
        Task NavigateToViewModelAsync<TViewModel, TNavigationParameter>(TNavigationParameter parameter);

        /// <summary>
        /// Creates the view for the given view model type.
        /// </summary>
        IView CreateViewFor<TViewModel>();
    }
}

