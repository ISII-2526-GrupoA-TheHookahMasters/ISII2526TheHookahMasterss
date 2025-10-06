namespace AppForSEII2526.API.Models
{
    public class ReparacionItem
    {
        [Key]
        public int IdReparacion { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cantidad minima de compra es 1")]
        public int Cantidad { get; set; }

        public string? Descripcion { get; set; }

        [Required]
        public int IdHerramienta { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio")]
        public decimal Precio { get; set; }

        //Relaciones
        public Reparacion Reparacion { get; set; }
        public Herramienta Herramienta { get; set; }
    }
}