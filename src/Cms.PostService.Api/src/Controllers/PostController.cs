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
    public async Task<ActionResult<PostGetPaginationResponse>> PaginationAsync(
        [FromQuery] PostGetPaginationRequest request,
        CancellationToken cancellationToken
    )
    {
        var query = new PostGetPaginationQuery(request.Page, request.PageSize);

        var response = await postGetPaginationQueryHandler.HandleAsync(query, cancellationToken);

        var result = PostResponseMappings.ToPostGetPaginationResponse(response);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PostGetByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PostGetByIdResponse>> GetByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var query = new PostGetByIdQuery(id);

        var response = await postGetByIdQueryHandler.HandleAsync(query, cancellationToken);

        var result = PostResponseMappings.ToPostGetByIdResponse(response);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PostCreateCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] PostCreateRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = PostRequestMappings.ToPostCreateCommand(request);

        var response = await postCreateCommandHandler.HandleAsync(command, cancellationToken);

        var result = PostResponseMappings.ToPostCreateResponse(response);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PostUpdateCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] PostUpdateRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = PostRequestMappings.ToPostUpdateCommand(request, id);        

        var response = await postUpdateCommandHandler.HandleAsync(command, cancellationToken);

        var result = PostResponseMappings.ToPostUpdateResponse(response);

        return result is null ? NotFound() : Ok(result);
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
        var command = new PostDeleteCommand(id);

        await postDeleteCommandHandler.HandleAsync(command, cancellationToken);

        return NoContent();
    }

    [HttpPut("{id:guid}/workflow/next")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PostWorkflowNextResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PostWorkflowNextResponse>> WorkflowNextAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var command = new PostWorkflowNextCommand(id);

        var response = await postWorkflowNextCommandHandler.HandleAsync(command, cancellationToken);

        if (response is null)
        {
            return NotFound();
        }

        var result = new PostWorkflowNextResponse(response.Id, response.Status);

        return Ok(result);
    }

    [HttpPut("{id:guid}/workflow/back")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PostWorkflowBackResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PostWorkflowBackResponse>> WorkflowBackAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var command = new PostWorkflowBackCommand(id);

        var response = await postWorkflowBackCommandHandler.HandleAsync(command, cancellationToken);

        if (response is null)
        {
            return NotFound();
        }

        var result = new PostWorkflowBackResponse(response.Id, response.Status);

        return Ok(result);
    }
}
