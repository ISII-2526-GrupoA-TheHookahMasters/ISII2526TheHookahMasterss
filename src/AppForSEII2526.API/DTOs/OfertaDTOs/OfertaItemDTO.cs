
namespace AppForSEII2526.API.DTOs.OfertaDTOs
{
    public class OfertaItemDTO
    {
        [JsonPropertyName("herramientaId")]
        public int HerramientaId { get; set; }

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

        public OfertaItemDTO(int herramientaId, float precioFinal, string nombreHerramienta,
                         float precio, string material, string fabricante)
        {
            HerramientaId = herramientaId;
            PrecioFinal = precioFinal;
            NombreHerramienta = nombreHerramienta;
            Precio = precio;
            Material = material;
            Fabricante = fabricante;
        }

        public override bool Equals(object? obj)
        {
            return obj is OfertaItemDTO dTO &&
                   HerramientaId == dTO.HerramientaId &&
                   PrecioFinal == dTO.PrecioFinal &&
                   NombreHerramienta == dTO.NombreHerramienta &&
                   Precio == dTO.Precio &&
                   Material == dTO.Material &&
                   Fabricante == dTO.Fabricante;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HerramientaId, PrecioFinal, NombreHerramienta, Precio, Material, Fabricante);
        }
    }
}

