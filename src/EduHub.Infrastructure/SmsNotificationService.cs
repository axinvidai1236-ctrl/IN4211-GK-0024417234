using System;
using EduHub.Application.Interfaces;

namespace EduHub.Infrastructure
{
    public class SmsNotificationService : INotificationService
    {
        public void Send(string message) => Console.WriteLine($"[SMS] {message}");
    }
}