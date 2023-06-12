using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
{
    configurationBuilder.AddJsonFile($"ocelot.{hostBuilderContext.HostingEnvironment.EnvironmentName}.json", true, true);
});

builder.Host.ConfigureLogging((hostBuilderContext, loggingBuilder) =>
{
    loggingBuilder.AddConfiguration(hostBuilderContext.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

builder.Services.AddOcelot().AddCacheManager(settings => settings.WithDictionaryHandle());

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await app.UseOcelot();

app.Run();
