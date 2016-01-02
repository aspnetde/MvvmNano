using Xamarin.Forms;
using MvvmNano;

namespace MvvmNano.Forms
{
    public abstract class MvvmNanoApplication : Application
    {
        protected void SetUpFormsPresenter()
        {
            MvvmNanoViewModel.Presenter = new MvvmNanoFormsPresenter(this);
        }
    }
}

