using MvvmNano;
using System.Collections.Generic;

namespace MvvmNanoDemo
{
    public class WelcomeViewModel : MvvmNanoViewModel<User>
    {
        private string _username;
        public string Username 
        {
            get { return _username; }
            private set { _username = value; NotifyPropertyChanged(); }
        }

        public List<Club> Clubs { get; private set; }

        public MvvmNanoCommand<Club> ShowClubCommand
        {
            get { return new MvvmNanoCommand<Club>(ShowClub); }
        }

        public WelcomeViewModel(IClubRepository clubs)
        {
            Clubs = clubs.All();
        }

        public override void Initialize(User parameter)
        {
            Username = parameter.Name;
        }

        private async void ShowClub(Club club)
        {
            await NavigateToAsync<ClubViewModel, Club>(club);
        }
    }
}

