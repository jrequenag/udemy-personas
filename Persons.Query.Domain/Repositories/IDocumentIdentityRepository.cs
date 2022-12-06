using Persons.Query.Domain.Entities;

namespace Persons.Query.Domain.Repositories;
public interface IDocumentIdentityRepository {
    Task CreateAsync(DocumentsIdentityEntity person);
    Task UpdateAsync(DocumentsIdentityEntity person);
    Task<DocumentsIdentityEntity> GetById(Guid id);
    Task DeleteAsync(Guid id);
}
