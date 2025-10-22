namespace AppForSEII2526.API.DTOs
{
    public class OfertaItemDTO
    {
        [JsonPropertyName("ofertaId")]
        public int OfertaId { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Display(Name = "Precio final de la oferta")]
        [JsonPropertyName("precioFinal")]
        public float PrecioFinal { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Longitud máxima de 100 caracteres superada")]
        [JsonPropertyName("nombreHerramienta")]
        public string NombreHerramienta { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio de la Herramienta")]
        [JsonPropertyName("precio")]
        public float Precio { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Longitud máxima de 100 caracteres superada")]
        [JsonPropertyName("material")]
        public string Material { get; set; }

        [JsonPropertyName("fabricante")]
        public string Fabricante { get; set; }

        public OfertaItemDTO(int ofertaId, float precioFinal, string nombreHerramienta,
                         float precio, string material, string fabricante)
        {
            OfertaId = ofertaId;
            PrecioFinal = precioFinal;
            NombreHerramienta = nombreHerramienta;
            Precio = precio;
            Material = material;
            Fabricante = fabricante;
        }
    }
}

