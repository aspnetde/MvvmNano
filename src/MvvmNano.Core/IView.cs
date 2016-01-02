using System;

namespace MvvmNano
{
    public interface IView : IDisposable
    {
        void SetViewModel(IViewModel viewModel);
    }
}

