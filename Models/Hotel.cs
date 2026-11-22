using System.ComponentModel.DataAnnotations;

namespace ViajaYa.Models;

public class Hotel
{
    [Key]
    public int HotelId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un destino.")]
    public int DestinoId { get; set; }
    public Destino? Destino { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La direccion es obligatoria.")]
    [StringLength(150, ErrorMessage = "La direccion no puede superar los 150 caracteres.")]
    public string Direccion { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "Las habitaciones disponibles no pueden ser negativas.")]
    public int HabitacionesDisponibles { get; set; } = 20;
}
