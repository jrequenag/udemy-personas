using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;

using Microsoft.AspNetCore.Mvc;

using Person.Cmd.Api;
using Person.Common;

using Persons.Cmd.Api.Commands;

namespace Persons.Cmd.Api.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class RemoveIdentityDocumentController : ControllerBase {
    private readonly ILogger<RemoveIdentityDocumentController> _logger;
    private readonly ICommandDispatcher _commandDispatcher;

    public RemoveIdentityDocumentController(ILogger<RemoveIdentityDocumentController> logger
        , ICommandDispatcher commandDispatcher) {
        _logger = logger;
        _commandDispatcher = commandDispatcher;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveDocumentIdentity(Guid id, RemoveIdentityDocumentCommand command) {
        try {
            command.Id = id;
            await _commandDispatcher.SendAsync(command);
            return StatusCode(StatusCodes.Status204NoContent, new BaseResponse() {
                Message = "El documento de identidad ha sido agregado satisfactoriamente"
            });
        } catch(InvalidOperationException ex) {
            _logger.LogWarning(ex, "El cliente hizo una peticion invalida");
            return StatusCode(StatusCodes.Status400BadRequest, new BaseResponse() {
                Message = ex.Message
            });
        } catch(AggregateNotFoundException ex) {
            _logger.Log(LogLevel.Warning, ex, "Could not retrieve aggregate, client passed an incorrect person ID targetting the aggregate!");
            return BadRequest(new BaseResponse {
                Message = ex.Message
            });
        } catch(Exception ex) {
            const string SAFE_ERROR_MESSAGE = "Error, mientras se procesaba se ejecutava el aggregado del documento de de identidad";

            _logger.LogError(ex, SAFE_ERROR_MESSAGE);
            return StatusCode(StatusCodes.Status500InternalServerError, new NewPersonResponse() {
                Message = SAFE_ERROR_MESSAGE
            });
        }

    }
}