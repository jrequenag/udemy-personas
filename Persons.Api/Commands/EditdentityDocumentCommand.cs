using CQRS.Core.Commands;

namespace Persons.Cmd.Api.Commands;

public class EditdentityDocumentCommand : BaseCommand {
    public Guid IdentityDocumentId { get; set; }
    public string IdentityDocument { get; set; }
}
