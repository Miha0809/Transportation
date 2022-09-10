using System.ComponentModel.DataAnnotations;


namespace Transportation.Models.CargoCompany;

public class CargoCompany
{
    [Key] public int Id { get; set; }

    [Required]
    [StringLength(128, MinimumLength = 2)]
    [DataType(DataType.Text)]
    public string Name { get; set; }

    [Required]
    [StringLength(128, MinimumLength = 5)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [StringLength(128, MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public virtual DateTime? Created { get; set; }

    public virtual Role? Role { get; set; }
}
