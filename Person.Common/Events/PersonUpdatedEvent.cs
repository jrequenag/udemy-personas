using CQRS.Core.Events;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.Common.Events;
public class PersonUpdatedEvent : BaseEvent {
    public PersonUpdatedEvent() : base(nameof(PersonUpdatedEvent)) {
    }

    public string Name { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string MotherLastName { get; set; }
}
