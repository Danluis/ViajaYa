using System.ComponentModel.DataAnnotations;

namespace ViajaYa.Models;

public class Piloto
{
    public int PilotoId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 50 caracteres.")]
    public string Apellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "La licencia es obligatoria.")]
    [StringLength(50, ErrorMessage = "La licencia no puede superar los 50 caracteres.")]
    public string Licencia { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime FechaNacimiento { get; set; }

    [Required(ErrorMessage = "El telefono es obligatorio.")]
    [Phone(ErrorMessage = "El telefono no tiene un formato valido.")]
    [StringLength(20, ErrorMessage = "El telefono no puede superar los 20 caracteres.")]
    public string Telefono { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Introduce un correo valido.")]
    [StringLength(100, ErrorMessage = "El correo no puede superar los 100 caracteres.")]
    public string Correo { get; set; } = string.Empty;

    [Range(0, 100000, ErrorMessage = "Las horas de vuelo no pueden ser negativas.")]
    public int HorasVuelo { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio.")]
    [StringLength(30, ErrorMessage = "El estado no puede superar los 30 caracteres.")]
    public string Estado { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime? UltimoVuelo { get; set; }

    [DataType(DataType.Date)]
    public DateTime? ChequeoMedico { get; set; }

    [StringLength(40, ErrorMessage = "El tipo de aeronave no puede superar los 40 caracteres.")]
    public string? TipoAeronave { get; set; }

    [StringLength(500, ErrorMessage = "Las observaciones no pueden superar los 500 caracteres.")]
    public string? Observaciones { get; set; }

    public ICollection<Vuelo> Vuelos { get; set; } = new List<Vuelo>();
}
