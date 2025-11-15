using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
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
    [ProducesResponseType(typeof(TopicGetByIdQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var response = await topicGetByIdQueryHandler.HandleAsync(
            new TopicGetByIdQuery(id),
            cancellationToken
        );

        return response is null ? NotFound() : Ok(response);
    }

    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(TopicCreateCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] TopicCreateCommand request,
        CancellationToken cancellationToken
    )
    {
        var response = await topicCreateCommandHandler.HandleAsync(request, cancellationToken);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(TopicUpdateCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] TopicUpdateCommand request,
        CancellationToken cancellationToken
    )
    {
        var response = await topicUpdateCommandHandler.HandleAsync(request, cancellationToken);

        return response is null ? NotFound() : Ok(response);
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
        await topicDeleteCommandHandler.HandleAsync(new TopicDeleteCommand(id), cancellationToken);

        return NoContent();
    }
}
