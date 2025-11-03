using AppForSEII2526.API.DTOs.OfertaDTOs;

namespace AppForSEII2526.API.DTOs
{
    public class OfertaDetailDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha Final")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [JsonPropertyName("fechaFinal")]
        public DateTime FechaFinal { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha Inicio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [JsonPropertyName("fechaInicio")]
        public DateTime FechaInicio { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha Oferta")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [JsonPropertyName("fechaOferta")]
        public DateTime FechaOferta { get; set; }

        [Required]
        [JsonPropertyName("tipoMetodoPago")]
        public TiposMetodoPago TipoMetodoPago { get; set; }

        [JsonPropertyName("tipoDirigidaOferta")]
        public TiposDirigidaOferta? TipoDirigidaOferta { get; set; }

        public IList<OfertaItemDTO> OfertaItems { get; set; }

        public OfertaDetailDTO(int id, DateTime fechaFinal, DateTime fechaInicio, DateTime fechaOferta,
                         TiposMetodoPago tipoMetodoPago, TiposDirigidaOferta? tipoDirigidaOferta, IList<OfertaItemDTO> ofertaItems)
        {
            Id = id;
            FechaFinal = fechaFinal;
            FechaInicio = fechaInicio;
            FechaOferta = fechaOferta;
            TipoMetodoPago = tipoMetodoPago;
            TipoDirigidaOferta = tipoDirigidaOferta;
            OfertaItems = ofertaItems;
        }

        protected bool CompareDate(DateTime date1, DateTime date2)
        {
            return (date1.Subtract(date2) < new TimeSpan(0, 1, 0));
        }

        public override bool Equals(object? obj)
        {
            return obj is OfertaDetailDTO dTO &&
                   Id == dTO.Id &&
                   CompareDate(FechaFinal, dTO.FechaFinal) &&
                   CompareDate(FechaInicio, dTO.FechaInicio) &&
                   CompareDate(FechaOferta, dTO.FechaOferta) &&
                   TipoMetodoPago == dTO.TipoMetodoPago &&
                   TipoDirigidaOferta == dTO.TipoDirigidaOferta &&
                   OfertaItems.SequenceEqual(dTO.OfertaItems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FechaFinal, FechaInicio, FechaOferta, TipoMetodoPago, TipoDirigidaOferta, OfertaItems);
        }
    }
}
