namespace AppForSEII2526.API.DTOs.CompraDTOs
{
    public class CompraDetailDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombreCliente")]
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text), Display(Name = "Nombre de Cliente")]
        public string NombreCliente { get; set; }

        [JsonPropertyName("apellidoCliente")]
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text), Display(Name = "Apellido de Cliente")]
        public string ApellidoCliente { get; set; }

        [JsonPropertyName("direccionEnvio")]
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text), Display(Name = "Dirección de envío")]
        public string DireccionEnvio { get; set; }

        [JsonPropertyName("fechaCompra")]
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha compra")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCompra { get; set; }

        [JsonPropertyName("precioTotal")]
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio de compra")]
        public float PrecioTotal { get; set; }

        [JsonPropertyName("tipoMetodoPago")]
        [Required]
        public TiposMetodoPago TipoMetodoPago { get; set; }

        public IList<CompraItemDTO> CompraItemsDTO { get; set; }

        public CompraDetailDTO(int id, string nombreCliente, string apellidoCliente, string direccionEnvio, DateTime fechaCompra, float precioTotal, IList<CompraItemDTO> compraItemsDTO)
        {
            Id = id;
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
            DireccionEnvio = direccionEnvio;
            FechaCompra = fechaCompra;
            PrecioTotal = precioTotal;
            CompraItemsDTO = compraItemsDTO;
        }
    }
}
