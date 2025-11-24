namespace AppForSEII2526.API.DTOs.OfertaDTOs
{
    public class OfertaForCreateDTO
    {
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
        [JsonPropertyName("tipoMetodoPago")]
        public TiposMetodoPago TipoMetodoPago { get; set; }

        [JsonPropertyName("tipoDirigidaOferta")]
        public TiposDirigidaOferta? TipoDirigidaOferta { get; set; }

        public IList<OfertaItemDTO> OfertaItems { get; set; }

        public OfertaForCreateDTO(DateTime fechaFinal, DateTime fechaInicio,
                         TiposMetodoPago tipoMetodoPago, TiposDirigidaOferta? tipoDirigidaOferta, IList<OfertaItemDTO> ofertaItems)
        {
            FechaFinal = fechaFinal;
            FechaInicio = fechaInicio;
            TipoMetodoPago = tipoMetodoPago;
            TipoDirigidaOferta = tipoDirigidaOferta;
            OfertaItems = ofertaItems;
        }

        public bool CompareDate(DateTime date1, DateTime date2)
        {
            return (date1.Subtract(date2) < new TimeSpan(0, 1, 0));
        }

        public override bool Equals(object? obj)
        {
            return obj is OfertaForCreateDTO dTO &&
                   CompareDate(FechaFinal, dTO.FechaFinal) &&
                   CompareDate(FechaInicio, dTO.FechaInicio) &&
                   TipoMetodoPago == dTO.TipoMetodoPago &&
                   TipoDirigidaOferta == dTO.TipoDirigidaOferta &&
                   OfertaItems.SequenceEqual(dTO.OfertaItems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FechaFinal, FechaInicio, TipoMetodoPago, TipoDirigidaOferta, OfertaItems);
        }
    }
}