namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(CompraId), nameof(HerramientaId))]
    public class CompraItem
    {
	    public int CompraId { get; set; }
        
	    public int HerramientaId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cantidad minima de compra es 1")]
        public int Cantidad { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio de compra")]
        public float Precio { get; set; }

        [Required]
        public string Descripcion { get; set; }

        //Relaciones
        public Herramienta Herramienta { get; set; }
        public Compra Compra { get; set; }
    }
}
