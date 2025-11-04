
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.AlquilerDTOs
{
    public class AlquilerDetailDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombreCliente")]
        [Required]
        public string NombreCliente { get; set; }

        [JsonPropertyName("apellidosCliente")]
        [Required]
        public string ApellidosCliente { get; set; }

        [JsonPropertyName("direccionEnvio")]
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text), Display(Name = "Dirección de envío")]
        public string DireccionEnvio { get; set; }

        [JsonPropertyName("fechaAlquiler")]
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaAlquiler { get; set; }

        [JsonPropertyName("precioTotal")]
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "precioTotal")]
        public float PrecioTotal
        {
            get
            {
                return AlquilerItems.Sum(ai => ai.Precio * Periodo);
            }
        }

        [JsonIgnore]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }

        [JsonIgnore]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [JsonPropertyName("periodo")]
        [Required]
        public int Periodo {
            get
            {
                return (FechaFin - FechaInicio).Days;
            }
        }

        public IList<AlquilarItemDTO> AlquilerItems {  get; set; }

        public AlquilerDetailDTO(int id, string nombreCliente, string apellidosCliente, string direccionEnvio, DateTime fechaAlquiler, DateTime fechaFin, DateTime fechaInicio, IList<AlquilarItemDTO> alquilerItems)
        {
            Id = id;
            NombreCliente = nombreCliente;
            ApellidosCliente = apellidosCliente;
            DireccionEnvio = direccionEnvio;
            FechaAlquiler = fechaAlquiler;
            FechaFin = fechaFin;
            FechaInicio = fechaInicio;
            AlquilerItems = alquilerItems;
        }

        public bool CompareDate(DateTime date1, DateTime date2)
        {
            return date1.Subtract(date2) < new TimeSpan(0, 1, 0);
        }

        public override bool Equals(object? obj)
        {
            return obj is AlquilerDetailDTO dTO &&
                   Id == dTO.Id &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidosCliente == dTO.ApellidosCliente &&
                   DireccionEnvio == dTO.DireccionEnvio &&
                   CompareDate(FechaAlquiler, dTO.FechaAlquiler) &&
                   CompareDate(FechaFin, dTO.FechaFin) &&
                   CompareDate(FechaInicio, dTO.FechaInicio) &&
                   AlquilerItems.SequenceEqual(dTO.AlquilerItems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, NombreCliente, ApellidosCliente, DireccionEnvio, FechaAlquiler, FechaFin, FechaInicio, AlquilerItems);
        }
    }

}
