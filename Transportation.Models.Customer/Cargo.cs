using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Transportation.Models.Customer;

public class Cargo
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Range(0, uint.MaxValue)]
    public uint Price { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [StringLength(128, MinimumLength = 3)]
    public string Name { get; set; }

    [Required]
    public bool IsFree { get; set; }
    public virtual Address? Address { get; set; }
    public virtual SizeCargo? SizeCargo { get; set; }
}
