using System.ComponentModel.DataAnnotations;

namespace Transportation.Models.Customer;

public class Address
{
    [Required]
    [DataType(DataType.Text)]
    [StringLength(256, MinimumLength = 5)]
    public string From { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [StringLength(256, MinimumLength = 5)]
    public string To { get; set; }
}
