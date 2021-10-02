using System.Collections.Generic;

namespace Domain.Models
{
    public class Draw : BaseEntity
    {
        public Session Session { get; set; }
        public int SessionId { get; set; }
        public Admin Admin { get; set; }
        public int AdminId { get; set; }
        public List<WinningNumber> WinningNumbers { get; set; }

        public Draw()
        {
            WinningNumbers = new List<WinningNumber>();
        }

    }
}
