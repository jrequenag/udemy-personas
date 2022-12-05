using CQRS.Core.Events;

namespace Person.Common.Events;
public class PersonDeletedEvent : BaseEvent {
    public PersonDeletedEvent() : base(nameof(PersonDeletedEvent)) {
    }
}
