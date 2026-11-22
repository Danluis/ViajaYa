using System.ComponentModel.DataAnnotations;

namespace ViajaYa.Models;

public class Destino
{
    [Key]
    public int DestinoId { get; set; }

    [Required(ErrorMessage = "La ciudad es obligatoria.")]
    [StringLength(80, ErrorMessage = "La ciudad no puede superar los 80 caracteres.")]
    public string Ciudad { get; set; } = string.Empty;

    [Required(ErrorMessage = "El pais es obligatorio.")]
    [StringLength(60, ErrorMessage = "El pais no puede superar los 60 caracteres.")]
    public string Pais { get; set; } = string.Empty;

    [StringLength(300, ErrorMessage = "La descripcion no puede superar los 300 caracteres.")]
    public string? Descripcion { get; set; }
}
