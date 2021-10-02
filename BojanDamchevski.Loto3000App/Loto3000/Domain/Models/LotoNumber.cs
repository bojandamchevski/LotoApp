namespace Domain.Models
{
    public class LotoNumber : BaseEntity
    {
        public int LotoNumberChoice { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}