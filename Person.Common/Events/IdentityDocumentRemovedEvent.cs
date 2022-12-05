using CQRS.Core.Events;

namespace Person.Common.Events;
public class IdentityDocumentRemovedEvent : BaseEvent {
    public IdentityDocumentRemovedEvent() : base(nameof(IdentityDocumentRemovedEvent)) {
    }

    public Guid IdentityId { get; set; }
}
