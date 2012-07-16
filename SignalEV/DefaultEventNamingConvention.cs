using System;

namespace SignalEV
{
    public static class DefaultEventNamingConvention
    {
        /// <summary>
        /// convention for naming the methods being called from server On + EventType
        /// </summary>
        public static string GetEventName(Type eventType)
        {
            return String.Format("On{0}", eventType.Name);
        }
    }
}