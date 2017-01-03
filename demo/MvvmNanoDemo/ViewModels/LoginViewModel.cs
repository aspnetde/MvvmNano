using MvvmNano;
using MvvmNano.Forms;
using MvvmNanoDemo.Data;
using MvvmNanoDemo.ViewModels;
using Xamarin.Forms;

namespace MvvmNanoDemo
{
    public class LoginViewModel : MvvmNanoViewModel
    {
        private string _username;
        private string _password;

        public string Username 
        {
            get { return _username; }
            set 
            { 
                _username = value; 
                NotifyPropertyChanged(); 
                NotifyPropertyChanged("IsFormValid"); 
            }
        }
         
        public string Password 
        {
            get { return _password; }
            set 
            { 
                _password = value; 
                NotifyPropertyChanged(); 
                NotifyPropertyChanged("IsFormValid"); 
            }
        }

        public bool IsFormValid
        {
            get { return !string.IsNullOrWhiteSpace(_username) && !string.IsNullOrWhiteSpace(_password); }
        }

        public MvvmNanoCommand LogInCommand
        {
            get { return new MvvmNanoCommand(LogIn); }
        }

        public MvvmNanoCommand ShowAboutCommand
        {
            get { return new MvvmNanoCommand(NavigateTo<AboutViewModel>); }
        }

        private void LogIn()
        {
            if (!IsFormValid)
                return;

            MvvmNanoIoC.Resolve<IUserData>().User = new User(Username);

            MvvmNanoIoC.Resolve<IPresenter>().ChangeRootViewModel<MasterViewModel>();
        }
    }
}
