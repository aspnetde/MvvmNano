using System;

namespace MvvmNano
{
    public interface IMessenger
    {
        void Send<TMessage>(TMessage message, object sender = null)
            where TMessage : IMessage;

        void Subscribe<TMessage>(object subscriber, Action<object, TMessage> callback)
            where TMessage : IMessage;

        void Unsubscribe<TMessage>(object subscriber)
            where TMessage : IMessage;
    }
}

