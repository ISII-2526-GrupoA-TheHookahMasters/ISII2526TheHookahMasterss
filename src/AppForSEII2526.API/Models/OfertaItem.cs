namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(OfertaId), nameof(HerramientaId))]
    public class OfertaItem
    {
        public int OfertaId { get; set; }

        public int HerramientaId { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
        public int Porcentaje { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Display(Name = "Precio final de la oferta")]
        public float PrecioFinal { get; set; }

        //Relaciones
        public Oferta Oferta { get; set; }
        public Herramienta Herramienta { get; set; }

        public OfertaItem(int ofertaId, int herramientaId, int porcentaje, float precioFinal, Oferta oferta, Herramienta herramienta)
        {
            OfertaId = ofertaId;
            HerramientaId = herramientaId;
            Porcentaje = porcentaje;
            PrecioFinal = precioFinal;
            Oferta = oferta;
            Herramienta = herramienta;
        }

        public OfertaItem(Oferta oferta, Herramienta herramienta)
        {
            Oferta = oferta;
            Herramienta = herramienta;
        }

        public OfertaItem()
        {
        }
    }
}
