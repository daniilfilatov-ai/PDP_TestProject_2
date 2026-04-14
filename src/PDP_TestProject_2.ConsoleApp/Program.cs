using Microsoft.Extensions.Logging;

using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = factory.CreateLogger("Program");
logger.LogInformation("That is test log message");
logger.LogError("That is test error message");
