using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Prize : BaseEntity
    {
        public string PrizeType { get; set; }
        public int PrizeNumber { get; set; }
        public Prize()
        {
            Random random = new Random();
            PrizeNumber = random.Next(1, 37);
        }
    }
}
