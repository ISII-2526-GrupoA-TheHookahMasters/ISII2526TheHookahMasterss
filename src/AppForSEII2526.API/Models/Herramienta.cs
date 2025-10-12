namespace AppForSEII2526.API.Models
{
    public class Herramienta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Longitud máxima de 100 caracteres superada")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Longitud máxima de 100 caracteres superada")]
        public string Material { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio de la Herramienta")]
        public float Precio { get; set; }

        public int TiempoReparacion { get; set; }

        //Relaciones
        public Fabricante Fabricante { get; set; }
        public List<CompraItem> CompraItems { get; set; }
        public List<AlquilarItem> AlquilarItems { get; set; }
        public List<OfertaItem> OfertaItems { get; set; }
        public List<ReparacionItem> ReparacionItems { get; set; }
    }
}