using System;
using Xamarin.Forms;

namespace MvvmNano.Forms
{
    public class MvvmNanoFormsMessenger : IMessenger
    {
        public void Send<TMessage>(TMessage message, object sender = null) where TMessage : IMessage
        {
            MessagingCenter.Send(sender ?? new object(), typeof(TMessage).FullName, message);
        }

        public void Subscribe<TMessage>(object subscriber, Action<object, TMessage> callback) where TMessage : IMessage
        {
            MessagingCenter.Subscribe(subscriber, typeof(TMessage).FullName, callback, null);
        }

        public void Unsubscribe<TMessage>(object subscriber) where TMessage : IMessage
        {
            MessagingCenter.Unsubscribe<object, TMessage>(subscriber, typeof(TMessage).FullName);
        }
    }
}

