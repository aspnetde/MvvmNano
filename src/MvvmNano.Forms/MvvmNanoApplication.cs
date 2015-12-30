using Xamarin.Forms;
using MvvmNano.Core;

namespace MvvmNano.Forms
{
    public class MvvmNanoApplication : Application
    {
        public MvvmNanoApplication()
        {
            // Workaround
            MvvmNanoViewModel.Presenter = new FormsPresenter(Current.MainPage.Navigation);
        }
    }
}

