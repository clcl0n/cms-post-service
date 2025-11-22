using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cms.PostService.Api.Setups;

internal static class ProblemDetailsSetup
{
    public static IServiceCollection SetupProblemDetails(
        this IServiceCollection services,
        IHostEnvironment environment
    )
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                var error = context.HttpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error;

                context.ProblemDetails.Status = error switch
                {
                    _ => context.ProblemDetails.Status,
                };

                context.HttpContext.Response.StatusCode = context.ProblemDetails.Status!.Value;

                context.ProblemDetails.Title = environment.IsDevelopment()
                    ? context.ProblemDetails.Title = error?.Message
                    : context.ProblemDetails.Title =
                        "An error occurred while processing your request.";

                context.ProblemDetails.Detail = string.Empty;

                context.ProblemDetails.Type = context.ProblemDetails.Status switch
                {
                    StatusCodes.Status400BadRequest =>
                        "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    StatusCodes.Status401Unauthorized =>
                        "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
                    StatusCodes.Status404NotFound =>
                        "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                    StatusCodes.Status409Conflict =>
                        "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
                    StatusCodes.Status410Gone =>
                        "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.9",
                    _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                };
            };
        });

        return services;
    }
}
