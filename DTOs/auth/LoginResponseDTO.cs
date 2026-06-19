namespace laTienda.DTOs.Auth
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public List<string> Roles { get; set; } = new();
    }
}