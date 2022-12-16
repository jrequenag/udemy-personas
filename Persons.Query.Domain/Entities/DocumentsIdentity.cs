using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
