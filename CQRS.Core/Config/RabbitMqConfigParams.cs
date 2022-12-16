namespace CQRS.Core.Config;
public class RabbitMqConfigParams {
    public RabbitMqConfigParams() { }
    public RabbitMqConfigParams(string hostname, string username, string password, int port) {
        Hostname = hostname;
        Username = username;
        Password = password;
        Port = port;
    }

    public string Hostname { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
    public int Port { get; init; }

}
