using System.ComponentModel.DataAnnotations;

namespace Transportation.Models.CargoCompany;

public class CargoCompany : User
{
    [Required]
    [StringLength(128, MinimumLength = 2)]
    [DataType(DataType.Text)]
    public string Name { get; set; }

    [Required]
    [StringLength(int.MaxValue, MinimumLength = 2)]
    [DataType(DataType.MultilineText)]
    public string Description { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public virtual DateTime? Created { get; set; }
    public virtual List<Driver.Driver> Drivers { get; set; }
}
