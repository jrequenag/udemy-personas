using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Persons.Query.Domain.Entities;
[Table("DocumentosIdentidad")]
public class DocumentsIdentityEntity {
    [Key]
    public Guid DocumentIdentityId { get; set; }
    public string DocumentIdentity { get; set; }
    public Guid PersonId { get; set; }
    [JsonIgnore]
    public virtual PersonEntity Person { get; set; }
}
