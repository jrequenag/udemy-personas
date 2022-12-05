using CQRS.Core.Events;

namespace Person.Common.Events;
public class PersonCreatedEvent : BaseEvent {
    public PersonCreatedEvent() : base(nameof(PersonCreatedEvent)) {
    }

    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string MotherLastName { get; set; }

}
