namespace MvvmNano
{
    /// <summary>
    /// A simple Service Locator, which is mainly used to enable
    /// Dependency Injection within the View Models, so they can
    /// be built in a testable fashion.
    /// 
    /// Static and unique within the whole application. SetUp()
    /// needs to be called at app startup!
    /// </summary>
    public static class MvvmNanoIoC
    {
        private static IMvvmNanoIoCAdapter _adapter;

        private static IMvvmNanoIoCAdapter Adapter
        {
            get
            {
                if (_adapter == null)
                {
                    throw new MvvmNanoException("Please Call MvvmNanoIoC.SetUp() first, before using MvvmNanoIoC.");
                }

                return _adapter;
            }
        }

        /// <summary>
        /// Provides the IoC Container implementation, for example MvvmNano.Ninject
        /// </summary>
        public static void SetUp(IMvvmNanoIoCAdapter adapter)
        {
            _adapter = adapter;
        }

        /// <summary>
        /// Registers an Interface and the Implementation type which should be used
        /// at runtime for this Interface when Resolve() is being called.
        /// </summary>
        public static void Register<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            Adapter.Register<TInterface, TImplementation>();
        }

        /// <summary>
        /// Registers an Interface and the Implementation type which should be used
        /// at runtime for this Interface when Resolve() is being called.
        /// </summary>
        public static void RegisterAsSingleton<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            Adapter.RegisterAsSingleton<TInterface, TImplementation>();
        }

        /// <summary>
        /// Registers and Interface and a concrete instance implementing this
        /// interface, so the instance is not created when resolving the Interface
        /// but it is passed this concrete instance back.
        /// </summary>
        public static void RegisterAsSingleton<TInterface>(TInterface instance)
        {
            Adapter.RegisterAsSingleton(instance);
        }

        /// <summary>
        /// Resolves the implemenation of the Interface, if properly registered before.
        /// </summary>
        public static TInterface Resolve<TInterface>()
        {
            return Adapter.Resolve<TInterface>();
        }
    }
}

