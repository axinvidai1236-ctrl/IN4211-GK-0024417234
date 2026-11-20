using System;
using System.Threading.Tasks;
using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Domain.Entities;
using Moq;
using Xunit;

namespace EduHub.Tests
{
    public class EventServiceTests
    {
        [Fact]
        public async Task CreateEventAsync_WithValidData_ShouldCallAddAndSendExactlyOnce()
        {
          
            var mockRepo = new Mock<IEventRepository>();
            var mockNotification = new Mock<INotificationService>();

            Func<string, INotificationService> mockFactory = (channel) => mockNotification.Object;

            var eventService = new EventService(mockRepo.Object, mockFactory);

            var clubId = Guid.NewGuid();
            var title = "Test Event";
            var date = DateTime.Now.AddDays(1);
            var capacity = 50;
            var channel = "email";

            await eventService.CreateEventAsync(clubId, title, date, capacity, channel);

            mockRepo.Verify(r => r.AddAsync(It.IsAny<Event>()), Times.Once);

            mockNotification.Verify(n => n.Send(It.IsAny<string>()), Times.Once);
        }
    }
}