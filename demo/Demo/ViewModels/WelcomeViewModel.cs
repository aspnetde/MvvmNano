using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Model;
using MvvmNano;

namespace Demo.ViewModels
{
    public class WelcomeViewModel : MvvmNanoViewModel
    {  
        public List<Club> Clubs { get; private set; }

        public MvvmNanoCommand<Club> ShowClubCommand
        {
            get { return new MvvmNanoCommand<Club>(ShowClub); }
        }

        public MvvmNanoCommand ShowSecondDetailCommand => new MvvmNanoCommand(async ()=> await ShowSecondDetail());

        private Task ShowSecondDetail()
        {
            return NavigateToAsync<SecondViewModel>();
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
