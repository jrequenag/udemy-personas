namespace Persons.Cmd.Api.Commands;

public interface ICommandHandler {
    Task HandlerAsync(NewPersonCommand command);
    Task HandlerAsync(AddIdentityDocumentCommand command);
    Task HandlerAsync(DeletePersonCommand command);
    Task HandlerAsync(EditdentityDocumentCommand command);
    Task HandlerAsync(EditPersonCommand command);
    Task HandlerAsync(RemoveIdentityDocumentCommand command);
}
