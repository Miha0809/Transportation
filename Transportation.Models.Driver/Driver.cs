
using System.ComponentModel.DataAnnotations;
using Transportation.Models.Customer;

namespace Transportation.Models.Driver;

public class Driver
{
    [Key]
    public int Id { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [StringLength(32, MinimumLength = 2)]
    public string Name { get; set; }

    [Required]
    [StringLength(128, MinimumLength = 5)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [StringLength(128, MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public bool IsFree { get; set; }

    public Cargo Cargo { get; set; }
    public HistoryCargo HistoryCargo { get; set; }
    public Role? Role { get; set; }
}
