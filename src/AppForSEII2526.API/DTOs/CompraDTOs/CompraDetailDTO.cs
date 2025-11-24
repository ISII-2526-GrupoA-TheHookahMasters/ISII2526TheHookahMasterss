
using AppForSEII2526.API.Models;

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
        public float PrecioTotal { get { 
            
            return CompraItems.Sum(ci => ci.Precio * ci.Cantidad);

            }
        }

        [JsonPropertyName("tipoMetodoPago")]
        [Required]
        public TiposMetodoPago TipoMetodoPago { get; set; }

        public IList<CompraItemDTO> CompraItems { get; set; }

        public CompraDetailDTO(int id, string nombreCliente, string apellidoCliente, string direccionEnvio, DateTime fechaCompra, IList<CompraItemDTO> compraItems)
        {
            Id = id;
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
            DireccionEnvio = direccionEnvio;
            FechaCompra = fechaCompra;
            CompraItems = compraItems;
        }

        protected bool CompareDate(DateTime date1, DateTime date2)
        {
            return (date1.Subtract(date2) < new TimeSpan(0, 1, 0));
        }

        public override bool Equals(object? obj)
        {
            return obj is CompraDetailDTO dTO &&
                   Id == dTO.Id &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidoCliente == dTO.ApellidoCliente &&
                   DireccionEnvio == dTO.DireccionEnvio &&
                   CompareDate(FechaCompra, dTO.FechaCompra) &&
                   PrecioTotal == dTO.PrecioTotal &&
                   TipoMetodoPago == dTO.TipoMetodoPago &&
                   CompraItems.SequenceEqual(dTO.CompraItems);

        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, NombreCliente, ApellidoCliente, DireccionEnvio, FechaCompra, PrecioTotal, TipoMetodoPago, CompraItems);
        }
    }
}
