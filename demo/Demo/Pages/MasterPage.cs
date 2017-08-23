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
            AddDetailData<WelcomeViewModel>(new MvvmNanoMasterDetailData("Welcome"));
            AddDetailData<FirstViewModel>(new MvvmNanoMasterDetailData("First Example Detail"));
            AddDetailData<SecondViewModel>(new MvvmNanoMasterDetailData("Second Example Detail"));
            AddDetailData<ThirdViewModel>(new MvvmNanoMasterDetailData("Third Example Detail")); 
            AddDetailData<TabbedViewModel>(new MvvmNanoMasterDetailData("Tab Detail"));
        }

        protected override Page CreateMasterPage()
        {
            var page = base.CreateMasterPage() as ContentPage;
            var listView = page.Content as ListView;
            listView.Header = _usernameLabel;
            listView.Footer = _logoutButton;
            return page;
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
