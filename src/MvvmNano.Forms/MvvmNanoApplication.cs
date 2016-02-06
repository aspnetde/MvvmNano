using Xamarin.Forms;

namespace MvvmNano.Forms
{
    /// <summary>
    /// The entry point of your XF application
    /// </summary>
    public class MvvmNanoApplication : Application
    {
        protected override void OnStart()
        {
            base.OnStart();

            SetUpPresenter();
        }

        /// <summary>
        /// Registers MvvmNanoFormsPresenter. If you're using your own
        /// custom presenter, override this method for registration (but
        /// don't call base.SetUpPresenter()!).
        /// </summary>
        protected virtual void SetUpPresenter()
        {
            MvvmNanoIoC.RegisterAsSingleton<IPresenter>(
                new MvvmNanoFormsPresenter(this)
            );
        }
    }
}

