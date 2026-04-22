using TrainingCenterAPI.Models;

namespace TrainingCenterAPI.Data
{
    public static class DataStore
    {
        public static List<Room> Rooms { get; set; } = new()
        {
            new Room { Id = 1, Name = "Sala A1", BuildingCode = "A", Floor = 1, Capacity = 30, HasProjector = true, IsActive = true },
            new Room { Id = 2, Name = "Lab 204", BuildingCode = "B", Floor = 2, Capacity = 24, HasProjector = true, IsActive = true },
            new Room { Id = 3, Name = "Sala B1", BuildingCode = "B", Floor = 1, Capacity = 15, HasProjector = false, IsActive = true },
            new Room { Id = 4, Name = "Magazyn", BuildingCode = "C", Floor = 0, Capacity = 5, HasProjector = false, IsActive = false },
            new Room { Id = 5, Name = "Aula", BuildingCode = "A", Floor = 0, Capacity = 100, HasProjector = true, IsActive = true }
        };

        public static List<Reservation> Reservations { get; set; } = new()
        {
            new Reservation { Id = 1, RoomId = 2, OrganizerName = "Anna Kowalska", Topic = "Warsztaty z HTTP i REST", Date = new DateTime(2026, 5, 10), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 30, 0), Status = "confirmed" },
            new Reservation { Id = 2, RoomId = 1, OrganizerName = "Jan Nowak", Topic = "Szkolenie C#", Date = new DateTime(2026, 5, 11), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(16, 0, 0), Status = "planned" },
            new Reservation { Id = 3, RoomId = 5, OrganizerName = "Zarząd", Topic = "Spotkanie roczne", Date = new DateTime(2026, 6, 1), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(14, 0, 0), Status = "confirmed" },
            new Reservation { Id = 4, RoomId = 3, OrganizerName = "Kasia Wiśniewska", Topic = "Konsultacje HR", Date = new DateTime(2026, 5, 10), StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0), Status = "planned" }
        };
    }
}