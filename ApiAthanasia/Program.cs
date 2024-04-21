using ApiAthanasia.Data;
using ApiAthanasia.Helpers;
using ApiAthanasia.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NSwag.Generation.Processors.Security;
using NSwag;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
HelperActionServicesOAuth helper = new HelperActionServicesOAuth(builder.Configuration);
builder.Services.AddSingleton<HelperActionServicesOAuth>(helper);
builder.Services.AddAuthentication(helper.GetAuthenticateSchema()).AddJwtBearer(helper.GetJwtBearerOptions());
builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "Api Athanasia";
    document.Description = "Api Athanasia.  Proyecto Azure 2024";
    document.AddSecurity("JWT", Enumerable.Empty<string>(),
        new NSwag.OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Copia y pega el Token añadiendole la palabra bearer en el campo 'Value:' así: \"Bearer TuToken\"."
        }
    );
    document.OperationProcessors.Add(
    new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});
builder.Services.AddTransient<HelperMails>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<IRepositoryAthanasia, RepositoryAthanasia>();
string connectionString = builder.Configuration.GetConnectionString("SqlServerAzure");
builder.Services.AddDbContext<AthanasiaContext>(
    options => options.UseSqlServer(connectionString)
    );
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}
app.UseOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name:"Api Athanasia");
    options.RoutePrefix = "";
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
