using Movies.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory()) 
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.Migrations.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();  

builder.Services
    .AddAppConections(builder.Configuration)
    .AddHandlers()
    .AddAndConfigureControllers()
    .AddHttpLogging(logging =>
    {
        logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
        logging.RequestBodyLogLimit = 4096; 
        logging.ResponseBodyLogLimit = 4096;
    })
    .AddCors(p => p.AddPolicy("CORS", builder =>
    {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    }));

var app = builder.Build();

app.UseHttpLogging();
app.UseDocumentation();
app.UseCors("CORS");
// app.UseAuthentication();
// app.UseAuthorization();
app.MapControllers();

app.Run();