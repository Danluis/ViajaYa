using System.ComponentModel.DataAnnotations;

namespace ViajaYa.Models;

public class DetalleReservaHotel
{
    [Key]
    public int DetalleId { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    public int ReservaHotelId { get; set; }
    public ReservaHotel? ReservaHotel { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un hotel.")]
    public int HotelId { get; set; }
    public Hotel? Hotel { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad de habitaciones debe ser al menos 1.")]
    public int Cantidad { get; set; }

    public decimal Precio { get; set; }
}