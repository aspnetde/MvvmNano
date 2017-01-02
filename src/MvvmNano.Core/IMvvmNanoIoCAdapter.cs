namespace MvvmNano
{
    public interface IMvvmNanoIoCAdapter
    {
        void Register<TInterface, TImplementation>() where TImplementation : TInterface;
        void RegisterAsSingleton<TInterface, TImplementation>() where TImplementation : TInterface;
        void RegisterAsSingleton<TInterface>(TInterface instance);

        TInterface Resolve<TInterface>();
    }
}
