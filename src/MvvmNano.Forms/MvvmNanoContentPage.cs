using System;
using Xamarin.Forms;
using System.Linq.Expressions;

namespace MvvmNano.Forms
{
    /// <summary>
    /// The base class for all of your MasterContent Pages
    /// </summary>
    public abstract class MvvmNanoContentPage<TViewModel> : ContentPage, IView
        where TViewModel : IViewModel
    {
        /// <summary>
        /// The current instance of this Pages's View Model.
        /// </summary>
        protected TViewModel ViewModel => (TViewModel)BindingContext;

        /// <summary>
        /// Convenience helper, which enables you to bind any property
        /// of your View Model to an object you pass in. 
        /// </summary>
        protected void BindToViewModel(BindableObject self, BindableProperty targetProperty, 
            Expression<Func<TViewModel, object>> sourceProperty, BindingMode mode = BindingMode.Default, 
            IValueConverter converter = null, string stringFormat = null)
        {
            self.SetBinding(targetProperty, sourceProperty, mode, converter, stringFormat);
        }
            
        /// <summary>
        /// Sets the View Model for this Page. Automatically 
        /// called by MvvmNano.
        /// </summary>
        /// <param name="viewModel">The View Model.</param>
        public void SetViewModel(IViewModel viewModel)
        {
            BindingContext = viewModel;

            OnViewModelSet();
        }

        /// <summary>
        /// Called whenever the View Model is being set, so it's a good place
        /// to start doing anything you want to do it.
        /// </summary>
        public virtual void OnViewModelSet()
        {
            // Hook
        }

        /// <summary>
        /// Cleans up the View Model aka BindingContext and the content.
        /// </summary>
        public virtual void Dispose()
        {
            ViewModel.Dispose();
            BindingContext = null;
            Content = null;
        }
    }
}

