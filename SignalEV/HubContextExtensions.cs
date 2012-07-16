using System;
using SignalR.Hubs;

namespace SignalEV
{
    public static class HubContextExtensions
    {
        static HubContextExtensions()
        {
            EventName = DefaultEventNamingConvention.GetEventName;
        }
        public static Func<Type, string> EventName { get; set; }

        public static void PublishEvent<TEvent>(this IHubContext hubContext, TEvent @event)
        {
            var clients = (ClientAgent)hubContext.Clients;
            clients.Invoke(EventName(typeof(TEvent)), new object[] { @event });
        }
    }
}
