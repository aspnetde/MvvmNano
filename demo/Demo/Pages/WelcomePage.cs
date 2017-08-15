using Demo.ViewModels;
using MvvmNano.Forms;
using Xamarin.Forms;

namespace Demo.Pages
{
    public class WelcomePage : MvvmNanoContentPage<WelcomeViewModel>
    {
        private ListView _clubList; 

        public override void OnViewModelSet()
        {
            base.OnViewModelSet();

            _clubList = new ListView
            {
                ItemsSource = ViewModel.Clubs,
                ItemTemplate = new DataTemplate(typeof(ClubCell))
            };

            _clubList.ItemSelected += ClubSelected;

            Content = new StackLayout
            {
                Children = 
                {
                    _clubList
                }
            };
        }

        private void ClubSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            ViewModel.ShowClubCommand.Execute(e.SelectedItem);
            _clubList.SelectedItem = null; 
        }

        public override void Dispose()
        {
            base.Dispose();

            _clubList.ItemSelected -= ClubSelected;
        } 
    }
}

