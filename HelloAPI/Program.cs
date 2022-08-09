using HelloAPI.Domain;
using HelloAPI.Interfaces;
using HelloAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductLogic, ProductLogic>();
builder.Services.AddScoped<IQuickOrderLogic, QuickOrderLogic>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<CustomExceptionHandlingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
