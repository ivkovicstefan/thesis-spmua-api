namespace SPMUA.Model.DTOs.Admin
{
    public class AdminRegisterDTO
    {
        public string AdminFirstName { get; set; } = String.Empty;
        public string AdminLastName { get; set; } = String.Empty;
        public string AdminEmail { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string PasswordHash { get; set; } = String.Empty;
    }
}
