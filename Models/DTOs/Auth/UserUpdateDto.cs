namespace ProjectGreenLens.Models.DTOs.Auth
{
    public class UserUpdateDto
    {
        public int id { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }
        public string? passwordHash { get; set; }
        public int roleId { get; set; }
    }
}
