using System.ComponentModel.DataAnnotations;

namespace Transportation.Models;

public class RefreshToken
{
    public int Id { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    public int UserId { get; set; }
}