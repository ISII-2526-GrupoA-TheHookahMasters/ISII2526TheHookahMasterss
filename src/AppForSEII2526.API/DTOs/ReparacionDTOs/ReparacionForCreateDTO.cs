namespace AppForSEII2526.API.DTOs.ReparacionDTOs
{
    public class ReparacionForCreateDTO
    {
        [Required]
        [JsonPropertyName("nombreCliente")]
        public string NombreCliente { get; set; }

        [Required]
        [JsonPropertyName("apellidosCliente")]
        public string ApellidosCliente { get; set; }

        [Required]
        [JsonPropertyName("fechaEntrega")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha de Entrega")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaEntrega { get; set; }

        [Required]
        [JsonPropertyName("fechaRecogida")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha de Recogida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaRecogida { get; set; }

        [Required]
        [JsonPropertyName("precioTotal")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio Total")]
        public float PrecioTotal { get; set; }

        public IList<ReparacionItemDTO> ReparacionItems { get; set; }

        [Required]
        public TiposMetodoPago TipoMetodoPago { get; set; }

        public ReparacionForCreateDTO(string nombreCliente, string apellidosCliente, DateTime fechaEntrega, DateTime fechaRecogida, float precioTotal, TiposMetodoPago tipoMetodoPago, IList<ReparacionItemDTO> reparacionItems)
        {
            NombreCliente = nombreCliente;
            ApellidosCliente = apellidosCliente;
            FechaEntrega = fechaEntrega;
            FechaRecogida = fechaRecogida;
            PrecioTotal = precioTotal;
            TipoMetodoPago = tipoMetodoPago;
            ReparacionItems = reparacionItems;
            
        }


    }
}
