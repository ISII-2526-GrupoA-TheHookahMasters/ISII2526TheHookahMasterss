using System;

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
    [DataType(DataType.Currency)]
    [Display(Name = "Precio de compra")]
    public decimal Precio { get; set; }

}
