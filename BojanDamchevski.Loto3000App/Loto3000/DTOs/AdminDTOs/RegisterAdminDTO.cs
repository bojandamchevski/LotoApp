namespace DTOs.AdminDTOs
{
    public class RegisterAdminDTO
    {
        public string AdminName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
        public string Role { get; set; }

        public RegisterAdminDTO()
        {
            Role = "Admin";
        }
    }
}
