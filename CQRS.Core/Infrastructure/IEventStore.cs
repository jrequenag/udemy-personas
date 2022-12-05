using CQRS.Core.Events;




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Infrastructure;
public interface IEventStore {
    Task SaveEvenrtAsync(Guid aggregateId, IEnumerable<BaseEvent> evenest, int expectedVersion);
    Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId);
}
