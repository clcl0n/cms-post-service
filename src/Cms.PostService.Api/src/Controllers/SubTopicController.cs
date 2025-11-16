using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Api.Contracts.Requests;
using Cms.PostService.Api.Contracts.Responses;
using Cms.PostService.Api.Mappings;
using Cms.PostService.Application.Contracts.Commands;
using Cms.PostService.Application.Contracts.Queries;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Cms.PostService.Application.Handlers.Queries.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.PostService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubTopicController(
    ISubTopicCreateCommandHandler subTopicCreateCommandHandler,
    ISubTopicUpdateCommandHandler subTopicUpdateCommandHandler,
    ISubTopicDeleteCommandHandler subTopicDeleteCommandHandler,
    ISubTopicGetByIdQueryHandler subTopicGetByIdQueryHandler
) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(SubTopicGetByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SubTopicGetByIdResponse>> GetByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var query = new SubTopicGetByIdQuery(id);

        var response = await subTopicGetByIdQueryHandler.HandleAsync(query, cancellationToken);

        if (response is null)
        {
            return NotFound();
        }

        var result = SubTopicResponseMappings.ToSubTopicGetByIdResponse(response);

        return Ok(result);
    }

    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(SubTopicCreateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SubTopicCreateResponse>> CreateAsync(
        [FromBody] SubTopicCreateRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new SubTopicCreateCommand(request.ParentTopicId, request.Title);

        var response = await subTopicCreateCommandHandler.HandleAsync(command, cancellationToken);

        var result = SubTopicResponseMappings.ToSubTopicCreateResponse(response);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(SubTopicUpdateCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] SubTopicUpdateRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new SubTopicUpdateCommand(id, request.Title);

        var response = await subTopicUpdateCommandHandler.HandleAsync(command, cancellationToken);

        if (response is null)
        {
            return NotFound();
        }

        var result = SubTopicResponseMappings.ToSubTopicUpdateResponse(response);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var command = new SubTopicDeleteCommand(id);

        await subTopicDeleteCommandHandler.HandleAsync(command, cancellationToken);

        return NoContent();
    }
}
