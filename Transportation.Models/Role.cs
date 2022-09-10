using System.ComponentModel.DataAnnotations;

namespace Transportation.Models;

public class Role
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
}