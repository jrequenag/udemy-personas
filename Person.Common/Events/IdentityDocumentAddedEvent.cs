using CQRS.Core.Events;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.Common.Events;
public class IdentityDocumentAddedEvent : BaseEvent {
    public IdentityDocumentAddedEvent() : base(nameof(IdentityDocumentAddedEvent)) {
    }

    public Guid IdentityId { get; set; }
    public string IdentityDocument { get; set; }
}
