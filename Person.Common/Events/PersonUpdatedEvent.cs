using CQRS.Core.Events;

namespace Person.Common.Events;
public class PersonUpdatedEvent : BaseEvent {
    public PersonUpdatedEvent() : base(nameof(PersonUpdatedEvent)) {
    }

    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string MotherLastName { get; set; }
}
