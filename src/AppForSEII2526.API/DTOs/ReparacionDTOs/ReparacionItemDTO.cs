using System.Globalization;

namespace AppForSEII2526.API.DTOs.ReparacionDTOs
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

        public override bool Equals(object? obj)
        {
            return obj is ReparacionItemDTO dTO &&
                   HerramientaId == dTO.HerramientaId &&
                   NombreHerramienta == dTO.NombreHerramienta &&
                   Precio == dTO.Precio &&
                   Cantidad == dTO.Cantidad &&
                   Descripcion == dTO.Descripcion;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HerramientaId, NombreHerramienta, Precio, Cantidad, Descripcion);
        }
    }
}
