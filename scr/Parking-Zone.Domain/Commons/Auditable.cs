namespace Parking_Zone.Domain.Entities
{
    public abstract class Auditable
    {
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdateAt { get; set; }
    }
}
