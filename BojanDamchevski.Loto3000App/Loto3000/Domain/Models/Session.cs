using System;

namespace Domain.Models
{
    public class Session : BaseEntity
    {
        public DateTime LastDraw { get; set; }
        public DateTime NextDraw { get; set; }
        public Draw Draw { get; set; }
        public int DrawId { get; set; }
    }
}