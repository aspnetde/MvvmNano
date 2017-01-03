using MvvmNano;
using MvvmNanoDemo.Data;

namespace MvvmNanoDemo.ViewModels
{
    public class MasterViewModel : MvvmNanoViewModel
    {
        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyPropertyChanged(nameof(Username));
            }
        }

        public MvvmNanoCommand LogoutCommand
        {
            get { return new MvvmNanoCommand(Logout);}
        }

        private void Logout()
        {
            MvvmNanoIoC.Resolve<IPresenter>().ChangeRootViewModel<LoginViewModel>();
        }

        public MasterViewModel()
        {
            Username = MvvmNanoIoC.Resolve<IUserData>().User.Name;
        }
    }
}
