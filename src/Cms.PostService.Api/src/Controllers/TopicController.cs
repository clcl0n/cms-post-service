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
public class TopicController(
    ITopicCreateCommandHandler topicCreateCommandHandler,
    ITopicUpdateCommandHandler topicUpdateCommandHandler,
    ITopicDeleteCommandHandler topicDeleteCommandHandler,
    ITopicGetByIdQueryHandler topicGetByIdQueryHandler
) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(TopicGetByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TopicGetByIdResponse>> GetByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var query = new TopicGetByIdQuery(id);

        var response = await topicGetByIdQueryHandler.HandleAsync(query, cancellationToken);

        if (response is null)
        {
            return NotFound();
        }

        var result = TopicResponseMappings.ToTopicGetByIdResponse(response);

        return Ok(result);
    }

    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(TopicCreateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TopicCreateResponse>> CreateAsync(
        [FromBody] TopicCreateRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new TopicCreateCommand(request.Title);

        var response = await topicCreateCommandHandler.HandleAsync(command, cancellationToken);
        
        var result = TopicResponseMappings.ToTopicCreateResponse(response);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(TopicUpdateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TopicUpdateResponse>> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] TopicUpdateRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new TopicUpdateCommand(id, request.Title);

        var response = await topicUpdateCommandHandler.HandleAsync(command, cancellationToken);

        if (response is null)
        {
            return NotFound();
        }

        var result = TopicResponseMappings.ToTopicUpdateResponse(response);

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
        var command = new TopicDeleteCommand(id);

        await topicDeleteCommandHandler.HandleAsync(command, cancellationToken);

        return NoContent();
    }
}
