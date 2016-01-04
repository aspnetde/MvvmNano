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



        protected Task ShowViewModelAsync<TViewModel, TViewModelParameter>(TViewModelParameter parameter) 
            where TViewModel : IViewModel<TViewModelParameter>
        {
            if (Presenter == null)
                throw new InvalidOperationException("Please set MvvmNanoViewModel.Presenter.");

            return Presenter.ShowViewModelAsync<TViewModel, TViewModelParameter>(parameter);
        }

        public virtual void Dispose()
        {
            // Hook
        }
    }
}

