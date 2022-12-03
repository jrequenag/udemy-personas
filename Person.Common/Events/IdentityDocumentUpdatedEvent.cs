using CQRS.Core.Events;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.Common.Events;
public class IdentityDocumentUpdateEvent : BaseEvent {
    public IdentityDocumentUpdateEvent() : base(nameof(IdentityDocumentUpdateEvent)) {
    }

    public Guid IdentityId { get; set; }
    public string IdentityDocument { get; set; }
    public DateTime EditDate { get; set; }
}
