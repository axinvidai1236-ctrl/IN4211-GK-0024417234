using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddSingleton<IEventRepository, InMemoryEventRepository>();
builder.Services.AddTransient<EmailNotificationService>();
builder.Services.AddTransient<SmsNotificationService>();

builder.Services.AddTransient<Func<string, INotificationService>>(serviceProvider => key =>
{
    return key.ToLower() switch
    {
        "email" => serviceProvider.GetRequiredService<EmailNotificationService>(),
        "sms" => serviceProvider.GetRequiredService<SmsNotificationService>(),
        _ => throw new ArgumentException("Invalid notification channel")
    };
});

builder.Services.AddTransient<EventService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();