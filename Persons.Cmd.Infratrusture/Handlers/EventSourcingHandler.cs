using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;

using Persons.Cmd.Domain.Aggregates;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons.Cmd.Infratrusture.Handlers;
public class EventSourcingHandler : IEventSourcingHandler<PersonAggregate> {
    private readonly IEventStore _eventStore;

    public EventSourcingHandler(IEventStore eventStore) {
        _eventStore = eventStore;
    }
    public async Task<PersonAggregate> GetByIdAsync(Guid aggregateId) {
        var aggregate = new PersonAggregate();
        var events = await _eventStore.GetEventsAsync(aggregateId);

        if(events == null || !events.Any()) return aggregate;

        aggregate.ReplayEvents(events);
        aggregate.Version = events.Select(e => e.Version).Max();

        return aggregate;
    }

    public async Task SaveAsync(AggregateRoot aggregateRoot) {
        await _eventStore.SaveEvenrtAsync(aggregateRoot.Id, aggregateRoot.GetUIncommitedChanges(), aggregateRoot.Version);
        aggregateRoot.MarkChangedAsCommited();
    }
}
