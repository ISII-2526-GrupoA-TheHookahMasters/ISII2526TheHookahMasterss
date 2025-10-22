namespace AppForSEII2526.API.DTOs
{
    public class CompraItemDTO
    {
        [JsonPropertyName("compraId")]
        public int CompraId { get; set; }

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
        [Required]
        public string Descripcion { get; set; }

        [JsonPropertyName("cantidad")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cantidad minima de compra es 1")]
        public int Cantidad { get; set; }


        public CompraItemDTO(int compraId, string nombreHerramienta, string material, float precio, string descripcion, int cantidad)
        {
            CompraId = compraId;
            NombreHerramienta = nombreHerramienta;
            Material = material;
            Precio = precio;
            Descripcion = descripcion;
            Cantidad = cantidad;
        }
    }
}
