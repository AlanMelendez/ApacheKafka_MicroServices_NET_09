using Common.Core.Events;

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
    }
}
