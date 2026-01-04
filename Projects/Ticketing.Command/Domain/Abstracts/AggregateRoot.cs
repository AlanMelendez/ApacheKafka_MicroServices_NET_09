using Common.Core.Events;
using System.Reflection;

namespace Ticketing.Command.Domain.Abstracts
{
    public abstract class AggregateRoot
    {
        protected string _id = string.Empty;

        public string Id { get { return _id; } }
        
        public int Version { get; set; } = 0;
        private readonly List<BaseEvent> _changes = new List<BaseEvent>();


        // Events that've not been stored yet.
        public IEnumerable<BaseEvent> GetUncommittedChanges()
        {
            return _changes;
        }

        //Mark all event as committed when all are stored
        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        public void ApplyChange(BaseEvent @event, bool isNew = true)
        {
            // Use reflection or a mapping strategy to call the appropriate "Apply" method
            // By the class that calls this method, getting the method named "Apply" that takes the event type as parameter
            var method = this.GetType().GetMethod("Apply", new Type[] { @event.GetType() }); // we can send the parameter like this: [@event.GetType()]
            if (method is null)
            {
                throw new ArgumentNullException($"The Apply method was not found in {this.GetType().Name} for event {@event.GetType().Name}");
            }

            method.Invoke(this, new object[] { @event } ); // we can send the parameter like this: [@event]

            if (isNew)
            {
                _changes.Add(@event);
            }
        }


        public void RaiseEvent(BaseEvent @event)
        {
          ApplyChange(@event, true);
        }
    }
}
