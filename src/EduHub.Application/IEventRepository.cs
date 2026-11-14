using System.Threading.Tasks;
using EduHub.Domain.Entities;

namespace EduHub.Application.Interfaces
{
    public interface IEventRepository
    {
        Task AddAsync(Event ev);
    }
}