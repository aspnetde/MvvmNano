using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MvvmNano.Forms
{
    /// <summary>
    /// The entry point of your XF application
    /// </summary>
    public class MvvmNanoApplication : Application
    {
        public ObservableCollection<MasterDetailData> MasterDetails { get; set; } = new ObservableCollection<MasterDetailData>();

        public BetterMasterDetailPage MasterPage
        {
            get
            {
                return this.MainPage as BetterMasterDetailPage;
            }
        }

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

