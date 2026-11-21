using System.ComponentModel.DataAnnotations;

namespace ViajaYa.Models;

public class Aeronave
{
    public int AeronaveId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(60, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 60 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El modelo es obligatorio.")]
    [StringLength(50, ErrorMessage = "El modelo no puede superar los 50 caracteres.")]
    public string Modelo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El tipo de aeronave es obligatorio.")]
    [StringLength(40, ErrorMessage = "El tipo no puede superar los 40 caracteres.")]
    public string Tipo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La matricula es obligatoria.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "La matricula debe tener entre 3 y 20 caracteres.")]
    public string Matricula { get; set; } = string.Empty;

    [Range(1, 1000, ErrorMessage = "La capacidad debe estar entre 1 y 1000 pasajeros.")]
    public int Capacidad { get; set; }

    [Range(typeof(decimal), "0.01", "999999999", ErrorMessage = "El precio debe ser mayor que cero.")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio.")]
    [StringLength(30, ErrorMessage = "El estado no puede superar los 30 caracteres.")]
    public string Estado { get; set; } = string.Empty;

    [StringLength(40, ErrorMessage = "El estado de mantenimiento no puede superar los 40 caracteres.")]
    public string? Mantenimiento { get; set; }

    [StringLength(100, ErrorMessage = "El tecnico asignado no puede superar los 100 caracteres.")]
    public string? TecnicoAsignado { get; set; }

    [StringLength(50, ErrorMessage = "El tipo de mantenimiento no puede superar los 50 caracteres.")]
    public string? TipoMantenimiento { get; set; }

    [DataType(DataType.Date)]
    public DateTime? FechaInicioMantenimiento { get; set; }

    [StringLength(500, ErrorMessage = "Las observaciones no pueden superar los 500 caracteres.")]
    public string? Observaciones { get; set; }

    public ICollection<Vuelo> Vuelos { get; set; } = new List<Vuelo>();

    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
