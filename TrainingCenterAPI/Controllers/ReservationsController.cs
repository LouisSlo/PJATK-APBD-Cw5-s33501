using Microsoft.AspNetCore.Mvc;
using TrainingCenterAPI.Data;
using TrainingCenterAPI.Models;

namespace TrainingCenterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetReservations([FromQuery] DateTime? date, [FromQuery] string? status, [FromQuery] int? roomId)
        {
            var query = DataStore.Reservations.AsQueryable();

            if (date.HasValue)
                query = query.Where(r => r.Date.Date == date.Value.Date);
                
            if (!string.IsNullOrEmpty(status))
                query = query.Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
                
            if (roomId.HasValue)
                query = query.Where(r => r.RoomId == roomId.Value);

            return Ok(query.ToList());
        }
        [HttpGet("{id}")]
        public IActionResult GetReservation(int id)
        {
            var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null) return NotFound($"Rezerwacja o ID {id} nie została znaleziona.");
            
            return Ok(reservation);
        }
        [HttpPost]
        public IActionResult CreateReservation([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);

            if (room == null) return NotFound("Podana sala nie istnieje.");

            if (!room.IsActive) return BadRequest("Nie można dokonać rezerwacji dla nieaktywnej sali.");

            bool isConflict = DataStore.Reservations.Any(r => 
                r.RoomId == reservation.RoomId && 
                r.Date.Date == reservation.Date.Date &&
                (reservation.StartTime < r.EndTime && reservation.EndTime > r.StartTime) 
            );

            if (isConflict) return Conflict("Rezerwacja koliduje czasowo z inną rezerwacją w tej sali.");

            reservation.Id = DataStore.Reservations.Any() ? DataStore.Reservations.Max(r => r.Id) + 1 : 1;
            DataStore.Reservations.Add(reservation);

            return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateReservation(int id, [FromBody] Reservation updatedReservation)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingReservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
            if (existingReservation == null) return NotFound($"Rezerwacja o ID {id} nie istnieje.");
            
            existingReservation.RoomId = updatedReservation.RoomId;
            existingReservation.OrganizerName = updatedReservation.OrganizerName;
            existingReservation.Topic = updatedReservation.Topic;
            existingReservation.Date = updatedReservation.Date;
            existingReservation.StartTime = updatedReservation.StartTime;
            existingReservation.EndTime = updatedReservation.EndTime;
            existingReservation.Status = updatedReservation.Status;

            return Ok(existingReservation);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null) return NotFound($"Rezerwacja o ID {id} nie istnieje.");

            DataStore.Reservations.Remove(reservation);
            return NoContent();
        }
    }
}