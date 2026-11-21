using System.ComponentModel.DataAnnotations;

namespace ViajaYa.Models;

public class Vuelo
{
    public int VueloId { get; set; }

    [Required(ErrorMessage = "El origen es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El origen debe tener entre 2 y 100 caracteres.")]
    public string Origen { get; set; } = string.Empty;

    [Required(ErrorMessage = "El destino es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El destino debe tener entre 2 y 100 caracteres.")]
    public string Destino { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "La hora de salida es obligatoria.")]
    public TimeSpan HoraSalida { get; set; }

    [Required(ErrorMessage = "La hora de llegada es obligatoria.")]
    public TimeSpan HoraLlegada { get; set; }

    [Range(1, 1440, ErrorMessage = "El tiempo estimado debe estar entre 1 y 1440 minutos.")]
    public int TiempoEstimado { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un piloto.")]
    public int PilotoId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una aeronave.")]
    public int AeronaveId { get; set; }

    public Piloto? Piloto { get; set; }

    public Aeronave? Aeronave { get; set; }

    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
