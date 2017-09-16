using Demo.ViewModels;
using MvvmNano.Forms;
using MvvmNano.Forms.MasterDetail;
using Xamarin.Forms;

namespace Demo.Pages
{
    public class MasterPage : MvvmNanoDefaultMasterDetailPage<MasterViewModel>
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
            AddDetailData<WelcomeViewModel>(new CustomMasterDetailData("Welcome", Color.Red));
            AddDetailData<FirstViewModel>(new CustomMasterDetailData("First Example Detail", Color.Orange));
            AddDetailData<SecondViewModel>(new CustomMasterDetailData("Second Example Detail", Color.Yellow));
            AddDetailData<ThirdViewModel>(new CustomMasterDetailData("Third Example Detail", Color.Green)); 
            AddDetailData<TabbedViewModel>(new CustomMasterDetailData("Tab Detail", Color.Blue));
        }

        protected override Page CreateMasterPage()
        {
            DetailListView.Header = _usernameLabel;
            DetailListView.Footer = _logoutButton;
            return base.CreateMasterPage(); 
        }

        /// <summary>
        /// We hook up our own item template here.
        /// Our item template will add the background color from our <see cref="CustomMasterDetailData"/> to each cell.
        /// </summary>
        /// <returns></returns>
        protected override DataTemplate GetItemTemplate()
        {
            return new DataTemplate(() =>
            {
                Label titleLabel = new Label
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                titleLabel.SetBinding(Label.TextProperty, nameof(CustomMasterDetailData.Title)); 
                titleLabel.SetBinding(Label.TextColorProperty, nameof(CustomMasterDetailData.TextColor));

                return new ViewCell
                {
                    View = titleLabel
                };
            });
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
        }
    }
}
