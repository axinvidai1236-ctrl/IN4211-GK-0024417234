using System;
using System.Threading.Tasks;
using EduHub.Application.Interfaces;
using EduHub.Domain.Entities;

namespace EduHub.Application.Services
{
    public class EventService
    {
        private readonly IEventRepository _repository;
        private readonly Func<string, INotificationService> _notificationFactory;

        public EventService(IEventRepository repository, Func<string, INotificationService> notificationFactory)
        {
            _repository = repository;
            _notificationFactory = notificationFactory;
        }

        public async Task CreateEventAsync(Guid clubId, string title, DateTime date, int capacity, string channel)
        {
            var ev = Event.Create(clubId, title, date, capacity);
            
            await _repository.AddAsync(ev);
            
            var notificationService = _notificationFactory(channel);
            notificationService.Send($"Sự kiện mới: {ev.Title}");
        }
    }
}