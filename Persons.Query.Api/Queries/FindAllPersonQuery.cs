using CQRS.Core.Queries;

namespace Persons.Query.Api.Queries;
public class FindAllPersonWithIdentityDocument : BaseQuery {
    public Guid Id { get; set; }
}