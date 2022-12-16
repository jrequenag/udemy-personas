using Person.Common;

namespace Person.Cmd.Api;
public class NewPersonResponse : BaseResponse {
    public Guid Id { get; set; }
}