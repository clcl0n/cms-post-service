using System.CommandLine;
using Cms.PostService.Application;
using Cms.PostService.Cli.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cms.PostService.Cli;

public static class Program
{
    public static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Services.ConfigureWolverine(builder.Configuration);
        builder.Services.AddApplication(builder.Configuration);

        builder.Services.AddSingleton(args);

        builder.Services.AddHostedService<Worker>();

        IHost host = builder.Build();

        host.Run();
    }
}
