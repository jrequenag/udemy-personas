using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;

using Microsoft.AspNetCore.Mvc;

using Person.Cmd.Api;
using Person.Common;

using Persons.Cmd.Api.Commands;

namespace Persons.Cmd.Api.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class EditPersonController : ControllerBase {
    private readonly ILogger<NewPersonController> _logger;
    private readonly ICommandDispatcher _commandDispatcher;

    public EditPersonController(ILogger<NewPersonController> logger
        , ICommandDispatcher commandDispatcher) {
        _logger = logger;
        _commandDispatcher = commandDispatcher;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> NewPersonAsync(Guid id, EditPersonCommand command) {
        try {
            command.Id = id;
            await _commandDispatcher.SendAsync(command);
            return StatusCode(StatusCodes.Status201Created, new NewPersonResponse() {
                Id = id,
                Message = "La persona ha sidoc creada"
            });
        } catch(InvalidOperationException ex) {
            _logger.LogWarning(ex, "El cliente hizo una peticion invalida");
            return StatusCode(StatusCodes.Status400BadRequest, new BaseResponse() {
                Message = ex.Message
            });
        } catch(AggregateNotFoundException ex) {
            _logger.Log(LogLevel.Warning, ex, "Could not retrieve aggregate, client passed an incorrect post ID targetting the aggregate!");
            return BadRequest(new BaseResponse {
                Message = ex.Message
            });
        } catch(Exception ex) {
            const string SAFE_ERROR_MESSAGE = "Error, mientras se procesaba se ejecutava la edicion de la persona";

            _logger.LogError(ex, SAFE_ERROR_MESSAGE);
            return StatusCode(StatusCodes.Status500InternalServerError, new NewPersonResponse() {
                Message = SAFE_ERROR_MESSAGE
            });
        }

    }
}