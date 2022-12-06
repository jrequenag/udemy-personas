using Microsoft.EntityFrameworkCore;

namespace Persons.Query.Infrastructure.DataAccess;
public class DatabaseContextFactory {
    private readonly Action<DbContextOptionsBuilder> _configurationDbContext;
    public DatabaseContextFactory(Action<DbContextOptionsBuilder> configurationDbContext) {
        _configurationDbContext = configurationDbContext;
    }
    public DatabaseContext CreateContext() {
        DbContextOptionsBuilder<DatabaseContext> optionsBuilder = new();
        _configurationDbContext(optionsBuilder);
        return new DatabaseContext(optionsBuilder.Options);
    }
}