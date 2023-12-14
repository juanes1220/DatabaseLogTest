
using Serilog;

// Temporary logger setup while application initializes and loads final configuration from application settings
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting Application.");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host
        .ConfigureAppConfiguration((hostingContext, builder) =>
        {
            builder.Sources.Clear();
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        })
        .UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration))

        .ConfigureServices((host, services) =>
        {
            services.AddControllers();
        });

    var app = builder.Build();
    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}
