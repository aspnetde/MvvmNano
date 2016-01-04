using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System;

namespace MvvmNano
{
    public abstract class MvvmNanoViewModelBase
    {
        protected class NavigationStep2<TNavigationViewModel>
        {
            public Task WithParameterAsync<TNavigationParameter>(TNavigationParameter parameter)
            {
                if (Presenter == null)
                    throw new InvalidOperationException("Please set MvvmNanoViewModel.Presenter.");

                return Presenter.ShowViewModelAsync<TNavigationViewModel, TNavigationParameter>(parameter);
            }
        }

        public static IPresenter Presenter { get; set; }

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

        protected Task NavigateToAsync<TNavigationViewModel>()
            where TNavigationViewModel : IViewModel
        {
            if (Presenter == null)
                throw new InvalidOperationException("Please set MvvmNanoViewModel.Presenter.");

            return Presenter.ShowViewModelAsync<TNavigationViewModel>();
        }

        public virtual void Dispose()
        {
        }
    }

    public class MvvmNanoViewModel : MvvmNanoViewModelBase, IViewModel
    {
        public virtual void Initialize()
        {
        }
    }

    public class MvvmNanoViewModel<TParameter> : MvvmNanoViewModelBase, IViewModel<TParameter>
    {
        public virtual void Initialize(TParameter parameter)
        {
        }
    }
}

