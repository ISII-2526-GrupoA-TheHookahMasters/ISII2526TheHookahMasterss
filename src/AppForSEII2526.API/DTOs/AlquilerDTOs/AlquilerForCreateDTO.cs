
namespace AppForSEII2526.API.DTOs.AlquilerDTOs
{
    public class AlquilerForCreateDTO
    {
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

        [JsonPropertyName("precioTotal")]
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "precioTotal")]
        public float PrecioTotal { 
            get 
            {
                return AlquilerItems.Sum(a => a.Precio * Periodo);
            } 
        }

        [JsonPropertyName("fechaFin")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }

        [JsonPropertyName("fechaInicio")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [JsonPropertyName("periodo")]
        [Required]
        public int Periodo { get; set; }

        [JsonPropertyName("tipoMetodoPago")]
        [Required]
        public TiposMetodoPago TipoMetodoPago { get; set; }

        public IList<AlquilarItemDTO> AlquilerItems { get; set; }
        public AlquilerForCreateDTO( string nombreCliente, string apellidosCliente, string direccionEnvio, DateTime fechaFin, DateTime fechaInicio, IList<AlquilarItemDTO> alquilerItems, TiposMetodoPago tipoMetodoPago)
        {
            NombreCliente = nombreCliente;
            ApellidosCliente = apellidosCliente;
            DireccionEnvio = direccionEnvio;
            FechaFin = fechaFin;
            FechaInicio = fechaInicio;
            Periodo = (FechaFin - FechaInicio).Days;
            AlquilerItems = alquilerItems;
            TipoMetodoPago = tipoMetodoPago;
        }
        public bool CompareDate(DateTime date1, DateTime date2)
        {
            return date1.Subtract(date2) < new TimeSpan(0, 1, 0);
        }

        public override bool Equals(object? obj)
        {
            return obj is AlquilerForCreateDTO dTO &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidosCliente == dTO.ApellidosCliente &&
                   DireccionEnvio == dTO.DireccionEnvio &&
                   PrecioTotal == dTO.PrecioTotal &&
                   CompareDate(FechaFin, dTO.FechaFin) &&
                   CompareDate(FechaInicio, dTO.FechaInicio) &&
                   Periodo == dTO.Periodo &&
                   TipoMetodoPago == dTO.TipoMetodoPago &&
                   AlquilerItems.SequenceEqual(dTO.AlquilerItems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( NombreCliente, ApellidosCliente, DireccionEnvio, PrecioTotal, FechaFin, FechaInicio, TipoMetodoPago, AlquilerItems);
        }
    }
}
