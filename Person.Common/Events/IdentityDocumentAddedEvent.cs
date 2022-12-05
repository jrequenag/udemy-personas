using CQRS.Core.Events;

namespace Person.Common.Events;
public class IdentityDocumentAddedEvent : BaseEvent {
    public IdentityDocumentAddedEvent() : base(nameof(IdentityDocumentAddedEvent)) {
    }

    public Guid IdentityId { get; set; }
    public string IdentityDocument { get; set; }
}
