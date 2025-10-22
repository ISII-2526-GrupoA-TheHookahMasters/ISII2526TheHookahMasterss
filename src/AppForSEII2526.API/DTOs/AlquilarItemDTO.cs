namespace AppForSEII2526.API.DTOs
{
    public class AlquilarItemDTO
    {

        [JsonPropertyName("herramientaId")]
        public int HerramientaId { get; set; }

        [JsonPropertyName("nombreHerramienta")]
        [Required]
        public string NombreHerramienta { get; set; }

        [JsonPropertyName("material")]
        [Required]
        public string Material { get; set; }

        [JsonPropertyName("cantidad")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cantidad mínima de alquiler es 1")]
        public int Cantidad { get; set; }

        [JsonPropertyName("precio")]
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio del alquiler")]
        public float Precio { get; set; }


        public AlquilarItemDTO(int herramientaId,string nombreHerramienta, string material, int cantidad, float precio)
        {
            HerramientaId=herramientaId;
            NombreHerramienta = nombreHerramienta;
            Material = material;
            Cantidad = cantidad;
            Precio = precio;
        }



    }
}
