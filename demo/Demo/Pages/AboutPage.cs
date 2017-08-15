using Demo.ViewModels;
using MvvmNano.Forms;
using Xamarin.Forms;

namespace Demo.Pages
{
    public class AboutPage : MvvmNanoContentPage<AboutViewModel>
    {
        public AboutPage()
        {
            Title = "About this App";
            Padding = 10;

            var backButton = new Button
            {
                Text = "Navigate back!",
                VerticalOptions = LayoutOptions.End
            };

            BindToViewModel(backButton,
                Button.CommandProperty,
                vm => vm.NavigateBackCommand);

            Content = new Grid()
            {
                RowDefinitions =
                {
                    new RowDefinition{ Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition{ Height = new GridLength(1, GridUnitType.Auto)} 
                },
                Children = 
                {
                    new Label
                    {
                        Text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet."
                    },
                    { backButton, 0, 1}
                }
            };
        }
    }
}

