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
public class PostController(
    IPostCreateCommandHandler postCreateCommandHandler,
    IPostUpdateCommandHandler postUpdateCommandHandler,
    IPostDeleteCommandHandler postDeleteCommandHandler,
    IPostGetByIdQueryHandler postGetByIdQueryHandler,
    IPostGetPaginationQueryHandler postGetPaginationQueryHandler,
    IPostWorkflowNextCommandHandler postWorkflowNextCommandHandler,
    IPostWorkflowBackCommandHandler postWorkflowBackCommandHandler
) : ControllerBase
{
    [HttpGet("pagination")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PostGetPaginationQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PaginationAsync(
        [FromQuery] PostGetPaginationQuery request,
        CancellationToken cancellationToken
    )
    {
        var response = await postGetPaginationQueryHandler.HandleAsync(request, cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PostGetByIdQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var response = await postGetByIdQueryHandler.HandleAsync(
            new PostGetByIdQuery(id),
            cancellationToken
        );

        return response is null ? NotFound() : Ok(response);
    }

    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PostCreateQueryResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] PostCreateQuery request,
        CancellationToken cancellationToken
    )
    {
        var response = await postCreateCommandHandler.HandleAsync(request, cancellationToken);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PostUpdateCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] PostUpdateCommand request,
        CancellationToken cancellationToken
    )
    {
        var response = await postUpdateCommandHandler.HandleAsync(request, cancellationToken);

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
        await postDeleteCommandHandler.HandleAsync(new PostDeleteCommand(id), cancellationToken);

        return NoContent();
    }

    [HttpPut("{id:guid}/workflow/next")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PostWorkflowNextCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> WorkflowNextAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var response = await postWorkflowNextCommandHandler.HandleAsync(
            new PostWorkflowNextCommand(id),
            cancellationToken
        );

        return response is null ? NotFound() : Ok(response);
    }

    [HttpPut("{id:guid}/workflow/back")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PostWorkflowBackCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> WorkflowBackAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var response = await postWorkflowBackCommandHandler.HandleAsync(
            new PostWorkflowBackCommand(id),
            cancellationToken
        );

        return response is null ? NotFound() : Ok(response);
    }
}
