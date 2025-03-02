using PediVax.BusinessObjects.Enum;

namespace PediVax.BusinessObjects.DTO.ResponseDTO;

public class AuthResponseDTO
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public EnumList.Role Role { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; } 
    public double Expiration { get; set; } 
}