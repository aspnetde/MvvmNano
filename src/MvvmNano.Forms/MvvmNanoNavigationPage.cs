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

            DisposePage(CurrentPage as IDisposable);
        }

        private static void PagePopped(object sender, NavigationEventArgs e)
        {
            DisposePage(e.Page as IDisposable);
        }

        private static void DisposePage(IDisposable page)
        {
            if (page == null)
                return;

            page.Dispose();
        }
    }
}

