using Microsoft.EntityFrameworkCore;

using Persons.Query.Domain.Entities;
using Persons.Query.Domain.Repositories;
using Persons.Query.Infrastructure.DataAccess;

namespace Persons.Query.Infrastructure.repositories;
public class DocumentIdentityRepository : IDocumentIdentityRepository {
    private readonly DatabaseContextFactory _databaseContext;

    public DocumentIdentityRepository(DatabaseContextFactory databaseContext) {
        _databaseContext = databaseContext;
    }

    public async Task CreateAsync(DocumentsIdentityEntity identityDocument) {
        using DatabaseContext context = _databaseContext.CreateContext();
        context.DocumentsIdentities.Add(identityDocument);
        _ = await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id) {
        using DatabaseContext context = _databaseContext.CreateContext();
        var identityDocument = await GetById(id);
        if(identityDocument == null) return;

        context.DocumentsIdentities.Remove(identityDocument);
        _ = await context.SaveChangesAsync();
    }

    public async Task<DocumentsIdentityEntity> GetById(Guid id) {
        using DatabaseContext context = _databaseContext.CreateContext();
        return await context.DocumentsIdentities.FirstOrDefaultAsync(d => d.DocumentIdentityId == id);
    }

    public async Task UpdateAsync(DocumentsIdentityEntity documentIdentity) {
        using DatabaseContext context = _databaseContext.CreateContext();
        context.DocumentsIdentities.Update(documentIdentity);
        _ = await context.SaveChangesAsync();
    }
}