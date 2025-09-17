namespace ProjectGreenLens.Models.DTOs.Auth
{
    public class UserAddDto
    {
        public string username { get; set; } = null!;
        public string email { get; set; } = null!;
        public string passwordHash { get; set; } = null!;
        public int roleId { get; set; }
    }
}
