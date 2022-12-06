using Microsoft.EntityFrameworkCore;

using Persons.Query.Domain.Entities;

namespace Persons.Query.Infrastructure.DataAccess;
public class DatabaseContext : DbContext {
    public DatabaseContext(DbContextOptions options) : base(options) { }
    public DbSet<PersonEntity> Person { get; set; }
    public DbSet<DocumentsIdentityEntity> DocumentsIdentities { get; set; }
}