using Person.Common.Events;

namespace Persons.Query.Infrastructure.Handlers;
public interface IEventHandler {
    Task On(PersonCreatedEvent @event);
    Task On(PersonUpdatedEvent @event);
    Task On(PersonDeletedEvent @event);
    Task On(IdentityDocumentUpdateEvent @event);
    Task On(IdentityDocumentRemovedEvent @event);
    Task On(IdentityDocumentAddedEvent @event);


}