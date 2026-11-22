using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViajaYa.Models;

public class DetalleReservaHotel
{
    [Key]
    public int DetalleId { get; set; }

    [Required]
    public int ReservaHotelId { get; set; }

    [ForeignKey(nameof(ReservaHotelId))]
    public ReservaHotel? ReservaHotel { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un hotel")]
    public int HotelId { get; set; }

    [ForeignKey(nameof(HotelId))]
    public Hotel? Hotel { get; set; }

    [Required(ErrorMessage = "La cantidad de habitaciones es obligatoria")]
    [Range(1, 50, ErrorMessage = "Debe reservar al menos 1 habitacion")]
    public int CantidadHabitaciones { get; set; } = 1;

    [Required(ErrorMessage = "La cantidad de noches es obligatoria")]
    [Range(1, 60, ErrorMessage = "Debe reservar al menos 1 noche")]
    public int CantidadNoches { get; set; } = 1;

    [Required]
    [Range(0, 100000)]
    public decimal PrecioUnitario { get; set; }
}
