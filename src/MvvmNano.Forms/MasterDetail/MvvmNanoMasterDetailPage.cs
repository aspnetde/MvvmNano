using System;
using System.Linq.Expressions;
using Xamarin.Forms;

namespace MvvmNano.Forms.MasterDetail
{
    /// <summary>
    /// A MasterDetailPage implementation to use in the MvvmNanoContext.
    /// Derive from this page to create a master layout that fits the style of your app or use the default implementation <see cref="MvvmNanoDefaultMasterDetailPage{TViewModel}"/>.
    /// Add details in your App.cs by calling AddSiteToDetailPages(new MvvmNanoMasterDetailData(typeof (YourViewModel), "PageTitle"));  
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public abstract class MvvmNanoMasterDetailPage<TViewModel> : MvvmNanoMasterDetailPageBase, IView where TViewModel : IViewModel
    {
        /// <summary>
        /// The current instance of this Pages's View Model.
        /// </summary>
        protected TViewModel ViewModel
        {
            get { return (TViewModel)BindingContext; }
        }

        /// <summary>
        /// Cleans up the View Model aka BindingContext and the content.
        /// </summary>
        public void Dispose()
        {
            ViewModel.Dispose();
            Master.BindingContext = null;
        }

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
        }
    }
}