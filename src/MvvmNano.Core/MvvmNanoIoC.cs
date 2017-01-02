using Ninject;

namespace MvvmNano
{
    /// <summary>
    /// A simple Service Locator, which is mainly used to enable
    /// Dependency Injection within the View Models, so they can
    /// be built in a testable fashion.
    /// 
    /// Static and unique within the whole application.
    /// </summary>
    public static class MvvmNanoIoC
    {
        private static readonly StandardKernel _kernel;

        static MvvmNanoIoC()
        {
            _kernel = new StandardKernel();
        }

        /// <summary>
        /// Registers an Interface and the Implementation type which should be used
        /// at runtime for this Interface when Resolve<TInterface>() is called.
        /// </summary>
        public static void Register<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            _kernel.Unbind<TInterface>();
            _kernel.Bind<TInterface>().To<TImplementation>();
        }

        /// <summary>
        /// Registers an Interface and the Implementation type which should be used
        /// at runtime for this Interface when Resolve<TInterface>() is called.
        /// </summary>
        public static void RegisterAsSingleton<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            _kernel.Unbind<TInterface>();
            _kernel.Bind<TInterface>().To<TImplementation>().InSingletonScope();
        }

        /// <summary>
        /// Registers and Interface and a concrete instance implementing this
        /// interface, so the instance is not created when resolving the Interface
        /// but it is passed this concrete instance back.
        /// </summary>
        public static void RegisterAsSingleton<TInterface>(TInterface instance)
        {
            _kernel.Unbind<TInterface>();
            _kernel.Bind<TInterface>().ToConstant(instance);
        }

        /// <summary>
        /// Resolves the implemenation of the Interface, if properly registered before.
        /// </summary>
        public static TInterface Resolve<TInterface>()
        {
            return _kernel.Get<TInterface>();
        }
    }
}

