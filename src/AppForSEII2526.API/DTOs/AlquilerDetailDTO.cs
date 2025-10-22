namespace AppForSEII2526.API.DTOs
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
        public float PrecioTotal { get; set; }

        [JsonIgnore]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }

        [JsonIgnore]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [JsonPropertyName("periodo")]
        [Required]
        public int Periodo { get; set; }

        public IList<AlquilarItemDTO> AlquilerItemDTO {  get; set; }
        public AlquilerDetailDTO(int id, string nombreCliente, string apellidosCliente, string direccionEnvio, DateTime fechaAlquiler, float precioTotal, DateTime fechaFin, DateTime fechaInicio, IList<AlquilarItemDTO> alquilerItemDTO)
        {
            Id = id;
            NombreCliente = nombreCliente;
            ApellidosCliente = apellidosCliente;
            DireccionEnvio = direccionEnvio;
            FechaAlquiler = fechaAlquiler;
            PrecioTotal = precioTotal;
            FechaFin = fechaFin;
            FechaInicio = fechaInicio;
            Periodo = (FechaFin - FechaInicio).Days;
            AlquilerItemDTO = alquilerItemDTO;
           
        }
    }

}
