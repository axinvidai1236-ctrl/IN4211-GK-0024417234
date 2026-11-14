using System;
using EduHub.Domain.Entities;
using Xunit;

namespace EduHub.Tests
{
    public class RegistrationTests
    {
        [Fact]
        public void Confirm_AfterCancelled_ShouldThrowInvalidOperationException_AndStatusRemainsCancelled()
        {
            var memberId = Guid.NewGuid();
            var eventId = Guid.NewGuid();

            var r = Registration.Create(memberId, eventId);
            r.Cancel();

            // Sửa lại: Thêm dấu ngoặc nhọn { } để tránh lỗi CS0619
            var exception = Assert.Throws<InvalidOperationException>(() => { r.Confirm(); });

            Assert.Equal(RegistrationStatus.Cancelled, r.Status);
        }

        [Fact]
        public void Create_WithEmptyMemberId_ShouldThrowArgumentException()
        {
            var eventId = Guid.NewGuid();
            Assert.Throws<ArgumentException>(() => { Registration.Create(Guid.Empty, eventId); });
        }

        [Fact]
        public void Create_WithEmptyEventId_ShouldThrowArgumentException()
        {
            var memberId = Guid.NewGuid();
            Assert.Throws<ArgumentException>(() => { Registration.Create(memberId, Guid.Empty); });
        }

        [Fact]
        public void Cancel_WhenAlreadyCancelled_ShouldThrowInvalidOperationException()
        {
            var r = Registration.Create(Guid.NewGuid(), Guid.NewGuid());
            r.Cancel();

            Assert.Throws<InvalidOperationException>(() => { r.Cancel(); });
        }
    }
}