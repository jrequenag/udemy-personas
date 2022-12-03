using CQRS.Core.Commands;

namespace Persons.Cmd.Api.Commands;

public class DeletePersonCommand : BaseCommand {
    public Guid PersonId { get; set; }

}
