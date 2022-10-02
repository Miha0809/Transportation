using System.ComponentModel.DataAnnotations;

namespace Transportation.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(128, MinimumLength = 5)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [StringLength(128, MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public virtual Role? Role { get; set; }

}
