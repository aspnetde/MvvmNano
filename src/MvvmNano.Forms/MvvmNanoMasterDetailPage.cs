using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.ServiceModel.Channels;
using Xamarin.Forms;

namespace MvvmNano.Forms
{
    /// <summary>
    /// The MvvmNano MasterDetailPage that allows easy adding of detail pages within the MvvmNano context.
    /// Add details in your App.cs by calling AddSiteToDetailPages(new MasterDetailData(typeof (YourViewModel), "PageTitle"));  
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public abstract class MvvmNanoMasterDetailPage<TViewModel> : BetterMasterDetailPage, IView where TViewModel : IViewModel
    { 
        protected TViewModel ViewModel
        {
            get { return (TViewModel)BindingContext; }
        }  

        public void Dispose()
        {
            ViewModel.Dispose();
            Master.BindingContext = null;
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
            OnViewModelSet();
        }

        public virtual void OnViewModelSet()
        {
        }
    }
}