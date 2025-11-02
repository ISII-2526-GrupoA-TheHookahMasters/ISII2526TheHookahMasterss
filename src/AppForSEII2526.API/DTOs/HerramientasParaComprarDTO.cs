
namespace AppForSEII2526.API.DTOs
{
    public class HerramientasParaComprarDTO
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("nombre")]
        [StringLength(100, ErrorMessage = "Longitud máxima de 100 caracteres superada")]
        public string Nombre { get; set; }

        [Required]
        [JsonPropertyName("material")]
        [StringLength(100, ErrorMessage = "Longitud máxima de 100 caracteres superada")]
        public string Material { get; set; }

        [Required]
        [JsonPropertyName("precio")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio de la Herramienta")]
        public float Precio { get; set; }

        [Required]
        [JsonPropertyName("fabricante")]
        public string Fabricante { get; set; }


        public HerramientasParaComprarDTO(int id, string nombre, string material, float precio, string fabricante)
        {
            Id = id;
            Nombre = nombre;
            Material = material;
            Precio = precio;
            Fabricante = fabricante;
        }

        public override bool Equals(object? obj)
        {
            return obj is HerramientasParaComprarDTO dTO &&
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
