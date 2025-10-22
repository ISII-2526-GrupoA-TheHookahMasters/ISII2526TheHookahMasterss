using System.Globalization;

namespace AppForSEII2526.API.DTOs
{
    public class ReparacionItemDTO
    {
        [JsonPropertyName("reparacionId")]
        public int ReparacionId { get; set; }

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

        public ReparacionItemDTO(int reparacionId, string NombreHerramienta, float Precio, int Cantidad, string Descripcion)
        {
            this.ReparacionId = reparacionId;
            this.NombreHerramienta = NombreHerramienta;
            this.Precio = Precio;
            this.Cantidad = Cantidad;
            this.Descripcion = Descripcion;
        }
    }
}
