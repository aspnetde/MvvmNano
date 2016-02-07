using MvvmNano.Forms;
using Xamarin.Forms;
using System;
using System.Globalization;

namespace MvvmNanoDemo
{
    public class WelcomePage : MvvmNanoContentPage<WelcomeViewModel>
    {
        private ListView _clubList;

        public WelcomePage()
        {
            BindToViewModel(
                this, 
                Page.TitleProperty, 
                x => x.Username,
                converter: new TitleConverter()
            );

            NavigationPage.SetBackButtonTitle(this, string.Empty);
        }

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

            _clubList.SelectedItem = null;
            ViewModel.SelectClubCommand.Execute(e.SelectedItem);
        }

        public override void Dispose()
        {
            base.Dispose();

            _clubList.ItemSelected -= ClubSelected;
        }

        private class TitleConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return string.Format("Hi {0}!", value);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}

