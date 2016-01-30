using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MvvmNano
{
    /// <summary>
    /// The base class of every MvvmNano View Model
    /// </summary>
    public abstract class MvvmNanoViewModelBase
    {
        /// <summary>
        /// Raised, whenever NotifyPropertyChanged is called. 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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
        /// <typeparam name="TNavigationViewModel">The type of the View Model you want to navigate</typeparam>
        protected NavigationStep2<TNavigationViewModel> NavigateTo<TNavigationViewModel>()
        {
            return new NavigationStep2<TNavigationViewModel>();
        }

        /// <summary>
        /// The perfect place to clean up (detaching event handlers, disposing resources, etc.)
        /// </summary>
        public virtual void Dispose()
        {
            // Hook
        }

        protected class NavigationStep2<TNavigationViewModel>
        {
            private readonly IPresenter _presenter;

            public NavigationStep2()
            {
                _presenter = MvvmNanoIoC.Resolve<IPresenter>();
            }

            public void WithoutParameter()
            {
                _presenter.NavigateToViewModel<TNavigationViewModel>();
            }

            public void WithParameter<TNavigationParameter>(TNavigationParameter parameter)
            {
                _presenter.NavigateToViewModel<TNavigationViewModel, TNavigationParameter>(parameter);
            }
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

