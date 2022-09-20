using BookingAPIAlten.Core.DomainObjects;
using BookingAPIAlten.Domain.Enum;

namespace BookingAPIAlten.Domain
{
    public class Room : IAggregateRoot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Beds { get; set; }
        public StatusRoomEnum Status { get; set; }

        protected Room() { }

        public Room(int id, string name, int beds, StatusRoomEnum status)
        {
            Id = id;
            Name = name;
            Beds = beds;
            Status = status;
        }
    }
}
