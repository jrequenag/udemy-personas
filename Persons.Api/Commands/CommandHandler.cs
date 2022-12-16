using CQRS.Core.Handlers;

using Persons.Cmd.Domain.Aggregates;

namespace Persons.Cmd.Api.Commands;

public class CommandHandler : ICommandHandler {
    private readonly IEventSourcingHandler<PersonAggregate> _eventSourcingHandler;

    public CommandHandler(IEventSourcingHandler<PersonAggregate> eventSourcingHandler) {
        _eventSourcingHandler = eventSourcingHandler;
    }
    public async Task HandlerAsync(NewPersonCommand command) {
        var aggregate = new PersonAggregate(command.Id, command.FirstName, command.MiddleName, command.LastName, command.MotherLastName);
        await _eventSourcingHandler.SaveAsync(aggregate);
    }
    public async Task HandlerAsync(EditPersonCommand command) {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        if(aggregate == null)
            throw new ArgumentNullException(nameof(aggregate));
        aggregate.EditPerson(command.FirstName, command.MiddleName, command.LastName, command.MotherLastName);
        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandlerAsync(AddIdentityDocumentCommand command) {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        aggregate.AddIdentityDocument(command.IdentityDocument);
        await _eventSourcingHandler.SaveAsync(aggregate);
    }



    public async Task HandlerAsync(EditdentityDocumentCommand command) {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        aggregate.EditIdentityDocument(command.Id, command.IdentityDocumentId, command.IdentityDocument);
        await _eventSourcingHandler.SaveAsync(aggregate);
    }



    public async Task HandlerAsync(RemoveIdentityDocumentCommand command) {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        aggregate.RemoveIdentityDocument(command.IdentityId);

        await _eventSourcingHandler.SaveAsync(aggregate);

    }
    public async Task HandlerAsync(DeletePersonCommand command) {

        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        aggregate.DeletePerson(command.Id);
        await _eventSourcingHandler.SaveAsync(aggregate);
    }
}
