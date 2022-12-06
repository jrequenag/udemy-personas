using Microsoft.EntityFrameworkCore;

using Persons.Query.Domain.Entities;
using Persons.Query.Domain.Repositories;
using Persons.Query.Infrastructure.DataAccess;

namespace Persons.Query.Infrastructure.repositories;
public class PersonRepository : IPersonRepository {
    private readonly DatabaseContextFactory _databaseContext;

    public PersonRepository(DatabaseContextFactory databaseContext) {
        _databaseContext = databaseContext;
    }
    public async Task CreateAsync(PersonEntity person) {
        using DatabaseContext context = _databaseContext.CreateContext();
        context.Person.Add(person);
        _ = await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id) {
        using DatabaseContext context = _databaseContext.CreateContext();
        var person = await GetById(id);
        if(person == null)
            return;
        context.Person.Remove(person);
        _ = await context.SaveChangesAsync();
    }

    public async Task<List<PersonEntity>> GetAllAsync() {
        using DatabaseContext context = _databaseContext.CreateContext();
        return await context.Person.AsNoTracking()
            .Include(d => d.DocumentosIdentidad)
            .ToListAsync();
    }

    public async Task<PersonEntity> GetById(Guid personId) {
        using DatabaseContext context = _databaseContext.CreateContext();
        return await context.Person
            .Include(d => d.DocumentosIdentidad)
            .FirstOrDefaultAsync(p => p.PersonId == personId);
    }

    public async Task<List<PersonEntity>> GetByPersonName(string name) {
        using DatabaseContext context = _databaseContext.CreateContext();
        return await context.Person.AsNoTracking()
            .Where(p => p.Nombre.Contains(name))
            .Include(d => d.DocumentosIdentidad).AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<PersonEntity>> listWithDocumentIdentity() {
        using DatabaseContext context = _databaseContext.CreateContext();
        return await context.Person.AsNoTracking()
            .Where(p => p.DocumentosIdentidad != null && p.DocumentosIdentidad.Any())
            .Include(d => d.DocumentosIdentidad).AsNoTracking()
            .ToListAsync();
    }

    public async Task UpdateAsync(PersonEntity person) {
        using DatabaseContext context = _databaseContext.CreateContext();
        context.Person.Update(person);
        _ = await context.SaveChangesAsync();
    }
}