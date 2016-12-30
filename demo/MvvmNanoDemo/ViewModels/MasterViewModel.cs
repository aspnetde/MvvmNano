using System;
using MvvmNano;
using MvvmNano.Forms;
using Xamarin.Forms;

namespace MvvmNanoDemo
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
            ((MvvmNanoApplication)Application.Current).SetUpMainPage<LoginViewModel>();
        }
    }
}