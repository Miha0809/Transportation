using System.ComponentModel.DataAnnotations;

namespace Transportation.Models;

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; }
}