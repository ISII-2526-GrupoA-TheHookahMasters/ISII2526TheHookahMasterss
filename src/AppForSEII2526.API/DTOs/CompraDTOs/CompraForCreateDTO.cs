
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
        public float PrecioTotal
        {
            get
            {

                return CompraItems.Sum(ci => ci.Precio * ci.Cantidad);

            }
        }

        [JsonPropertyName("tipoMetodoPago")]
        [Required]
        public TiposMetodoPago TipoMetodoPago { get; set; }

        [JsonPropertyName("correoElectronico")]
        [StringLength(100, ErrorMessage = "El correo electrónico no puede tener más de 100 caracteres.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string? CorreoElectronico { get; set; }

        [JsonPropertyName("numTelefono")]
        [Range(100000000, 999999999, ErrorMessage = "El número de teléfono debe tener 9 dígitos (ej: 612345678).")]
        public int? NumTelefono { get; set; }


        public IList<CompraItemDTO> CompraItems { get; set; }

        public CompraForCreateDTO(string nombreCliente, string apellidoCliente, string direccionEnvio, TiposMetodoPago tipoMetodoPago, IList<CompraItemDTO> compraItems, string? correoElectronico = null, int? numTelefono = null)
        {
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
            DireccionEnvio = direccionEnvio;
            TipoMetodoPago = tipoMetodoPago;
            CompraItems = compraItems;
            CorreoElectronico = correoElectronico;
            NumTelefono = numTelefono;
        }

        public override bool Equals(object? obj)
        {
            return obj is CompraForCreateDTO dTO &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidoCliente == dTO.ApellidoCliente &&
                   DireccionEnvio == dTO.DireccionEnvio &&
                   PrecioTotal == dTO.PrecioTotal &&
                   TipoMetodoPago == dTO.TipoMetodoPago &&
                   CompraItems.SequenceEqual(dTO.CompraItems) &&
                   CorreoElectronico == dTO.CorreoElectronico &&
                   NumTelefono == dTO.NumTelefono;
                   
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                NombreCliente,
                ApellidoCliente,
                DireccionEnvio,
                TipoMetodoPago,
                CompraItems,
                CorreoElectronico,
                NumTelefono
            );
        }
    }
}
