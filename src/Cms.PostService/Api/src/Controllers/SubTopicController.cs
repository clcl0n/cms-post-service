using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Commands.SubTopic;
using Cms.PostService.Application.Contracts.Queries.SubTopic;
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
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var response = await subTopicGetByIdQueryHandler.HandleAsync(
            new SubTopicGetByIdRequest(id),
            cancellationToken
        );

        return response is null ? NotFound() : Ok(response);
    }

    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(SubTopicCreateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] SubTopicCreateRequest request,
        CancellationToken cancellationToken
    )
    {
        var response = await subTopicCreateCommandHandler.HandleAsync(request, cancellationToken);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(SubTopicUpdateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] SubTopicUpdateRequest request,
        CancellationToken cancellationToken
    )
    {
        var response = await subTopicUpdateCommandHandler.HandleAsync(request, cancellationToken);

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
        await subTopicDeleteCommandHandler.HandleAsync(
            new SubTopicDeleteRequest(id),
            cancellationToken
        );

        return NoContent();
    }
}
