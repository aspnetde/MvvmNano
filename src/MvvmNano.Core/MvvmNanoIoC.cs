using Ninject;

namespace MvvmNano
{
    public static class MvvmNanoIoC
    {
        private static readonly StandardKernel _kernel;

        static MvvmNanoIoC()
        {
            _kernel = new StandardKernel();
        }

        public static void Register<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            _kernel.Bind<TInterface>().To<TImplementation>();
        }

        public static T Resolve<T>()
        {
            return _kernel.Get<T>();
        }
    }
}

