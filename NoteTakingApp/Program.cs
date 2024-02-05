
using NoteTakingApp.Api;
using NoteTakingApp.Application;
using NoteTakingApp.Infrastructure;
using NoteTakingApp.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Environment.WebRootPath).
    AddPersisitenceServices(builder.Configuration).
    AddInfraStructureServices(builder.Configuration).
    AddApiServices(builder.Configuration);
var app = builder.Build();
app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(option =>
{
    option.SetIsOriginAllowed(_ => true)
    .AllowAnyHeader()
    .AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
