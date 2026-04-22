using Microsoft.AspNetCore.Mvc;
using TrainingCenterAPI.Data;
using TrainingCenterAPI.Models;

namespace TrainingCenterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRooms([FromQuery] int? minCapacity, [FromQuery] bool? hasProjector, [FromQuery] bool? activeOnly)
        {
            var query = DataStore.Rooms.AsQueryable();

            if (minCapacity.HasValue)
                query = query.Where(r => r.Capacity >= minCapacity.Value);
                
            if (hasProjector.HasValue)
                query = query.Where(r => r.HasProjector == hasProjector.Value);
                
            if (activeOnly.HasValue && activeOnly.Value)
                query = query.Where(r => r.IsActive);

            return Ok(query.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetRoom(int id)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound($"Sala o ID {id} nie została znaleziona.");
            
            return Ok(room);
        }

        [HttpGet("building/{buildingCode}")]
        public IActionResult GetRoomsByBuilding(string buildingCode)
        {
            var rooms = DataStore.Rooms
                .Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase))
                .ToList();
                
            return Ok(rooms);
        }

        [HttpPost]
        public IActionResult CreateRoom([FromBody] Room room)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            room.Id = DataStore.Rooms.Any() ? DataStore.Rooms.Max(r => r.Id) + 1 : 1;
            DataStore.Rooms.Add(room);

            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, [FromBody] Room updatedRoom)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingRoom = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (existingRoom == null) return NotFound($"Sala o ID {id} nie istnieje.");

            existingRoom.Name = updatedRoom.Name;
            existingRoom.BuildingCode = updatedRoom.BuildingCode;
            existingRoom.Floor = updatedRoom.Floor;
            existingRoom.Capacity = updatedRoom.Capacity;
            existingRoom.HasProjector = updatedRoom.HasProjector;
            existingRoom.IsActive = updatedRoom.IsActive;

            return Ok(existingRoom);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound($"Sala o ID {id} nie istnieje.");
            var hasFutureReservations = DataStore.Reservations.Any(res => res.RoomId == id && res.Date >= DateTime.Today);
            if (hasFutureReservations)
            {
                return Conflict("Nie można usunąć sali, ponieważ posiada ona przyszłe rezerwacje.");
            }

            DataStore.Rooms.Remove(room);
            return NoContent();
        }
    }
}