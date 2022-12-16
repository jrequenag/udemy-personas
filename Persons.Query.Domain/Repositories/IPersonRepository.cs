using Persons.Query.Domain.Entities;

namespace Persons.Query.Domain.Repositories;
public interface IPersonRepository {
    Task CreateAsync(PersonEntity person);
    Task UpdateAsync(PersonEntity person);
    Task DeleteAsync(Guid id);
    Task<PersonEntity> GetById(Guid id);
    Task<List<PersonEntity>> GetAllAsync();
    Task<List<PersonEntity>> GetByPersonName(string name);
    Task<List<PersonEntity>> listWithDocumentIdentity();
}
