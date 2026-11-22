using System.ComponentModel.DataAnnotations;

namespace ViajaYa.Models;

public class ReservaHotel
{
    [Key]
    public int ReservaHotelId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un cliente.")]
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un agente.")]
    public int AgenteId { get; set; }
    public Agente? Agente { get; set; }

    [Required(ErrorMessage = "La fecha de entrada es obligatoria.")]
    public DateTime FechaEntrada { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "La fecha de salida es obligatoria.")]
    public DateTime FechaSalida { get; set; } = DateTime.Now.AddDays(1);

    public string Observacion { get; set; } = string.Empty;

    public decimal Total { get; set; }

    public List<DetalleReservaHotel> Detalles { get; set; } = new List<DetalleReservaHotel>();
}