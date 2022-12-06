using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons.Query.Domain.Entities;
[Table("Personas")]
public class PersonEntity {
    [Key]
    public Guid PersonId { get; set; }
    public string Nombre { get; set; }
    public string SegundoNombre { get; set; }
    public string ApellidoPaterno { get; set; }
    public string ApellidoMaterno { get; set; }
    public DateTime FechaCreacion { get; set; }
    public virtual ICollection<DocumentsIdentityEntity> DocumentosIdentidad { get; set; }
}
