using BookingAPIAlten_Core.Domain.Enum;

namespace BookingAPIAlten_Core.Domain.Model
{
    public class Room : IAggregateRoot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Beds { get; set; }
        public StatusRoomEnum Status { get; set; }
    }
}
