using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Cms.PostService.Application.Contracts.Commands.Post;
using Cms.PostService.Application.Contracts.Commands.Post.Create;
using Cms.PostService.Application.Contracts.Commands.Post.Update;
using Cms.PostService.Application.Contracts.Queries.Post.GetById;
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
    [ProducesResponseType(typeof(PostGetPaginationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PaginationAsync(
        [FromQuery] PostGetPaginationRequest request,
        CancellationToken cancellationToken
    )
    {
        var response = await postGetPaginationQueryHandler.HandleAsync(request, cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PostGetByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var response = await postGetByIdQueryHandler.HandleAsync(
            new PostGetByIdRequest(id),
            cancellationToken
        );

        return response is null ? NotFound() : Ok(response);
    }

    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PostCreateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] PostCreateRequest request,
        CancellationToken cancellationToken
    )
    {
        var response = await postCreateCommandHandler.HandleAsync(request, cancellationToken);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PostUpdateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] PostUpdateRequest request,
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
        await postDeleteCommandHandler.HandleAsync(new PostDeleteRequest(id), cancellationToken);

        return NoContent();
    }

    [HttpPut("{id:guid}/workflow/next")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PostWorkflowNextResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> WorkflowNextAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var response = await postWorkflowNextCommandHandler.HandleAsync(
            new PostWorkflowNextRequest(id),
            cancellationToken
        );

        return response is null ? NotFound() : Ok(response);
    }

    [HttpPut("{id:guid}/workflow/back")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PostWorkflowBackResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> WorkflowBackAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var response = await postWorkflowBackCommandHandler.HandleAsync(
            new PostWorkflowBackRequest(id),
            cancellationToken
        );

        return response is null ? NotFound() : Ok(response);
    }
}
