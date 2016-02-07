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

        public List<Club> Clubs = new List<Club>
        {
            new Club("FC Bayern München", "Germany"),
            new Club("Borussia Dortmund", "Germany"),
            new Club("Real Madrid", "Spain"),
            new Club("FC Barcelona", "Spain"),
            new Club("Manchester United", "England")
        };

        public MvvmNanoCommand<Club> SelectClubCommand
        {
            get { return new MvvmNanoCommand<Club>(SelectClub); }
        }

        public override void Initialize(User parameter)
        {
            Username = parameter.Name;
        }

        private void SelectClub(Club club)
        {
            NavigateTo<ClubViewModel, Club>(club);
        }
    }
}

