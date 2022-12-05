using CQRS.Core.Commands;

namespace Persons.Cmd.Api.Commands;

public class RemoveIdentityDocumentCommand : BaseCommand {
    public Guid IdentityId { get; set; }
}
