namespace AppForSEII2526.API.DTOs
{
    public class HerramientasParaAlquilarDTO{
        [JsonPropertyName("Id")]
        public int Id { get; set; }


        [Required]
        [JsonPropertyName("Nombre")]
        [StringLength(100, ErrorMessage = "Longitud máxima de 100 caracteres superada")]
        public string Nombre { get; set; }

        [Required]
        [JsonPropertyName("Material")]
        [StringLength(100, ErrorMessage = "Longitud máxima de 100 caracteres superada")]
        public string Material { get; set; }

        [Required]
        [JsonPropertyName("Precio")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio de la Herramienta")]
        public float Precio { get; set; }

        [JsonPropertyName("Fabricante")]
        public string Fabricante { get; set; }


        public HerramientasParaAlquilarDTO (int id, string nombre ,string material, float precio, string fabricante)
        {
            Id = id;
            Nombre = nombre;
            Material = material;
            Precio = precio;
            Fabricante = fabricante;
        }


    }

}
