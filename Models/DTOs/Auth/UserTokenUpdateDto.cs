namespace ProjectGreenLens.Models.DTOs.Auth
{
    public class UserTokenUpdateDto
    {
        public int id { get; set; }
        public bool isRevoked { get; set; }
    }
}
