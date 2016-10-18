using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MvvmNano
{
    /// <summary>
    /// The base class of every MvvmNano View Model
    /// </summary>
    public abstract class MvvmNanoViewModelBase
    {
        private readonly IPresenter _presenter;

        /// <summary>
        /// Raised, whenever NotifyPropertyChanged is called. 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected MvvmNanoViewModelBase()
        {
            _presenter = MvvmNanoIoC.Resolve<IPresenter>();
        }

        protected MvvmNanoViewModelBase(IPresenter presenter)
        {
            _presenter = presenter;
        }

        /// <summary>
        /// Call this, whenever the value of one of your properties changes,
        /// so the UI can be notified.
        /// </summary>
        /// <param name="propertyName">Name of the property, usually optional to be set manually</param>
        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Navigates to another View Model.
        /// </summary>
        /// <typeparam name="TNavigationViewModel">The type of the View Model you want to navigate to.</typeparam>
        protected void NavigateTo<TNavigationViewModel>()
            where TNavigationViewModel : IViewModel
        {
            _presenter.NavigateToViewModel<TNavigationViewModel>();
        }

        /// <summary>
        /// Navigates to another View Model.
        /// </summary>
        /// <typeparam name="TNavigationViewModel">The type of the View Model you want to navigate to.</typeparam>
        protected Task NavigateToAsync<TNavigationViewModel>()
            where TNavigationViewModel : IViewModel
        {
            return _presenter.NavigateToViewModelAsync<TNavigationViewModel>();
        }

        /// <summary>
        /// Navigates to another View Model and passes a parameter.
        /// </summary>
        /// <param name="parameter">The parameter you want to pass to the View Model you want to navigate to.</param>
        /// <typeparam name="TNavigationViewModel">The type of the View Model you want to navigate to.</typeparam>
        /// <typeparam name="TNavigationParameter">The type of the parameter you want to pass to your View Model you are navigating to.</typeparam>
        protected void NavigateTo<TNavigationViewModel, TNavigationParameter>(TNavigationParameter parameter)
            where TNavigationViewModel : IViewModel<TNavigationParameter>
        {
            _presenter.NavigateToViewModel<TNavigationViewModel, TNavigationParameter>(parameter);
        }

        /// <summary>
        /// Navigates to another View Model and passes a parameter.
        /// </summary>
        /// <param name="parameter">The parameter you want to pass to the View Model you want to navigate to.</param>
        /// <typeparam name="TNavigationViewModel">The type of the View Model you want to navigate to.</typeparam>
        /// <typeparam name="TNavigationParameter">The type of the parameter you want to pass to your View Model you are navigating to.</typeparam>
        protected Task NavigateToAsync<TNavigationViewModel, TNavigationParameter>(TNavigationParameter parameter)
            where TNavigationViewModel : IViewModel<TNavigationParameter>
        {
            return _presenter.NavigateToViewModelAsync<TNavigationViewModel, TNavigationParameter>(parameter);
        }

        /// <summary>
        /// The perfect place to clean up (detaching event handlers, disposing resources, etc.)
        /// </summary>
        public virtual void Dispose()
        {
            // Hook
        }
    }

    public class MvvmNanoViewModel : MvvmNanoViewModelBase, IViewModel
    {
        /// <summary>
        /// The entry point of your View Model after it is opened.
        /// </summary>
        public virtual void Initialize()
        {
            // Hook
        }
    }

    public class MvvmNanoViewModel<TNavigationParameter> : MvvmNanoViewModelBase, IViewModel<TNavigationParameter>
    {
        /// <summary>
        /// Initializes the View Model when it is called by the Presenter,
        /// passing a parameter from the calling View Model
        /// </summary>
        /// <param name="parameter">The parameter passed by the calling View Model.</param>
        public virtual void Initialize(TNavigationParameter parameter)
        {
            // Hook
        }
    }
}

