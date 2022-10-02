using System.ComponentModel.DataAnnotations;
using Transportation.Models.Customer;

namespace Transportation.Models.Driver;

public class Driver : User
{
    [Required]
    [DataType(DataType.Text)]
    [StringLength(32, MinimumLength = 2)]
    public string Name { get; set; }

    public bool IsFree { get; set; }

    public virtual Cargo Cargo { get; set; }
    public virtual HistoryCargo HistoryCargo { get; set; }
}
