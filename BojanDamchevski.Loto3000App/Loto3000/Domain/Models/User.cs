using System.Collections.Generic;

namespace Domain.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<LotoNumber> LotoNumbers { get; set; }
        public Admin Admin { get; set; }
        public int AdminId { get; set; }
        public string Prize { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User()
        {
            LotoNumbers = new List<LotoNumber>();
        }
    }
}
