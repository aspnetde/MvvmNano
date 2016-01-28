using System;
using Xamarin.Forms;

namespace MvvmNano.Forms
{
    /// <summary>
    /// An implementation of NavigationPage which takes care of 
    /// cleaning up all popped and dismissed Pages. Strongly 
    /// recommended to use it instead of the default NavigationPage.
    /// </summary>
    public class MvvmNanoNavigationPage : NavigationPage
    {
        /// <summary>
        /// We need a root Page, which should be shown when this
        /// new navigation stack opens its doors.
        /// </summary>
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

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (Parent == null)
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

