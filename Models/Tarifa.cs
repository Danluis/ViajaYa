using System.ComponentModel.DataAnnotations;

namespace ViajaYa.Models;

public class Tarifa : IValidatableObject
{
    public int TarifaId { get; set; }

    [Required(ErrorMessage = "La ruta es obligatoria.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "La ruta debe tener entre 3 y 100 caracteres.")]
    public string Ruta { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime FechaInicio { get; set; }

    [Required(ErrorMessage = "La fecha final es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime FechaFinal { get; set; }

    [Required(ErrorMessage = "El tipo de aeronave es obligatorio.")]
    [StringLength(40, ErrorMessage = "El tipo de aeronave no puede superar los 40 caracteres.")]
    public string TipoAeronave { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "999999999", ErrorMessage = "El precio base debe ser mayor que cero.")]
    public decimal PrecioBase { get; set; }

    [Range(typeof(decimal), "0", "999999999", ErrorMessage = "El impuesto no puede ser negativo.")]
    public decimal Impuesto { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio.")]
    [StringLength(30, ErrorMessage = "El estado no puede superar los 30 caracteres.")]
    public string Estado { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (FechaInicio == default)
        {
            yield return new ValidationResult(
                "La fecha de inicio es obligatoria.",
                new[] { nameof(FechaInicio) });
        }

        if (FechaFinal == default)
        {
            yield return new ValidationResult(
                "La fecha final es obligatoria.",
                new[] { nameof(FechaFinal) });
        }

        if (FechaInicio != default && FechaFinal != default && FechaFinal <= FechaInicio)
        {
            yield return new ValidationResult(
                "La fecha final debe ser posterior a la fecha de inicio.",
                new[] { nameof(FechaFinal) });
        }
    }
}
