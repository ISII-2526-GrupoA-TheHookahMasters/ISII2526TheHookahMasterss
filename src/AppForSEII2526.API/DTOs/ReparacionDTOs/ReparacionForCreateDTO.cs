
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
        public float PrecioTotal
        {
            get
            {
                return ReparacionItems.Sum(ri => ri.Precio * ri.Cantidad);
            }
        }

        [Required]
        public TiposMetodoPago TipoMetodoPago { get; set; }

        public IList<ReparacionItemDTO> ReparacionItems { get; set; }

        public ReparacionForCreateDTO(string nombreCliente, string apellidosCliente, DateTime fechaEntrega, DateTime fechaRecogida, TiposMetodoPago tipoMetodoPago, IList<ReparacionItemDTO> reparacionItems)
        {
            NombreCliente = nombreCliente;
            ApellidosCliente = apellidosCliente;
            FechaEntrega = fechaEntrega;
            FechaRecogida = fechaRecogida;
            TipoMetodoPago = tipoMetodoPago;
            ReparacionItems = reparacionItems;
            
        }
        public bool CompareDate(DateTime date1, DateTime date2)
        {
            return (date1.Subtract(date2) < new TimeSpan(0, 1, 0));
        }

        public override bool Equals(object? obj)
        {
            return obj is ReparacionForCreateDTO dTO &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidosCliente == dTO.ApellidosCliente &&
                   CompareDate(FechaEntrega, dTO.FechaEntrega) &&
                   CompareDate(FechaRecogida, dTO.FechaRecogida) && 
                   PrecioTotal == dTO.PrecioTotal &&
                   ReparacionItems.SequenceEqual(dTO.ReparacionItems) &&
                   TipoMetodoPago == dTO.TipoMetodoPago;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NombreCliente, ApellidosCliente, FechaEntrega, FechaRecogida, PrecioTotal, ReparacionItems, TipoMetodoPago);
        }
    }
}
