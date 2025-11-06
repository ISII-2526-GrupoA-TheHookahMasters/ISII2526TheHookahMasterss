using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.OfertaDTOs
{
    public class OfertaItemDTO
    {
        [JsonPropertyName("herramientaId")]
        public int HerramientaId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Longitud máxima de 100 caracteres superada")]
        [JsonPropertyName("nombreHerramienta")]
        public string NombreHerramienta { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Longitud máxima de 100 caracteres superada")]
        [JsonPropertyName("material")]
        public string Material { get; set; }

        [JsonPropertyName("fabricante")]
        public string Fabricante { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio de la Herramienta")]
        [JsonPropertyName("precio")]
        public float Precio { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
        [JsonPropertyName("porcentaje")]
        public int Porcentaje { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Display(Name = "Precio final de la oferta")]
        [JsonPropertyName("precioFinal")]
        public float PrecioFinal { 
            get {
                return Precio * (1 - (Porcentaje / 100.0f));
            } 
        }

        public OfertaItemDTO(int herramientaId, string nombreHerramienta, string material,
                           string fabricante, float precio, int porcentaje)
        {
            HerramientaId = herramientaId;
            NombreHerramienta = nombreHerramienta;
            Material = material;
            Fabricante = fabricante;
            Precio = precio;
            Porcentaje = porcentaje;
        }

        public override bool Equals(object? obj)
        {
            return obj is OfertaItemDTO dTO &&
                   HerramientaId == dTO.HerramientaId &&
                   NombreHerramienta == dTO.NombreHerramienta &&
                   Material == dTO.Material &&
                   Fabricante == dTO.Fabricante &&
                   Precio == dTO.Precio &&
                   Porcentaje == dTO.Porcentaje &&
                   PrecioFinal == dTO.PrecioFinal;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HerramientaId, NombreHerramienta, Material, Fabricante, Precio, Porcentaje, PrecioFinal);
        }
    }
}
