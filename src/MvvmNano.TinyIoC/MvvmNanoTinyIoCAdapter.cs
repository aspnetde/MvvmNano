using TinyIoC;

namespace MvvmNano.TinyIoC
{
    /// <summary>
    /// MvvmNano IoC adapter that is using TinyIoC. 
    /// </summary>
    public class MvvmNanoTinyIoCAdapter : IMvvmNanoIoCAdapter
    {
        private readonly TinyIoCContainer _container;

        public MvvmNanoTinyIoCAdapter()
        {
            _container = new TinyIoCContainer();
        }

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _container.Register<TInterface, TImplementation>();
        }

        public void RegisterAsSingleton<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _container.Register<TInterface, TImplementation>().AsSingleton();
        }

        public void RegisterAsSingleton<TInterface>(TInterface instance)
        {
            _container.Register<TInterface>(instance);
        }

        public TInterface Resolve<TInterface>()
        {
            return _container.Resolve<TInterface>();
        }
    }
}
