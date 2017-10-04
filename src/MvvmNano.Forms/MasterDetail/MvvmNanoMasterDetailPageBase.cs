using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using MvvmNano.Forms.Internals;
using Xamarin.Forms;

namespace MvvmNano.Forms.MasterDetail
{ 
    /// <summary>
    /// Base page for <see cref="MvvmNanoMasterDetailPage{TViewModel}"/>, dont directly derive from this class. Use <see cref="MvvmNanoMasterDetailPage{TViewModel}"/> instead!
    /// </summary>
    public abstract class MvvmNanoMasterDetailPageBase : MasterDetailPage
    {
        /// <summary>
        /// The currently presented detail.
        /// </summary>
        private Page _detail;

        /// <summary>
        /// The data corrisponding with the currently presented detail page.
        /// </summary>
        private MvvmNanoMasterDetailData _currentDetailData;

        /// <summary>
        /// All registered details.
        /// </summary>
        public ObservableCollection<MvvmNanoMasterDetailData> MasterDetails { get; } = new ObservableCollection<MvvmNanoMasterDetailData>();  

        public MvvmNanoMasterDetailPageBase()
        {
            var masterPage = CreateMasterPage();

            //Set a default master title if not set yet
            if (string.IsNullOrEmpty(masterPage.Title))
                masterPage.Title = "master";

            Master = masterPage;
            Detail = new ContentPage { Title = "Default content page." };
        }

        /// <summary>
        /// Please provide a view to use as content of the master (swipe out menu). The user should be able to select his desired detail page there.
        /// </summary>
        /// <returns>View to be set as <see cref="MasterContent"/>.</returns>
        protected abstract Page CreateMasterPage(); 

        protected void AddDetailData<TViewModel>(MvvmNanoMasterDetailData data) where TViewModel : MvvmNanoViewModel
        {
            data.ViewModelType = typeof(TViewModel);
            data.ViewModelType = data.ViewModelType;
            MasterDetails.Add(data);

            DetailDataAdded<TViewModel>(data);

            //Check if this is the first window, show it if that is the case.
            if (MasterDetails.Count == 1)
            {
                SetDetail(data);
            }
        }

        /// <summary>
        /// Called when a new detail data item is added. Detail data is already added to <see cref="MasterDetails"/> at this point.
        /// </summary>
        /// <param name="detailData">The added detail data.</param>
        protected virtual void DetailDataAdded<TViewModel>(MvvmNanoMasterDetailData detailData)
        {
            
        }

        /// <summary>
        /// Set the Detail, hide the menu and make sure, that the correct menu entry is selected.
        /// </summary>
        /// <param name="page">The new detail.</param>
        /// <param name="pageData">The detail data associated with the new page, provide if already known. Will get resolved otherwise.</param>
        internal void SetDetail(Page page, MvvmNanoMasterDetailData pageData = null)
        {
            var oldDetailData = _currentDetailData; 

            //Resolve the page data if it is not given
            if (pageData == null)
            {
                pageData = MasterDetails.FirstOrDefault(
                    x => ViewViewModelHelper.ViewNameFromViewModel(x.ViewModelType) == page.GetType().Name);

                if (pageData == null)
                    throw new MvvmNanoException($"There is no detail registered for the page ${page.GetType().Name}.");
            }

            _currentDetailData = pageData;

            //Set the title if the page does not have one
            if (string.IsNullOrEmpty(page.Title))
            { 
                page.Title = pageData.Title;
            }

            //Show the page if it is not already presented
            if (_detail != page)
            {
                _detail = page;
                Detail = new MvvmNanoNavigationPage(page);
                IsPresented = false;
            }
            
            DetailSet(oldDetailData, pageData, page);
        }

        /// <summary>
        /// Called when a new detail data is set as detail page.
        /// </summary>
        /// <param name="lastDetailData">The detail data that was set bevore. Can be null if this is the first time a detail is set.</param>
        /// <param name="newDetailData">The new detail that is set.</param>
        /// <param name="page">The page represented by the detail data that is shown now.</param>
        protected virtual void DetailSet(MvvmNanoMasterDetailData lastDetailData, MvvmNanoMasterDetailData newDetailData, Page page)
        {
            
        }

        /// <summary>
        /// Set the page of <see cref="viewModelType"/> as Detail.
        /// </summary> 
        public void SetDetail(Type viewModelType) 
        {
            var page = CreatePage(viewModelType); 
            SetDetail(page);
        }

        /// <summary>
        /// Opens the page a <see cref="MvvmNanoMasterDetailData.ViewModelType"/> is referencing to.
        /// </summary>
        /// <param name="data"></param>
        public void SetDetail(MvvmNanoMasterDetailData data)
        {
            var page = CreatePage(data.ViewModelType); 
            SetDetail(page, data);
        }

        /// <summary>
        /// Creates the page for the given <see cref="viewModelType"/>.
        /// </summary>
        /// <param name="viewModelType">View model type of the desired page.</param>
        /// <returns></returns>
        private Page CreatePage(Type viewModelType)
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

        /// <summary>
        /// Gets an instance of the given <see cref="viewModelType"/>.
        /// </summary>
        /// <param name="viewModelType">View model type to get an instance of.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets an instance of the given <see cref="TViewModel"/>.
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <returns></returns>
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