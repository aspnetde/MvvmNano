using System;

namespace MvvmNano
{
    /// <summary>
    /// The view, can be a (Xamarin.Forms) ContentPage, an 
    /// (iOS) UINavigationControler or an (Android) Activity, for example.
    /// </summary>
    public interface IView : IDisposable
    {
        /// <summary>
        /// Called by the Presenter, passing the View Model instance of this View.
        /// </summary>
        /// <param name="viewModel">The View Model.</param>
        void SetViewModel(IViewModel viewModel);
    }
}

