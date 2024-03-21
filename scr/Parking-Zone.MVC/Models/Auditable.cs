namespace Parking_Zone.MVC.Models
{
    public abstract class Auditable
    {
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdateAt { get; set; }
    }
}
