using System.Linq; 
using Xamarin.Forms;

namespace MvvmNano.Forms
{
    /// <summary>
    /// A MasterDetailPage implementation that fits to the MvvmNano framework.
    /// </summary>
    public class NanoMasterDetailPage :  MasterDetailPage
    {
        private MvvmNanoApplication Application => Xamarin.Forms.Application.Current as MvvmNanoApplication;

        private MvvmNanoFormsPresenter Presenter => (MvvmNanoFormsPresenter)MvvmNanoIoC.Resolve<IPresenter>();
 
        /// <summary>
        /// This ListView contains all detail entrys. Add it to your custom master.
        /// </summary>
        protected ListView DetailListView { get; private set; }= new ListView();

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
            DetailListView.ItemsSource = Application.MasterDetails;
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
            base.Master = new ContentPage() {Title = "Master", Content = DetailListView}; 
            Detail = new ContentPage() {Title = "Default content page."};
            DetailListView.ItemSelected += MenuEntrySelected;
            DetailListView.ItemTapped += MenuEntryTapped;
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
            Presenter.SetDetail(data);
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

        private Page _detail;

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
            if (DetailListView.SelectedItem == null || Presenter.GetViewNameByViewModel(((MasterDetailData) DetailListView.SelectedItem).ViewModelType) !=
                page.GetType().Name)
            {
                DetailListView.SelectedItem =
                    Application.MasterDetails.FirstOrDefault(
                        o => Presenter.GetViewNameByViewModel(o.ViewModelType) == page.GetType().Name);
            }
        }
    }
}