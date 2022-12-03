﻿using CQRS.Core.Commands;

namespace Persons.Cmd.Api.Commands;

public class EditPersonCommand : BaseCommand {
    public string Name { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string MotherLastName { get; set; }
}
