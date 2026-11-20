using Microsoft.AspNetCore.Mvc;
using EduHub.Domain.Entities;
using EduHub.Application.Interfaces;

namespace EduHub.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _repository;

        public EventsController(IEventRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("clubs/{clubId}/events")]
        public IActionResult GetEventsByClub(Guid clubId)
        {
            return Ok(new List<Event>());
        }

        [HttpGet("events/{id}")]
        public IActionResult GetEventById(Guid id)
        {
            return NotFound("Event not found");
        }

        [HttpPost("clubs/{clubId}/events")]
        public async Task<IActionResult> CreateEvent(Guid clubId, [FromBody] CreateEventRequest request)
        {
            try
            {
                var ev = Event.Create(clubId, request.Title, request.Date, request.Capacity);
                await _repository.AddAsync(ev);
                return CreatedAtAction(nameof(GetEventById), new { id = ev.Id }, ev);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("events/{id}")]
        public IActionResult DeleteEvent(Guid id)
        {
            return NoContent();
        }
    }

    public record CreateEventRequest(string Title, DateTime Date, int Capacity);
}