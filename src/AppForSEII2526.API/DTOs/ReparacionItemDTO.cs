using System.Globalization;

namespace AppForSEII2526.API.DTOs
{
    public class ReparacionItemDTO
    {
        [JsonPropertyName("herramientaId")]
        public int HerramientaId { get; set; }

        [Required]
        [JsonPropertyName("nombreHerramienta")]
        public string NombreHerramienta { get; set; }

        [Required]
        [JsonPropertyName("precio")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio")]
        public float Precio { get; set; }

        [Required]
        [JsonPropertyName("cantidad")]
        [Range(1, int.MaxValue, ErrorMessage = "Cantidad minima de compra es 1")]
        public int Cantidad { get; set; }

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }

        public ReparacionItemDTO(int herramientaId, string nombreHerramienta, float precio, int cantidad, string descripcion)
        {
            HerramientaId = herramientaId;
            NombreHerramienta = nombreHerramienta;
            Precio = precio;
            Cantidad = cantidad;
            Descripcion = descripcion;
        }
    }
}
