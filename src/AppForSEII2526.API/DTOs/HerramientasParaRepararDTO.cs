using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.API.DTOs
{
    public class HerramientasParaRepararDTO
    {
        [Key]
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
        [JsonPropertyName("Fabricante")]
        public string Fabricante { get; set; }

        [Required]
        [JsonPropertyName("Precio")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio de la Herramienta")]
        public float Precio { get; set; }

        [JsonPropertyName("TiempoReparacion")]

        public int TiempoReparacion { get; set; }

        public HerramientasParaRepararDTO(int id, string nombre, string material, float precio, string fabricante, int tiemporeparacion)
        {
            Id = id;
            Nombre = nombre;
            Material = material;
            Precio = precio;
            Fabricante = fabricante;
            TiempoReparacion = tiemporeparacion;
        }

        public override bool Equals(object? obj)
        {
            return obj is HerramientasParaRepararDTO dTO &&
                   Id == dTO.Id &&
                   Nombre == dTO.Nombre &&
                   Material == dTO.Material &&
                   Fabricante == dTO.Fabricante &&
                   Precio == dTO.Precio &&
                   TiempoReparacion == dTO.TiempoReparacion;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre, Material, Fabricante, Precio, TiempoReparacion);
        }
    }
}
