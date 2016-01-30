using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MvvmNano
{
    /// <summary>
    /// The base class of every MvvmNano View Model
    /// </summary>
    public abstract class MvvmNanoViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected NavigationStep2<TNavigationViewModel> NavigateTo<TNavigationViewModel>()
        {
            return new NavigationStep2<TNavigationViewModel>();
        }

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
        public virtual void Initialize()
        {
            // Hook
        }
    }

    public class MvvmNanoViewModel<TNavigationParameter> : MvvmNanoViewModelBase, IViewModel<TNavigationParameter>
    {
        public virtual void Initialize(TNavigationParameter parameter)
        {
            // Hook
        }
    }
}

