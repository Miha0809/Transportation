using System.ComponentModel.DataAnnotations;
using Transportation.Models.Customer;

namespace Transportation.Models.Driver;

public class HistoryCargo
{
    [Key]
    public int Id { get; set; }

    public virtual List<Cargo> Cargoes { get; set; }
}
