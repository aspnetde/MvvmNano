using MvvmNano;

namespace MvvmNanoDemo.ViewModels
{
    public class AboutViewModel : MvvmNanoViewModel
    {
        public MvvmNanoCommand NavigateBackCommand => new MvvmNanoCommand(()=> NavigateTo<LoginViewModel>());
    }
}

