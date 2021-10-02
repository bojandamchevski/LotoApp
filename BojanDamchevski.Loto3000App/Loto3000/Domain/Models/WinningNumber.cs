namespace Domain.Models
{
    public class WinningNumber : BaseEntity
    {
        public int WinningNum { get; set; }
        public Draw Draw { get; set; }
        public int DrawId { get; set; }
    }
}
