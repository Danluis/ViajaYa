using System.ComponentModel.DataAnnotations;

namespace ViajaYa.Models;

public class Aerolinea
{
    [Key]
    public int AerolineaId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El pais es obligatorio.")]
    [StringLength(60, ErrorMessage = "El pais no puede superar los 60 caracteres.")]
    public string Pais { get; set; } = string.Empty;
}
