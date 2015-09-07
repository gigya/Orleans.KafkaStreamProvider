using System;
using System.Collections.Generic;

namespace TestGrains.Models
{
    [Serializable]
    public class LoadTestMessage
    {
        private const uint MaxSiteId = 10;
        private static readonly List<string> EventNames = new List<string>() { "AccountManagementEvent", "PotatoEvent", "OtherEvent" };


        public ulong SiteId { get; set; }
        public string CallbackUrl { get; set; }
        public Guid UUID { get; set; }
        public string EventName { get; set; }
        public DateTime SentTime { get; set; }

        public static LoadTestMessage GenerateTestMessage()
        {
            Random r = new Random(0);
            var siteId = r.Next(0, (int)MaxSiteId);
            var callbackUrl = "http://localhost/";
            var uuid = Guid.NewGuid();
            var eventName = EventNames[r.Next(0, EventNames.Count - 1)];

            return new LoadTestMessage { CallbackUrl = callbackUrl, SiteId = (uint)siteId, EventName = eventName, UUID = uuid };
        }
    }
}
