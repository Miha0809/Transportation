using System.ComponentModel.DataAnnotations;

namespace Transportation.Models.Customer;

public class SizeCargo
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Range(0, 20000)]
    public uint Width { get; set; }

    [Required]
    [Range(0, 20000)]
    public uint Height { get; set; }

    [Required]
    [Range(0, 20000)]
    public uint Length { get; set; }

    [Required]
    [Range(0, 20000)]
    public uint Weight { get; set; }
}