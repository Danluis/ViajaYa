using System.ComponentModel.DataAnnotations;

namespace ViajaYa.Models;

public class ReservaVuelo
{
    [Key]
    public int ReservaVueloId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un cliente.")]
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un agente.")]
    public int AgenteId { get; set; }
    public Agente? Agente { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    public DateTime Fecha { get; set; } = DateTime.Now;

    public string Observacion { get; set; } = string.Empty;

    public decimal Total { get; set; }

    public List<DetalleReservaVuelo> Detalles { get; set; } = new List<DetalleReservaVuelo>();
}