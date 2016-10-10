﻿using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MvvmNano.Forms
{
    /// <summary>
    /// The entry point of your XF application
    /// </summary>
    public class MvvmNanoApplication : Application
    {

        /// <summary>
        /// A collection of MasterDetailData that represents available detail pages.
        /// </summary>
        public ObservableCollection<MasterDetailData> MasterDetails { get; set; } = new ObservableCollection<MasterDetailData>();

        /// <summary>
        /// The <see cref="BetterMasterDetailPage"/> if one is set. 
        /// </summary>
        public BetterMasterDetailPage MasterPage => MainPage as BetterMasterDetailPage;

        /// <summary>
        /// Add a site to the <see cref="MasterDetails"/>.
        /// </summary>
        /// <param name="data"><see cref="MasterDetailData"/> with information for the detail site.</param>
        public void AddSiteToDetailPages(MasterDetailData data)
        {
            MasterDetails.Add(data);
        }

        protected override void OnStart()
        {
            base.OnStart();

            SetUpPresenter();
            SetUpMessenger();
        }

        /// <summary>
        /// Registers MvvmNanoFormsPresenter. If you're using your own
        /// custom presenter, override this method for registration (but
        /// don't call base.SetUpPresenter()!).
        /// </summary>
        protected virtual void SetUpPresenter()
        {
            MvvmNanoIoC.RegisterAsSingleton<IPresenter>(new MvvmNanoFormsPresenter(this));
        }

        /// <summary>
        /// Registers MvvmNanoFormsMessenger. If you're using your own
        /// custom messenger, override this method for registration (but
        /// don't call base.SetUpMessenger()!).
        /// </summary>
        protected virtual void SetUpMessenger()
        {
            MvvmNanoIoC.RegisterAsSingleton<IMessenger, MvvmNanoFormsMessenger>();
        }

        /// <summary>
        /// Sets up the main page for the given View Model type.
        /// </summary>
        protected void SetUpMainPage<TViewModel>() where TViewModel : MvvmNanoViewModel
        {
            MainPage = new MvvmNanoNavigationPage(GetPageFor<TViewModel>());
        }

        /// <summary>
        /// Sets up the main page as a <see cref="MvvmNanoMasterDetailPage{TViewModel}"/> for the given view model type.
        /// </summary> 
        public void SetUpMasterDetailPage<TViewModel>() where TViewModel : MvvmNanoViewModel
        {
            this.MainPage = (Page)this.GetMasterDetailPageFor<TViewModel>();
            if (this.MasterDetails.Count <= 0)
                return;
            ((MvvmNanoFormsPresenter)MvvmNanoIoC.Resolve<IPresenter>()).SetDetail(this.MasterDetails[0]);
        }

        /// <summary>
        /// Creates a MvvmNanoContentPage for the given View Model type.
        /// </summary>
        public MvvmNanoContentPage<TViewModel> GetPageFor<TViewModel>() where TViewModel : MvvmNanoViewModel
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>();
            viewModel.Initialize();

            var page = MvvmNanoIoC
                .Resolve<IPresenter>()
                .CreateViewFor<TViewModel>() as MvvmNanoContentPage<TViewModel>;

            if (page == null)
                throw new MvvmNanoException("Could not create a MvvmNanoContentPage for View Model of type " + typeof(TViewModel) + ".");

            page.SetViewModel(viewModel);

            return page;
        }

        /// <summary>
        /// Creates a <see cref="MvvmNanoMasterDetailPage{TViewModel}"/> for the given view model.
        /// </summary> 
        public MvvmNanoMasterDetailPage<TViewModel> GetMasterDetailPageFor<TViewModel>() where TViewModel : MvvmNanoViewModel
        {
            TViewModel viewModel = MvvmNanoIoC.Resolve<TViewModel>();
            viewModel.Initialize();
            MvvmNanoMasterDetailPage<TViewModel> viewFor = MvvmNanoIoC.Resolve<IPresenter>().CreateViewFor<TViewModel>() as MvvmNanoMasterDetailPage<TViewModel>;
            if (viewFor == null)
                throw new MvvmNanoException("Could not create a MvvmNanoMasterDetailPage for View Model of type " + (object)typeof(TViewModel) + ".");
            viewFor.SetViewModel((IViewModel)viewModel);
            return viewFor;
        }
    }
}

