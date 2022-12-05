using CQRS.Core.Events;

namespace Person.Common.Events;
public class IdentityDocumentUpdateEvent : BaseEvent {
    public IdentityDocumentUpdateEvent() : base(nameof(IdentityDocumentUpdateEvent)) {
    }

    public Guid IdentityId { get; set; }
    public string IdentityDocument { get; set; }
    public DateTime EditDate { get; set; }
}
