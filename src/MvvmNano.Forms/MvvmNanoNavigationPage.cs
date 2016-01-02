using System;
using Xamarin.Forms;

namespace MvvmNano.Forms
{
    public class MvvmNanoNavigationPage : NavigationPage
    {
        public MvvmNanoNavigationPage(Page root) : base(root)
        {
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Popped += PagePopped;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Popped -= PagePopped;
        }

        private static void PagePopped(object sender, NavigationEventArgs e)
        {
            var disposablePage = e.Page as IDisposable;

            if (disposablePage != null)
                disposablePage.Dispose();
        }
    }
}

