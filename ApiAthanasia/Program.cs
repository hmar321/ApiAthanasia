using ApiAthanasia.Data;
using ApiAthanasia.Helpers;
using ApiAthanasia.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NSwag.Generation.Processors.Security;
using NSwag;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "Api Athanasia",
//        Description = "Api para mi proyecto Azure"
//    });
//}); ;
// REGISTRAMOS SWAGGER COMO SERVICIO
builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "Api Athanasia";
    document.Description = "Api Athanasia.  Proyecto Azure 2024";
    // CONFIGURAMOS LA SEGURIDAD JWT PARA SWAGGER,
    // PERMITE AÑADIR EL TOKEN JWT A LA CABECERA.
    document.AddSecurity("JWT", Enumerable.Empty<string>(),
        new NSwag.OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Copia y pega el Token en el campo 'Value:' así: Bearer {Token JWT}."
        }
    );
    document.OperationProcessors.Add(
    new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

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
app.UseOpenApi();
app.UseSwaggerUI(options =>
{
    //options.InjectStylesheet("/css/bootstrap.css");
    //options.InjectStylesheet("/css/material3x.css");
    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name:"Api Athanasia");
    options.RoutePrefix = "";
});

app.UseHttpsRedirection();
//app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
