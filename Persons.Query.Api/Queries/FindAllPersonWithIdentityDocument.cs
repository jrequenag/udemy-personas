using CQRS.Core.Queries;

namespace Persons.Query.Api.Queries;
public class FindAllPersonQuery : BaseQuery {
    public Guid Id { get; set; }
}