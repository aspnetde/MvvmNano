using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using MvvmNano.Forms.Internals;
using Xamarin.Forms;

namespace MvvmNano.Forms
{
    public abstract class MvvmNanoMasterDetailPageBase : MasterDetailPage
    {    
        private Page _detail;

        public ObservableCollection<MvvmNanoMasterDetailData> MasterDetails { get; } = new ObservableCollection<MvvmNanoMasterDetailData>();

        /// <summary>
        /// This ListView contains all detail entrys. Add it to your custom master.
        /// </summary>
        protected ListView DetailListView { get; } = new ListView();

        /// <summary>
        /// ContentPage that represents the menu.
        /// </summary>
        protected new ContentPage Master => base.Master as ContentPage; 

        /// <summary>
        /// Content of the master part, this is the slide out panel containing the menu.
        /// </summary>
        public View MasterContent
        {
            get { return Master.Content; }
            set { Master.Content = value; }
        } 

        public MvvmNanoMasterDetailPageBase()
        {  
            DetailListView.ItemsSource = MasterDetails;
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
            base.Master = new ContentPage { Title = "Master", Content = DetailListView };
            Detail = new ContentPage { Title = "Default content page." };
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

        protected void AddDetailData<TViewModel>(MvvmNanoMasterDetailData<TViewModel> data) where TViewModel : MvvmNanoViewModel
        {
            var detailData = data as MvvmNanoMasterDetailData;
            detailData.ViewModelType = data.ViewModelType;
            MasterDetails.Add(detailData);

            //Check if this is the first window, show it if that is the case.
            if (MasterDetails.Count == 1)
            {
                SetDetail(detailData);
            }
        }

        /// <summary>
        /// Set the Detail, hide the menu and make sure, that the correct menu entry is selected.
        /// </summary>
        /// <param name="page">The new detail.</param>
        internal void SetDetail(Page page, MvvmNanoMasterDetailData pageData = null)
        {
            //Resolve the page data if it is not given
            if (pageData == null)
            {
                pageData = MasterDetails.FirstOrDefault(
                    x => ViewViewModelHelper.ViewNameFromViewModel(x.ViewModelType) == page.GetType().Name);

                if (pageData == null)
                    throw new MvvmNanoException($"There is no detail registered for the page ${page.GetType().Name}.");
            } 

            //Set the title if the page does not have one
            if (string.IsNullOrEmpty(page.Title))
            { 
                page.Title = pageData.Title;
            }

            //Show the page if it is not already presented
            if (_detail != page)
            {
                _detail = page;
                Detail = new MvvmNanoNavigationPage(page)
                {
                    Parent = this
                };
                IsPresented = false;
            }

            //Highlight the new page
            if (DetailListView.SelectedItem == null || //No page is selected
                 ViewViewModelHelper.ViewNameFromViewModel(((MvvmNanoMasterDetailData) DetailListView.SelectedItem).ViewModelType) != page.GetType().Name) //A page with a different name is selected
            { 
                DetailListView.SelectedItem = pageData;
            }
        }

        /// <summary>
        /// Set the page of <see cref="viewModelType"/> as Detail.
        /// </summary> 
        internal void SetDetail(Type viewModelType) 
        {
            var page = CreateView(viewModelType); 
            SetDetail(page);
        }

        /// <summary>
        /// Opens the page a <see cref="MvvmNanoMasterDetailData.ViewModelType"/> is referencing to.
        /// </summary>
        /// <param name="data"></param>
        internal void SetDetail(MvvmNanoMasterDetailData data)
        {
            var page = CreateView(data.ViewModelType); 
            SetDetail(page, data);
        }

        private Page CreateView(Type viewModelType)
        {
            try
            {
                var viewModel = GetViewModel(viewModelType); 
                var view = MvvmNanoIoC.Resolve<IPresenter>().CreateViewFor(viewModelType);
                view.SetViewModel(viewModel);

                return view as Page;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        private IViewModel GetViewModel(Type viewModelType)
        {
            try
            {
                var allMethods = typeof(MvvmNanoMasterDetailPageBase).GetRuntimeMethods();
                MethodInfo method = allMethods.First(x => x.Name == nameof(ResolveViewModel));
                MethodInfo genericMethod = method.MakeGenericMethod(new []{viewModelType});
                var viewModel = (IViewModel)genericMethod.Invoke(this, null);
                return viewModel;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        private IViewModel ResolveViewModel<TViewModel>()
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>() as IViewModel;
            if (viewModel == null)
            {
                throw new MvvmNanoFormsPresenterException($"{typeof(TViewModel)} does not implement IViewModel.");
            }

            return viewModel;
        }
    }
} 