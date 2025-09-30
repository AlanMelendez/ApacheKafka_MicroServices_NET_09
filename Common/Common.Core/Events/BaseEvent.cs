
using Common.Core.Messages;

namespace Common.Core.Events
{
    public abstract class BaseEvent : Message
    {
        protected BaseEvent(string typeEvent) { 
        
            Type = typeEvent;
        }

        public int Version { get; set; }
        public string Type { get; set; } 
    }
}
