using MvvmNano;

namespace Demo.ViewModels
{
    public class AboutViewModel : MvvmNanoViewModel
    {
        public MvvmNanoCommand NavigateBackCommand => new MvvmNanoCommand(()=> NavigateTo<LoginViewModel>());
    }
}

