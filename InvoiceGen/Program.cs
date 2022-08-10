// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;
using Serilog;


ConfigureLogging();

try
{
    Log.Information("Starting program");
    Console.WriteLine("Hello, World!");
    Log.Information("Finished execution");
}
catch (Exception ex)
{
    Log.Error(ex, ex.Message);
}

static void ConfigureLogging()
{
    var name = typeof(Program).Assembly.GetName().Name;

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithProperty("Assembly", name)
        .WriteTo.Console()
        .WriteTo.Seq(serverUrl: "http://host.docker.internal:5341")
        .CreateLogger();
}

IHost host = Host.CreateDefaultBuilder(args)
    .UseSerilog((hostContext, services, configuration) => {
        configuration.WriteTo.Console()
        .WriteTo.Seq(serverUrl: "http://host.docker.internal:5341");
    })
    .Build();

await host.RunAsync();