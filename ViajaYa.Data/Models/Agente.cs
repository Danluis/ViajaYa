using System.ComponentModel.DataAnnotations;

namespace ViajaYa.Models;

public class Agente
{
    [Key]
    public int AgenteId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El codigo es obligatorio.")]
    [StringLength(20, ErrorMessage = "El codigo no puede superar los 20 caracteres.")]
    public string Codigo { get; set; } = string.Empty;
}
