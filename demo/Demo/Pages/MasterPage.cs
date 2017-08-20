using Demo.ViewModels;
using MvvmNano.Forms;
using Xamarin.Forms;

namespace Demo.Pages
{
    public class MasterPage : MvvmNanoMasterDetailPage<MasterViewModel>
    {
        private Label _usernameLabel = new Label
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Margin = 10,
            FontSize = 40
        };

        private Button _logoutButton = new Button
        {
            Text = "Logout"
        };

        public MasterPage()
        {
            AddDetailData(new MvvmNanoMasterDetailData<WelcomeViewModel>("Welcome"));
            AddDetailData(new MvvmNanoMasterDetailData<FirstViewModel>("First Example Detail"));
            AddDetailData(new MvvmNanoMasterDetailData<SecondViewModel>("Second Example Detail"));
            AddDetailData(new MvvmNanoMasterDetailData<ThirdViewModel>("Third Example Detail")); 
            AddDetailData(new MvvmNanoMasterDetailData<TabbedViewModel>("Tab Detail"));
        }

        public override void OnViewModelSet()
        {
            base.OnViewModelSet();

            BindToViewModel(_usernameLabel,
                Label.TextProperty,
                model => model.Username,
                stringFormat: "Hello, {0}!"
            );

            BindToViewModel(_logoutButton,
                Button.CommandProperty,
                model => model.LogoutCommand
            );

            MasterContent = new Grid
            {
                Padding = new Thickness(0, 20, 0, 0),
                RowDefinitions =
                {
                    new RowDefinition{Height = new GridLength(1, GridUnitType.Auto)},
                    new RowDefinition{Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition{Height = new GridLength(1, GridUnitType.Auto)}
                },
                Children =
                {
                    _usernameLabel,
                    { DetailListView, 0, 1 },
                    { _logoutButton, 0, 2 }
                }
            };
        }
    }
}
