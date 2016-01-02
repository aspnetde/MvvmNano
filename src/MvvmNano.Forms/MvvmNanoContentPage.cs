using System;
using Xamarin.Forms;
using System.Linq.Expressions;

namespace MvvmNano.Forms
{
    public abstract class MvvmNanoContentPage<TViewModel> : ContentPage, IView
        where TViewModel : IViewModel
    {
        protected TViewModel ViewModel
        {
            get { return (TViewModel)BindingContext; }
        }

        protected void BindToViewModel(BindableObject self, BindableProperty targetProperty, 
            Expression<Func<TViewModel, object>> sourceProperty, BindingMode mode = BindingMode.Default, 
            IValueConverter converter = null, string stringFormat = null)
        {
            self.SetBinding(targetProperty, sourceProperty, mode, converter, stringFormat);
        }

        public void SetViewModel(IViewModel viewModel)
        {
            BindingContext = viewModel;
        }

        public virtual void Dispose()
        {
            ViewModel.Dispose();
            BindingContext = null;
            Content = null;
        }
    }
}

