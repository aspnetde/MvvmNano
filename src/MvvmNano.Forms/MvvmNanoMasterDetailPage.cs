using System;
using System.Linq;
using System.Linq.Expressions;
using Xamarin.Forms;

namespace MvvmNano.Forms
{
    /// <summary>
    /// The MvvmNano MasterDetailPage that allows easy adding of detail pages within the MvvmNano context.
    /// Add details in your App.cs by calling AddSiteToDetailPages(new MasterDetailData(typeof (YourViewModel), "PageTitle"));  
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public abstract class MvvmNanoMasterDetailPage<TViewModel> : NanoMasterDetailPage, IView where TViewModel : IViewModel
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

    /// <summary>
    /// A MasterDetailPage implementation that fits to the MvvmNano framework.
    /// </summary>
    public class NanoMasterDetailPage : MasterDetailPage
    {
        private MvvmNanoMasterDetailApplication _application;

        private MvvmNanoFormsPresenter _presenter;

        private Page _detail;

        /// <summary>
        /// This ListView contains all detail entrys. Add it to your custom master.
        /// </summary>
        protected ListView DetailListView { get; } = new ListView();

        /// <summary>
        /// ContentPage that represents the menu.
        /// </summary>
        protected new ContentPage Master => base.Master as ContentPage;

        /// <summary>
        /// MasterContent of the menu.
        /// </summary>
        public View MasterContent
        {
            get { return Master.Content; }
            set { Master.Content = value; }
        }

        public NanoMasterDetailPage()
        {
            _application = Application.Current as MvvmNanoMasterDetailApplication;
            _presenter = (MvvmNanoFormsPresenter)MvvmNanoIoC.Resolve<IPresenter>();

            DetailListView.ItemsSource = _application.MasterDetails;
            DetailListView.ItemTemplate = new DataTemplate(() =>
            {
                Label titleLabel = new Label
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                titleLabel.SetBinding(Label.TextProperty, "Title");
                return new ViewCell
                {
                    View = titleLabel
                };
            });
            base.Master = new ContentPage() { Title = "Master", Content = DetailListView };
            Detail = new ContentPage() { Title = "Default content page." };
        }

        /// <summary>
        /// Hide the menu if an item is tapped that is already selected. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="itemTappedEventArgs"></param>
        private void MenuEntryTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            if (itemTappedEventArgs.Item == DetailListView.SelectedItem)
                IsPresented = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="selectedItemChangedEventArgs"></param>
        private void MenuEntrySelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var data = DetailListView.SelectedItem as MasterDetailData;
            _presenter.SetDetail(data);
        }

        /// <summary>
        /// Add event handler
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            DetailListView.ItemSelected += MenuEntrySelected;
            DetailListView.ItemTapped += MenuEntryTapped;
        }

        /// <summary>
        /// All events: ABOARD THE SINKING PAGE!!!
        /// </summary>
        protected override void OnDisappearing()
        {
            DetailListView.ItemSelected -= MenuEntrySelected;
            DetailListView.ItemTapped -= MenuEntryTapped;
            base.OnDisappearing();
        }

        /// <summary>
        /// Set the Detail, hide the menu and make sure, that the correct menu entry is selected.
        /// </summary>
        /// <param name="page">The new detail.</param>
        public void SetDetail(Page page)
        {
            if (_detail != page)
            {
                _detail = page;
                Detail = new MvvmNanoNavigationPage(page);
                IsPresented = false;
            }
            if (DetailListView.SelectedItem == null || _presenter.GetViewNameByViewModel(((MasterDetailData)DetailListView.SelectedItem).ViewModelType) !=
                page.GetType().Name)
            {
                DetailListView.SelectedItem =
                    _application.MasterDetails.FirstOrDefault(
                        o => _presenter.GetViewNameByViewModel(o.ViewModelType) == page.GetType().Name);
            }
        }
    }
}
