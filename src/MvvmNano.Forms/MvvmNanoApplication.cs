using Xamarin.Forms;
using MvvmNano;

namespace MvvmNano.Forms
{
    public class MvvmNanoApplication : Application
    {
        // TODO: That should be moved somewhere else, 
        // not living in the constructor of the app
        public MvvmNanoApplication()
        {
            // Workaround
            MvvmNanoViewModel.Presenter = new FormsPresenter(Current.MainPage.Navigation);
        }
    }
}

