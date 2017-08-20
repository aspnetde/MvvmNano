using System.Threading.Tasks;
using MvvmNano;

namespace Demo.ViewModels
{
    public class FirstViewModel : MvvmNanoViewModel
    {
        public MvvmNanoCommand ToPreviousPageCommand => new MvvmNanoCommand(async ()=>await  ToPreviousPage());

        public Task ToPreviousPage()
        {
            return NavigateToAsync<ThirdViewModel>();
        }

        public MvvmNanoCommand ToNextPageCommand => new MvvmNanoCommand(async () => await ToNextPage());

        public Task ToNextPage()
        {
            return NavigateToAsync<SecondViewModel>();
        }
    }
}