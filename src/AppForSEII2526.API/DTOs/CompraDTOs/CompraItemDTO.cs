
namespace AppForSEII2526.API.DTOs
{
    public class CompraItemDTO
    {
        [JsonPropertyName("HerramientaId")]
        public int HerramientaId { get; set; }

        [JsonPropertyName("nombreHerramienta")]
        [Required]
        [StringLength(100, ErrorMessage = "Longitud máxima de 100 caracteres superada")]
        public string NombreHerramienta { get; set; }

        [JsonPropertyName("materialHerramienta")]
        [Required]
        [StringLength(100, ErrorMessage = "Longitud máxima de 100 caracteres superada")]
        public string Material { get; set; }

        [JsonPropertyName("precioHerramienta")]
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio de la Herramienta")]
        public float Precio { get; set; }

        [JsonPropertyName("descripcionHerramienta")]
        
        public string Descripcion { get; set; }

        [JsonPropertyName("cantidad")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cantidad minima de compra es 1")]
        public int Cantidad { get; set; }


        public CompraItemDTO(int herramientaId, string nombreHerramienta, string material, float precio, string descripcion, int cantidad)
        {
            HerramientaId = herramientaId;
            NombreHerramienta = nombreHerramienta;
            Material = material;
            Precio = precio;
            Descripcion = descripcion;
            Cantidad = cantidad;
        }

        public override bool Equals(object? obj)
        {
            return obj is CompraItemDTO dTO &&
                   HerramientaId == dTO.HerramientaId &&
                   NombreHerramienta == dTO.NombreHerramienta &&
                   Material == dTO.Material &&
                   Precio == dTO.Precio &&
                   Descripcion == dTO.Descripcion &&
                   Cantidad == dTO.Cantidad;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HerramientaId, NombreHerramienta, Material, Precio, Descripcion, Cantidad);
        }
    }
}
