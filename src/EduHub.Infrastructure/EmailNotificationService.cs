using System;
using EduHub.Application.Interfaces;

namespace EduHub.Infrastructure
{
    public class EmailNotificationService : INotificationService
    {
        public void Send(string message) => Console.WriteLine($"[EMAIL] {message}");
    }
}