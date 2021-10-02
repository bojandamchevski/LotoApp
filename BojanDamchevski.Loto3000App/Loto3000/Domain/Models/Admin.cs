using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Admin : BaseEntity
    {
        public string AdminName { get; set; }
        public List<Draw> Draws { get; set; }
        public List<User> Users { get; set; }
        public Admin()
        {
            AdminName = "Admin";
            Users = new List<User>();
        }

        public List<int> MakeDraw()
        {
            List<int> drawNumbers = new List<int>();
            for (int i = 0; i <= 37 && i >= 1; i++)
            {
                Random random = new Random();
                int randomNumber = random.Next(1, 37);
                drawNumbers.Add(randomNumber);
            }
            Draw draw = new Draw()
            {
                Session = new Session()
                {
                    LastDraw = DateTime.Now,
                    NextDraw = DateTime.Now.AddDays(45)
                }
            };
            for (int j = 0; j < drawNumbers.Count; j++)
            {
                WinningNumber winningNumber = new WinningNumber()
                {
                    WinningNum = drawNumbers[j]
                };
                draw.WinningNumbers.Add(winningNumber);
            }
            Draws.Add(draw);
            return drawNumbers;
        }
    }
}
