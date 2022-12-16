using CQRS.Core.Domain;

using Person.Common.Events;

namespace Persons.Cmd.Domain.Aggregates;
public class PersonAggregate : AggregateRoot {
    private bool _active;
    private string _firstName;
    private string _middleName;
    private string _lastName;
    private string _motherLastName;
    public readonly Dictionary<Guid, string> _identityDocuments = new();

    public bool Active {
        get => _active; set => _active = value;
    }
    public PersonAggregate() { }

    public PersonAggregate(Guid id, string firstName, string middleName, string lastName, string motherLastName) {
        RaiseEvent(new PersonCreatedEvent() {
            Id = id,
            FirstName = firstName,
            MiddleName = middleName,
            LastName = lastName,
            MotherLastName = motherLastName
        });
    }

    public void Apply(PersonCreatedEvent @event) {
        _id = @event.Id;
        _active = true;
        _firstName = @event.FirstName;
        _middleName = @event.MiddleName;
        _lastName = @event.LastName;
        _motherLastName = @event.MotherLastName;
    }

    public void EditPerson(string firstName, string middleName, string lastName, string motherLastName) {
        if(!_active)
            throw new InvalidOperationException("no puede editar una persona inactiva");
        if(string.IsNullOrEmpty(firstName))
            throw new InvalidOperationException($"el valor de {nameof(firstName)}, no puede ser nulo o vacio, por favor provea un valor valido");
        if(string.IsNullOrEmpty(middleName))
            throw new InvalidOperationException($"el valor de {nameof(middleName)}, no puede ser nulo o vacio, por favor provea un valor valido");
        if(string.IsNullOrEmpty(lastName))
            throw new InvalidOperationException($"el valor de {nameof(lastName)}, no puede ser nulo o vacio, por favor provea un valor valido");
        if(string.IsNullOrEmpty(motherLastName))
            throw new InvalidOperationException($"el valor de {nameof(motherLastName)}, no puede ser nulo o vacio, por favor provea un valor valido");

        RaiseEvent(new PersonUpdatedEvent() {
            Id = Id,
            FirstName = firstName,
            MiddleName = middleName,
            LastName = lastName,
            MotherLastName = motherLastName
        });
    }
    public void Apply(PersonUpdatedEvent @event) {
        _id = @event.Id;
    }

    public void AddIdentityDocument(string IdentityDocument) {
        if(string.IsNullOrEmpty(IdentityDocument))
            throw new InvalidOperationException($"el valor de {nameof(IdentityDocument)}, no puede ser nulo o vacio, por favor provea un valor valido");

        RaiseEvent(new IdentityDocumentAddedEvent() {
            Id = _id,
            IdentityId = Guid.NewGuid(),
            IdentityDocument = IdentityDocument
        });
    }
    public void Apply(IdentityDocumentAddedEvent @event) {
        _id = @event.Id;
        _identityDocuments.Add(@event.IdentityId, @event.IdentityDocument);
    }

    public void EditIdentityDocument(Guid id, Guid identityDocumentId, string IdentityDocument) {
        if(!_active)
            throw new InvalidOperationException("no puede editar una persona inactiva");
        RaiseEvent(new IdentityDocumentUpdateEvent() {
            Id = id,
            IdentityId = identityDocumentId,
            IdentityDocument = IdentityDocument
        });
    }
    public void Apply(IdentityDocumentUpdateEvent @event) {
        _id = @event.Id;
        _identityDocuments[@event.IdentityId] = @event.IdentityDocument;
    }
    public void RemoveIdentityDocument(Guid identityDocumentId) {
        if(!_active)
            throw new InvalidOperationException("no puede editar una persona inactiva");

        RaiseEvent(new IdentityDocumentRemovedEvent() {
            Id = _id,
            IdentityId = identityDocumentId

        });
    }
    public void Apply(IdentityDocumentRemovedEvent @event) {
        _id = @event.Id;
        _identityDocuments.Remove(@event.IdentityId);
    }
    public void DeletePerson(Guid personId) {
        if(!_active)
            throw new InvalidOperationException("no puede editar una persona inactiva");
        if(Id != personId)
            throw new InvalidOperationException("No puede borrar la persona");

        RaiseEvent(new PersonDeletedEvent { Id = _id });
    }
    public void Apply(PersonDeletedEvent @event) {
        _id = @event.Id;
        _active = false;
    }
}
