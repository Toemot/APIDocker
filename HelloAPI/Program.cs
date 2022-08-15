using HelloAPI.Domain;
using HelloAPI.Interfaces;
using HelloAPI.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "hello";
var simpleProp = "Ola";
var nestedProp = "Omotola";

Log.ForContext("ConnectionStrings", connectionString)
    .ForContext("SimpleProperty", simpleProp)
    .ForContext("NestedProp", nestedProp)
    .Information("Loaded config", connectionString);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductLogic, ProductLogic>();
builder.Services.AddScoped<IQuickOrderLogic, QuickOrderLogic>();

builder.Configuration.GetConnectionString("Db");
builder.Configuration.GetValue<string>("SimpleProperty");
builder.Configuration.GetValue<string>("Inventory:NestedProp");

var name = typeof(Program).Assembly.GetName().Name;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
    .Enrich.WithEnvironmentName()
    .Enrich.WithProperty("Assembly", name)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq(serverUrl: "http://seq_in_dc:5341")
    .CreateBootstrapLogger();

builder.Host.UseSerilog((hostContext, services, configuration) => {
    configuration.WriteTo.Console()
    .WriteTo.Seq(serverUrl: "http://seq_in_dc:5341");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<CustomExceptionHandlingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
