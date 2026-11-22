using System.ComponentModel.DataAnnotations;

namespace ViajaYa.Models;

public class DetalleReservaVuelo
{
    [Key]
    public int DetalleId { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    public int ReservaVueloId { get; set; }
    public ReservaVuelo? ReservaVuelo { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un vuelo.")]
    public int VueloId { get; set; }
    public Vuelo? Vuelo { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad de pasajes debe ser al menos 1.")]
    public int Cantidad { get; set; }

    public decimal Precio { get; set; }
}