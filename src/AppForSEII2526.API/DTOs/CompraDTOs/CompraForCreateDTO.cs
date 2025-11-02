namespace AppForSEII2526.API.DTOs.CompraDTOs
{
    public class CompraForCreateDTO
    {

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

        [JsonPropertyName("precioTotal")]
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio de compra")]
        public float PrecioTotal { get; set; }

        [JsonPropertyName("tipoMetodoPago")]
        [Required]
        public TiposMetodoPago TipoMetodoPago { get; set; }

        public IList<CompraItemDTO> CompraItems { get; set; }

        public CompraForCreateDTO(string nombreCliente, string apellidoCliente, string direccionEnvio, float precioTotal, TiposMetodoPago tipoMetodoPago, IList<CompraItemDTO> compraItems)
        {
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
            DireccionEnvio = direccionEnvio;
            PrecioTotal = precioTotal;
            TipoMetodoPago = tipoMetodoPago;
            CompraItems = compraItems;
        }
    }
}
