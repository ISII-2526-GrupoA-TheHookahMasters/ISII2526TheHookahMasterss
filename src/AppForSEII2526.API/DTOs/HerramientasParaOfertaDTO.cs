using System.Drawing;

namespace AppForSEII2526.API.DTOs
{
    public class HerramientasParaOfertaDTO
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Longitud máxima de 100 caracteres superada")]
        [JsonPropertyName("Nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Longitud máxima de 100 caracteres superada")]
        [JsonPropertyName("Material")]
        public string Material { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio de la Herramienta")]
        [JsonPropertyName("Precio")]
        public float Precio { get; set; }

        [JsonPropertyName("Fabricante")]
        public string Fabricante { get; set; }

        public HerramientasParaOfertaDTO(int id, string nombre, string material, float precio, string fabricante)
        {
            Id = id;
            Nombre = nombre;
            Material = material;
            Precio = precio;
            Fabricante = fabricante;
        }

        public override bool Equals(object? obj)
        {
            return obj is HerramientasParaOfertaDTO dTO &&
                   Id == dTO.Id &&
                   Nombre == dTO.Nombre &&
                   Material == dTO.Material &&
                   Precio == dTO.Precio &&
                   Fabricante == dTO.Fabricante;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre, Material, Precio, Fabricante);
        }


    }
}
