using ApiAthanasia.Data;
using ApiAthanasia.Helpers;
using ApiAthanasia.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api Athanasia",
        Description = "Api para mi proyecto Azure"
    });
}); ;

builder.Services.AddTransient<HelperPathProvider>();
builder.Services.AddTransient<HelperMails>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<IRepositoryAthanasia, RepositoryAthanasia>();
string connectionString = builder.Configuration.GetConnectionString("SqlServerAzure");
builder.Services.AddDbContext<AthanasiaContext>(
    options => options.UseSqlServer(connectionString)
    );
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name:"Api Athanasia");
    options.RoutePrefix = "";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
