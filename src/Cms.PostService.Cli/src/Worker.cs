using System.CommandLine;
using Cms.PostService.Application.Contracts.Commands.Topic;
using Cms.PostService.Application.Handlers.Commands.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cms.PostService.Cli;

internal sealed class Worker(
    ITopicCreateCommandHandler topicCreateCommandHandler,
    string[] args,
    ILogger<Worker> logger,    
    IHostApplicationLifetime lifetime) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);


        var rootCommand = new RootCommand();

        var generateCommands = new Command("generate");

        rootCommand.Add(generateCommands);

        var topicCommands = new Command("topic");

        var topicGenerateRandomCommand = new Command("random");

        topicGenerateRandomCommand.SetAction(async (parseResult) =>
        {
            await topicCreateCommandHandler.HandleAsync(
                new TopicCreateRequest(new Bogus.Randomizer().Word()),
                cancellationToken
            );
        });

        topicCommands.Add(topicGenerateRandomCommand);

        generateCommands.Add(topicCommands);

        var parsed = rootCommand.Parse(args);

        await parsed.InvokeAsync(null, cancellationToken);

        lifetime.StopApplication();
    }
}