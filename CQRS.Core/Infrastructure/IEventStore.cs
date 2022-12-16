using CQRS.Core.Events;

namespace CQRS.Core.Infrastructure;
public interface IEventStore {
    Task SaveEvenrtAsync(Guid aggregateId, IEnumerable<BaseEvent> evenest, int expectedVersion);
    Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId);
}
