using Ninject;

namespace MvvmNano.Ninject
{
    public class MvvmNanoNinjectAdapter : IMvvmNanoIoCAdapter
    {
        private readonly StandardKernel _kernel;

        public MvvmNanoNinjectAdapter()
        {
            _kernel = new StandardKernel();
        }

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _kernel.Unbind<TInterface>();
            _kernel.Bind<TInterface>().To<TImplementation>();
        }

        public void RegisterAsSingleton<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _kernel.Unbind<TInterface>();
            _kernel.Bind<TInterface>().To<TImplementation>().InSingletonScope();
        }

        public void RegisterAsSingleton<TInterface>(TInterface instance)
        {
            _kernel.Unbind<TInterface>();
            _kernel.Bind<TInterface>().ToConstant(instance);
        }

        public TInterface Resolve<TInterface>()
        {
            return _kernel.Get<TInterface>();
        }
    }
}
