using Microsoft.Extensions.DependencyInjection;
using Shared.Logging;
using Shared.Logging.Loggers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var fileLoggingSettings = builder.Configuration.GetSection("FileLogging");

builder.Services.AddSingleton<AbstractLogger>(l =>
{
    //abstract logger istenirse filelogger versin diye hem dependency inversion hem de singleton uygulanıyor
    //var logger = new FileLogger("C:\\Users\\Burak.Duygun\\OneDrive - Logo\\Desktop", "webapi");
    var logger = new FileLogger(fileLoggingSettings["Path"], fileLoggingSettings["ServiceName"]);
    //logger.SetLogLevel(Shared.Logging.LogLevel.Info);
    logger.SetLogLevel(Enum.Parse<Shared.Logging.LogLevel>(fileLoggingSettings["LogLevel"]));

    return logger;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
