using MvvmNano;
using System.Collections.Generic;

namespace MvvmNanoDemo
{
    public class WelcomeViewModel : MvvmNanoViewModel
    {  
        public List<Club> Clubs { get; private set; }

        public MvvmNanoCommand<Club> ShowClubCommand
        {
            get { return new MvvmNanoCommand<Club>(ShowClub); }
        }

        public WelcomeViewModel(IClubRepository clubs)
        {
            Clubs = clubs.All();
        } 

        private async void ShowClub(Club club)
        {
            await NavigateToAsync<ClubViewModel, Club>(club);
        }
    }
}

