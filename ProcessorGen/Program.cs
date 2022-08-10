using ProcessorGen;
using Serilog;


var name = typeof(Program).Assembly.GetName().Name;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.WithEnvironmentName()
    .Enrich.WithProperty("Assembly", name)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq(serverUrl: "http://host.docker.internal:5341")
    .CreateBootstrapLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog((hostContext, services, configuration) => {
        configuration.WriteTo.Console()
        .WriteTo.Seq(serverUrl: "http://host.docker.internal:5341");
    })
    .Build();

await host.RunAsync();
