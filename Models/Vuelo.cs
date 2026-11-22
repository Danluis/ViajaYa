using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViajaYa.Models;

public class Vuelo
{
    [Key]
    public int VueloId { get; set; }

    [Required(ErrorMessage = "El numero de vuelo es obligatorio")]
    [StringLength(20)]
    public string NumeroVuelo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe seleccionar una aerolinea")]
    public int AerolineaId { get; set; }

    [ForeignKey(nameof(AerolineaId))]
    public Aerolinea? Aerolinea { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un destino")]
    public int DestinoId { get; set; }

    [ForeignKey(nameof(DestinoId))]
    public Destino? Destino { get; set; }

    [Required(ErrorMessage = "La fecha de salida es obligatoria")]
    public DateTime FechaSalida { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "La fecha de llegada es obligatoria")]
    public DateTime FechaLlegada { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Los asientos disponibles son obligatorios")]
    [Range(0, 1000, ErrorMessage = "Los asientos deben estar entre 0 y 1000")]
    public int AsientosDisponibles { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0, 100000, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal Precio { get; set; }
}
