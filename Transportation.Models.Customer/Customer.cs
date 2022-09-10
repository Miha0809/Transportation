using System.ComponentModel.DataAnnotations;

namespace Transportation.Models.Customer;

public class Customer
{
    [Key]
    public int Id { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [StringLength(64, MinimumLength = 2)]
    public string Name { get; set; }

    [Required]
    [StringLength(128, MinimumLength = 5)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [StringLength(128, MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public virtual Cargo? Cargo { get; set; }
    public virtual Role? Role { get; set; }

}
