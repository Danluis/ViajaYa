using System.ComponentModel.DataAnnotations;

namespace ViajaYa.Models;

public class Cliente
{
    [Key]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "La cedula es obligatoria")]
    [StringLength(20)]
    public string Cedula { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El telefono es obligatorio")]
    [StringLength(20)]
    public string Telefono { get; set; } = string.Empty;

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato de email invalido")]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;
}
