using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MvvmNano
{
    public abstract class MvvmNanoViewModelBase
    {
        private static IPresenter _presenter;
        public static IPresenter Presenter 
        { 
            get
            {
                if (_presenter == null)
                    throw new MvvmNanoException("Please set MvvmNanoViewModelBase.Presenter.");
                
                return _presenter;
            }
            set { _presenter = value; }
        }

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
            public void WithoutParameter()
            {
                Presenter.NavigateToViewModel<TNavigationViewModel>();
            }

            public void WithParameter<TNavigationParameter>(TNavigationParameter parameter)
            {
                Presenter.NavigateToViewModel<TNavigationViewModel, TNavigationParameter>(parameter);
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

