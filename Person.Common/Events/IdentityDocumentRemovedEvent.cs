using CQRS.Core.Events;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.Common.Events;
public class IdentityDocumentRemovedEvent : BaseEvent {
    public IdentityDocumentRemovedEvent() : base(nameof(IdentityDocumentRemovedEvent)) {
    }

    public Guid IdentityId { get; set; }
}
