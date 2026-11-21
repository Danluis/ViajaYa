using System.ComponentModel.DataAnnotations;

namespace ViajaYa.Models;

public class Reserva
{
    public int ReservaId { get; set; }

    [Required(ErrorMessage = "El codigo de reserva es obligatorio.")]
    [StringLength(20, MinimumLength = 4, ErrorMessage = "El codigo debe tener entre 4 y 20 caracteres.")]
    public string Codigo { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un cliente.")]
    public int ClienteId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un vuelo.")]
    public int VueloId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una aeronave.")]
    public int AeronaveId { get; set; }

    [Range(1, 1000, ErrorMessage = "La cantidad de pasajeros debe estar entre 1 y 1000.")]
    public int CantidadPasajeros { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio.")]
    [StringLength(30, ErrorMessage = "El estado no puede superar los 30 caracteres.")]
    public string Estado { get; set; } = string.Empty;

    public Cliente? Cliente { get; set; }

    public Vuelo? Vuelo { get; set; }

    public Aeronave? Aeronave { get; set; }

    public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
