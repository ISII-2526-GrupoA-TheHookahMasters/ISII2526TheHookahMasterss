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
        public float PrecioTotal { get; set; }

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
        public AlquilerForCreateDTO( string nombreCliente, string apellidosCliente, string direccionEnvio, float precioTotal, DateTime fechaFin, DateTime fechaInicio, IList<AlquilarItemDTO> alquilerItems, TiposMetodoPago tipoMetodoPago)
        {
            NombreCliente = nombreCliente;
            ApellidosCliente = apellidosCliente;
            DireccionEnvio = direccionEnvio;
            PrecioTotal = precioTotal;
            FechaFin = fechaFin;
            FechaInicio = fechaInicio;
            Periodo = (FechaFin - FechaInicio).Days;
            AlquilerItems = alquilerItems;
            TipoMetodoPago = tipoMetodoPago;
        }
    }
}
