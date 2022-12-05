﻿using CQRS.Core.Domain;
using CQRS.Core.Events;
using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;

using Persons.Cmd.Domain.Aggregates;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons.Cmd.Infratrusture.Stores;
public class EventStore : IEventStore {
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly IEventProducer _eventProducer;

    public EventStore(IEventStoreRepository eventStoreRepository
         , IEventProducer eventProducer) {
        _eventStoreRepository = eventStoreRepository;
        _eventProducer = eventProducer;
    }
    public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId) {
        var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);

        if(eventStream == null || !eventStream.Any())
            throw new AggregateNotFoundException("Incorrect aggregateId");

        return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
    }

    public async Task SaveEvenrtAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion) {
        var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);
        if(expectedVersion != 1 && eventStream[^1].Version != expectedVersion)
            throw new ConcurrencyException();

        var version = expectedVersion;
        foreach(var @event in events) {
            version++;
            @event.Version = version;
            var eventType = @event.GetType().Name;
            var eventModel = new EventModel() {
                TimeStamp = DateTime.UtcNow,
                AggregateIdentifier = aggregateId,
                AggregateType = nameof(PersonAggregate),
                EventData = @event
            };

            await _eventStoreRepository.SaveAsync(eventModel);

            var topic = Environment.GetEnvironmentVariable("RabbitMq_Topic");
            await _eventProducer.ProducerAsync(topic, @event);

        }
    }
}
