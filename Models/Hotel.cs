using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViajaYa.Models;

public class Hotel
{
    [Key]
    public int HotelId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La direccion es obligatoria")]
    [StringLength(150)]
    public string Direccion { get; set; } = string.Empty;

    [Required(ErrorMessage = "La categoria es obligatoria")]
    [Range(1, 5, ErrorMessage = "La categoria debe estar entre 1 y 5 estrellas")]
    public int Categoria { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un destino")]
    public int DestinoId { get; set; }

    [ForeignKey(nameof(DestinoId))]
    public Destino? Destino { get; set; }

    [Required(ErrorMessage = "Las habitaciones disponibles son obligatorias")]
    [Range(0, 1000, ErrorMessage = "Las habitaciones deben estar entre 0 y 1000")]
    public int HabitacionesDisponibles { get; set; }

    [Required(ErrorMessage = "El precio por noche es obligatorio")]
    [Range(0, 100000, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal PrecioPorNoche { get; set; }
}
