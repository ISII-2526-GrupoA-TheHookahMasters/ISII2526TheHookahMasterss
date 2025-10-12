namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(IdOferta), nameof(IdHerramienta))]
    public class OfertaItem
    {
        public int IdOferta { get; set; }

        public int IdHerramienta { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
        public int Porcentaje { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Display(Name = "Precio final de la oferta")]
        public decimal PrecioFinal { get; set; }

        //Relaciones
        public Oferta Oferta { get; set; }
        public Herramienta Herramienta { get; set; }
    }
}
