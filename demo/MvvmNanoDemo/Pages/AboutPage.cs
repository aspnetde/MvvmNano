using MvvmNano.Forms;
using Xamarin.Forms;
using System;

namespace MvvmNanoDemo
{
    public class AboutPage : MvvmNanoContentPage<AboutViewModel>
    {
        private readonly ToolbarItem _doneButtpn;

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

            _doneButtpn = new ToolbarItem();
            _doneButtpn.Text = "Done";
            _doneButtpn.Clicked += Done;

            ToolbarItems.Add(_doneButtpn);
        }

        private void Done(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => 
                await Navigation.PopModalAsync()
            );
        }

        public override void Dispose()
        {
            base.Dispose();

            if (_doneButtpn != null)
                _doneButtpn.Clicked -= Done;
        }
    }
}

