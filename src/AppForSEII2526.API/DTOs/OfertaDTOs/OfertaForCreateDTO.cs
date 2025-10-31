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
        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
        [JsonPropertyName("porcentaje")]
        public int Porcentaje { get; set; }

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
    }
}
