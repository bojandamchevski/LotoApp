using System;
using System.Collections.Generic;

namespace DTOs.LotoNumbersDTOs
{
    public class LotoNumbersDTO
    {
        public List<int> LotoNumbers { get; set; } = new List<int>();
        public LotoNumbersDTO()
        {
            if (LotoNumbers.Count > 7)
            {
                throw new Exception("You can only enter 7 numbers.");
            }
        }
    }
}
