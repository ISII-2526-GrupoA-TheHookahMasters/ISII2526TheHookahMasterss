namespace AppForSEII2526.API.Models
{ 
    public class CompraItem
    {
        [Key]
	    public int IdCompra { get; set; }

        [Required]
	    public int IdHerramienta { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cantidad minima de compra es 1")]
        public int Cantidad { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio de compra")]
        public decimal Precio { get; set; }

        //Relaciones
        public Herramienta Herramienta { get; set; }
        public Compra Compra { get; set; }

    }
}
