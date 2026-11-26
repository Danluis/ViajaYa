using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViajaYa.Models;

public class ReservaHotel
{
    [Key]
    public int ReservaHotelId { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un cliente")]
    public int ClienteId { get; set; }

    [ForeignKey(nameof(ClienteId))]
    public Cliente? Cliente { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un agente")]
    public int AgenteId { get; set; }

    [ForeignKey(nameof(AgenteId))]
    public Agente? Agente { get; set; }

    [Required(ErrorMessage = "La fecha de reserva es obligatoria")]
    public DateTime FechaReserva { get; set; } = DateTime.Today;

    [Required]
    [StringLength(20)]
    public string Estado { get; set; } = "Confirmada";

    public List<DetalleReservaHotel> Detalles { get; set; } = new();
}
