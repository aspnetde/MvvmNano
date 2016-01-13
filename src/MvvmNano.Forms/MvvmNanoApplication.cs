using Xamarin.Forms;

namespace MvvmNano.Forms
{
    public class MvvmNanoApplication : Application
    {
        protected override void OnStart()
        {
            base.OnStart();

            SetUpPresenter();
        }

        protected virtual void SetUpPresenter()
        {
            MvvmNanoIoC.RegisterConcreteInstance<IPresenter>(
                new MvvmNanoFormsPresenter(this)
            );
        }
    }
}

