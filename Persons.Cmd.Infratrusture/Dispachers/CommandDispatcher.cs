using CQRS.Core.Commands;
using CQRS.Core.Infrastructure;

namespace Persons.Cmd.Infratrusture.Dispachers;
public class CommandDispatcher : ICommandDispatcher {
    private readonly Dictionary<Type, Func<BaseCommand, Task>> _handlers = new();
    public void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand {
        if(_handlers.ContainsKey(typeof(T)))
            throw new IndexOutOfRangeException("No puede registar el mismo manejador de comando 2 veces");

        _handlers.Add(typeof(T), x => handler((T)x));
    }

    public async Task SendAsync(BaseCommand command) {
        if(!_handlers.TryGetValue(command.GetType(), out Func<BaseCommand, Task> handler))
            throw new ArgumentException(nameof(handler), "No se ha encontrado manejador de comando registrado");

        await handler(command);
    }
}
