using Xamarin.Forms;

namespace MvvmNano.Forms.MasterDetail
{
    /// <summary>
    /// Provides a default implementation for the <see cref="MvvmNanoMasterDetailPage{TViewModel}"/> that is using a ListView to show the details in a menu.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public class MvvmNanoDefaultMasterDetailPage<TViewModel> : MvvmNanoMasterDetailPage<TViewModel> where TViewModel : IViewModel
    {
        /// <summary>
        /// Default listview to represent all details.
        /// </summary>
        protected ListView DetailListView { get; } = new ListView();

        /// <summary>
        /// Creates a <see cref="ContentPage"/> that contains a <see cref="ListView"/> that holds the <see cref="MvvmNanoMasterDetailData"/>.
        /// </summary>
        /// <returns></returns>
        protected override Page CreateMasterPage()
        {
            DetailListView.ItemsSource = MasterDetails;
            DetailListView.ItemTemplate = GetItemTemplate();
            return new ContentPage { Content = DetailListView };
        }

        /// <summary>
        /// Creates the <see cref="DetailListView"/> item template.
        /// </summary>
        /// <returns></returns>
        protected virtual DataTemplate GetItemTemplate()
        {
            return new DataTemplate(() =>
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
        }

        protected override void DetailSet(MvvmNanoMasterDetailData lastDetailData, MvvmNanoMasterDetailData newDetailData, Page page)
        {
            //Highlight the new page
            if (DetailListView.SelectedItem == null || //No page was selected
                lastDetailData != newDetailData) //A page with a different name is selected
            {
                DetailListView.SelectedItem = newDetailData;
            }
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
            var data = DetailListView.SelectedItem as MvvmNanoMasterDetailData;
            SetDetail(data);
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
    }
}