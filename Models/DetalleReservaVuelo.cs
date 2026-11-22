using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViajaYa.Models;

public class DetalleReservaVuelo
{
    [Key]
    public int DetalleId { get; set; }

    [Required]
    public int ReservaVueloId { get; set; }

    [ForeignKey(nameof(ReservaVueloId))]
    public ReservaVuelo? ReservaVuelo { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un vuelo")]
    public int VueloId { get; set; }

    [ForeignKey(nameof(VueloId))]
    public Vuelo? Vuelo { get; set; }

    [Required(ErrorMessage = "La cantidad de asientos es obligatoria")]
    [Range(1, 100, ErrorMessage = "Debe reservar al menos 1 asiento")]
    public int CantidadAsientos { get; set; } = 1;

    [Required]
    [Range(0, 100000)]
    public decimal PrecioUnitario { get; set; }
}
