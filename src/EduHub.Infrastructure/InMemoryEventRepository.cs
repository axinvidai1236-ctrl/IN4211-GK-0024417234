using System.Collections.Generic;
using System.Threading.Tasks;
using EduHub.Application.Interfaces;
using EduHub.Domain.Entities;

namespace EduHub.Infrastructure
{
    public class InMemoryEventRepository : IEventRepository
    {
        private readonly List<Event> _events = new();

        public Task AddAsync(Event ev)
        {
            _events.Add(ev);
            return Task.CompletedTask;
        }
    }
}