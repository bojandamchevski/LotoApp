using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Admin : BaseEntity
    {
        public string AdminName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Draw> Draws { get; set; }
        public List<User> Users { get; set; }
        public Admin()
        {
            AdminName = "Admin";
            Users = new List<User>();
            Draws = new List<Draw>();
        }
    }
}
