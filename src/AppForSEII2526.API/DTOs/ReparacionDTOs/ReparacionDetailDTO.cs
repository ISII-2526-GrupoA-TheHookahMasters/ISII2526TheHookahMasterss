
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.ReparacionDTOs
{
    public class ReparacionDetailDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

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

        public IList<ReparacionItemDTO> ReparacionItems { get; set; }

        public ReparacionDetailDTO(int id, string nombreCliente, string apellidoCliente, DateTime fechaEntrega, DateTime fechaRecogida, IList<ReparacionItemDTO> reparacionItems)
        {
            Id = id;
            NombreCliente = nombreCliente;
            ApellidosCliente = apellidoCliente;
            FechaEntrega = fechaEntrega;
            FechaRecogida = fechaRecogida;
            ReparacionItems = reparacionItems;

        }
        public bool CompareDate(DateTime date1, DateTime date2)
        {
            return (date1.Subtract(date2) < new TimeSpan(0, 1, 0));
        }

        public override bool Equals(object? obj)
        {
            return obj is ReparacionDetailDTO dTO &&
                   Id == dTO.Id &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidosCliente == dTO.ApellidosCliente &&
                   CompareDate(FechaEntrega, dTO.FechaEntrega) &&
                   CompareDate(FechaRecogida, dTO.FechaRecogida) &&
                   ReparacionItems.SequenceEqual(dTO.ReparacionItems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, NombreCliente, ApellidosCliente, FechaEntrega, FechaRecogida, ReparacionItems);
        }
    }
}
