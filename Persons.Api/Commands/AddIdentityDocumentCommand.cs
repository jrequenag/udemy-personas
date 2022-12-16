using CQRS.Core.Commands;

namespace Persons.Cmd.Api.Commands;

public class AddIdentityDocumentCommand : BaseCommand {
    public string IdentityDocument { get; set; }

}
