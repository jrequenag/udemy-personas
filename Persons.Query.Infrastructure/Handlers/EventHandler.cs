using Person.Common.Events;

using Persons.Query.Domain.Entities;
using Persons.Query.Domain.Repositories;

namespace Persons.Query.Infrastructure.Handlers;
public class EventHandler : IEventHandler {
    private readonly IPersonRepository _personRepository;
    private readonly IDocumentIdentityRepository _documentIdentityRepository;

    public EventHandler(IPersonRepository personRepository
        , IDocumentIdentityRepository documentIdentityRepository) {
        _personRepository = personRepository;
        _documentIdentityRepository = documentIdentityRepository;
    }
    public async Task On(PersonCreatedEvent @event) {
        var person = new PersonEntity {
            PersonId = @event.Id,
            Nombre = @event.FirstName,
            SegundoNombre = @event.MiddleName,
            ApellidoPaterno = @event.LastName,
            ApellidoMaterno = @event.MotherLastName,
        };
        await _personRepository.CreateAsync(person);
    }

    public async Task On(PersonUpdatedEvent @event) {
        var person = await _personRepository.GetById(@event.Id);
        if(person == null) return;
        person.Nombre = @event.FirstName;
        person.SegundoNombre = @event.MiddleName;
        person.ApellidoPaterno = @event.LastName;
        person.ApellidoMaterno = @event.MotherLastName;
        await _personRepository.UpdateAsync(person);
    }

    public async Task On(PersonDeletedEvent @event) {
        await _personRepository.DeleteAsync(@event.Id);
    }
    public async Task On(IdentityDocumentAddedEvent @event) {
        var identity = new DocumentsIdentityEntity() {
            DocumentIdentityId = @event.IdentityId,
            DocumentIdentity = @event.IdentityDocument,
            PersonId = @event.Id
        };
        await _documentIdentityRepository.CreateAsync(identity);
    }

    public async Task On(IdentityDocumentUpdateEvent @event) {
        var identityDocument = await _documentIdentityRepository.GetById(@event.IdentityId);
        if(identityDocument == null) return;
        identityDocument.DocumentIdentity = @event.IdentityDocument;
        await _documentIdentityRepository.UpdateAsync(identityDocument);
    }

    public async Task On(IdentityDocumentRemovedEvent @event) {
        await _documentIdentityRepository.DeleteAsync(@event.IdentityId);
    }


}