using Domain.Entities;

namespace Domain.Entities
{
    public class Building : BaseClass
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SysBuildingId { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
