using System;
using System.Linq;
using System.Reflection;
using SignalR.Client.Hubs;

namespace SignalEV
{
    public static class HubProxyExtensions
    {
        static HubProxyExtensions()
        {
            EventName = DefaultEventNamingConvention.GetEventName;
        }

        public static Func<Type, string> EventName { get; set; }
        /// <summary>
        /// Registers a generic method to handle all incoming events for given types from the Hub.
        /// </summary>
        /// <param name="eventTypes">The types of events to handle</param>
        /// <param name="instance">the instance on which to invoke <paramref name="handlerInfo"/>. in other words instance of the class taht defines the handler</param>
        /// <param name="handlerInfo">Method info for the generic event that is to handle the events. signature void(event).</param>
        //TODO could i make the MethodInfo parameter an expression pointing to the method
        public static void RegisterGenericHandler(this IHubProxy hubProxy, Type[] eventTypes, object instance, MethodInfo handlerInfo)
        {
            foreach (var eventType in eventTypes)
            {
                var eventName = EventName(eventType);

                //get the On<TEvent>(string, Action<TEvent>) method for "subscribing" to events coming from the server
                var onMethod = typeof(SignalR.Client.Hubs.HubProxyExtensions).GetMethods()
                    .SingleOrDefault(x => x.Name == "On" && x.IsGenericMethod && x.GetParameters().Length == 3 && x.GetGenericArguments().Length == 1);
                if (onMethod == null)
                    throw new Exception("Unable to find the On method on HubProxy(Extensions)");

                var genericPublishMethod = handlerInfo.MakeGenericMethod(eventType);

                //create a delegate to invoke from On() method. as far as i know there is no other way
                var delegateType = typeof(Action<>).MakeGenericType(eventType);
                var handlerDelegate = Delegate.CreateDelegate(delegateType, instance, genericPublishMethod);

                var genericMethod = onMethod.MakeGenericMethod(eventType);
                //invoke/register
                genericMethod.Invoke(hubProxy, new object[] { hubProxy, eventName, handlerDelegate });
            }
        }
    }
}
