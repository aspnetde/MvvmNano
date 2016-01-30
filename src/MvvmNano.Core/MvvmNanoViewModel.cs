using System.ComponentModel;
using System.Runtime.CompilerServices;

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

        protected void NavigateTo<TNavigationViewModel>()
        {
            _presenter.NavigateToViewModel<TNavigationViewModel>();
        }

        protected void NavigateTo<TNavigationViewModel, TNavigationParameter>(TNavigationParameter parameter)
        {
            _presenter.NavigateToViewModel<TNavigationViewModel, TNavigationParameter>(parameter);
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

