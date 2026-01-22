
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    public class Room : BaseClass
    {
        [MaxLength(255)]
        public string Name { get; set; }
        public int Capacity { get; set; }
        [MaxLength(500)]
        public string Location { get; set; }
        public int? BuildId { get; set; }
        public bool? HaveProjector { get; set; } = false;
        
        [ForeignKey(nameof(BuildId))]
        public Building Building { get; set; }


        public ICollection<Session> Sessions { get; set; }
    }
}
