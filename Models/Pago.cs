using System.ComponentModel.DataAnnotations;

namespace ViajaYa.Models;

public class Pago
{
    public int PagoId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una reserva.")]
    public int ReservaId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un cliente.")]
    public int ClienteId { get; set; }

    [Range(typeof(decimal), "0.01", "999999999", ErrorMessage = "El monto debe ser mayor que cero.")]
    public decimal Monto { get; set; }

    [Required(ErrorMessage = "La fecha del pago es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "El metodo de pago es obligatorio.")]
    [StringLength(40, ErrorMessage = "El metodo no puede superar los 40 caracteres.")]
    public string Metodo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El estado del pago es obligatorio.")]
    [StringLength(30, ErrorMessage = "El estado no puede superar los 30 caracteres.")]
    public string Estado { get; set; } = string.Empty;

    [StringLength(50, ErrorMessage = "El numero de comprobante no puede superar los 50 caracteres.")]
    public string? NumeroComprobante { get; set; }

    public Reserva? Reserva { get; set; }

    public Cliente? Cliente { get; set; }
}
