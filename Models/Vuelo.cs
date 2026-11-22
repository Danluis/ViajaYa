using System.ComponentModel.DataAnnotations;

namespace ViajaYa.Models;

public class Vuelo
{
    [Key]
    public int VueloId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una aerolinea.")]
    public int AerolineaId { get; set; }
    public Aerolinea? Aerolinea { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un destino.")]
    public int DestinoId { get; set; }
    public Destino? Destino { get; set; }

    [Required(ErrorMessage = "El numero de vuelo es obligatorio.")]
    [StringLength(20, ErrorMessage = "El numero de vuelo no puede superar los 20 caracteres.")]
    public string NumeroVuelo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Range(typeof(decimal), "0.01", "999999999", ErrorMessage = "El precio debe ser mayor que cero.")]
    public decimal Precio { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Los asientos disponibles no pueden ser negativos.")]
    public int AsientosDisponibles { get; set; } = 50;
}
