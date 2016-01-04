using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System;

namespace MvvmNano
{
    public class MvvmNanoViewModel : MvvmNanoViewModel<object>
    {
    }

    public class MvvmNanoViewModel<TParameter> : IViewModel<TParameter>
    {
        public class NavigationStep2<TNavigationViewModel>
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

        public virtual void Initialize(TParameter parameter)
        {
            // Hook
        }

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
            where TNavigationViewModel : IViewModel<object>
        {
            if (Presenter == null)
                throw new InvalidOperationException("Please set MvvmNanoViewModel.Presenter.");

            return Presenter.ShowViewModelAsync<TNavigationViewModel, object>(null);        
        }

        public virtual void Dispose()
        {
            // Hook
        }
    }
}

