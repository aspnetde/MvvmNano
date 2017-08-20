using System.Threading.Tasks;
using MvvmNano;

namespace Demo.ViewModels
{
    public class SecondViewModel : MvvmNanoViewModel
    {
        public MvvmNanoCommand ToPreviousPageCommand => new MvvmNanoCommand(async () => await ToPreviousPage());

        public Task ToPreviousPage()
        {
            return NavigateToAsync<FirstViewModel>();
        }

        public MvvmNanoCommand ToNextPageCommand => new MvvmNanoCommand(async () => await ToNextPage());

        public Task ToNextPage()
        {
            return NavigateToAsync<ThirdViewModel>();
        }
    }
}