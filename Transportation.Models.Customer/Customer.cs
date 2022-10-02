using System.ComponentModel.DataAnnotations;

namespace Transportation.Models.Customer;

public class Customer : User
{
    [Required]
    [DataType(DataType.Text)]
    [StringLength(64, MinimumLength = 2)]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.MultilineText)]
    [StringLength(int.MaxValue, MinimumLength = 2)]
    public string Description { get; set; }

    public virtual List<Cargo>? Cargoes { get; set; }
}
