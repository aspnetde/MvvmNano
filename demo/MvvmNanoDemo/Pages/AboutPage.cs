using MvvmNano.Forms;
using Xamarin.Forms;
using System;

namespace MvvmNanoDemo
{
    public class AboutPage : MvvmNanoContentPage<AboutViewModel>
    {
        private readonly ToolbarItem _doneButton;

        public AboutPage()
        {
            Title = "About this App";
            Padding = 10;

            Content = new StackLayout
            {
                Children = 
                {
                    new Label
                    {
                        Text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet."
                    }
                }
            };

            _doneButton = new ToolbarItem();
            _doneButton.Text = "Done";
            _doneButton.Clicked += Done;

            ToolbarItems.Add(_doneButton);
        }

        private async void Done(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public override void Dispose()
        {
            base.Dispose();

            if (_doneButton != null)
            {
                _doneButton.Clicked -= Done;
            }
        }
    }
}

