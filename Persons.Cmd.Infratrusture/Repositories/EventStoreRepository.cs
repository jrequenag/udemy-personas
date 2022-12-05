﻿using CQRS.Core.Domain;
using CQRS.Core.Events;

using Microsoft.Extensions.Options;

using MongoDB.Driver;

using Persons.Cmd.Infratrusture.Config;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons.Cmd.Infratrusture.Repositories;
public class EventStoreRepository : IEventStoreRepository {
    private readonly IMongoCollection<EventModel> _eventStoreCollection;
    public EventStoreRepository(IOptions<MongoDbConfig> config) {
        var mongoClient = new MongoClient(config.Value.ConnectionString);
        var mongoDataBase = mongoClient.GetDatabase(config.Value.DataBase);
        _eventStoreCollection = mongoDataBase.GetCollection<EventModel>(config.Value.Collection);

    }
    public async Task<List<EventModel>> FindAllAsync() {
        throw new NotImplementedException();
    }

    public async Task<List<EventModel>> FindByAggregateId(Guid aggregateId) {
        return await _eventStoreCollection.Find(x => x.AggregateIdentifier == aggregateId).ToListAsync().ConfigureAwait(false);

    }

    public async Task SaveAsync(EventModel @event) {
        await _eventStoreCollection.InsertOneAsync(@event).ConfigureAwait(false);
    }
}
