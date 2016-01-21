/*****************************************************************************
 * Copyright (c) Thomas Bandt (https://thomasbandt.com). Licensed under the 
 * MIT License. See LICENSE file in the project root for detailed information. 
 ****************************************************************************/
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
        /// <typeparam name="TInterface">The type of the interface, for example IUserRepository.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation, for example SqliteUserRepository.</typeparam>
        public static void Register<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            _kernel.Bind<TInterface>().To<TImplementation>();
        }

        /// <summary>
        /// Registers an Interface and the Implementation type which should be used
        /// at runtime for this Interface when Resolve<TInterface>() is called.
        /// 
        /// The Implementation is only crated once and then being held in memory
        /// for the lifetime of this application.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface, for example IUserRepository.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation, for example SqliteUserRepository.</typeparam>
        public static void RegisterAsSingleton<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            _kernel.Bind<TInterface>().To<TImplementation>().InSingletonScope();
        }

        /// <summary>
        /// Registers and Interface and a concrete instance implementing this
        /// interface, so the instance is not created when resolving the Interface
        /// but it is passed this concrete instance back.
        /// </summary>
        /// <param name="instance">The concrete instance.</param>
        /// <typeparam name="TInterface">The type of the Interface.</typeparam>
        public static void RegisterConcreteInstance<TInterface>(TInterface instance)
        {
            _kernel.Bind<TInterface>().ToConstant(instance);
        }

        /// <summary>
        /// Resolves the implemenation of the Interface, if properly registered before.
        /// </summary>
        /// <typeparam name="TInterface">The type of the Interface.</typeparam>
        public static TInterface Resolve<TInterface>()
        {
            return _kernel.Get<TInterface>();
        }
    }
}

