using System;

public class Compra
{
	[Key]
	public int Id { get; set; }

	[Required]
    public string NombreCliente { get; set; }

	[Required]
    public string ApellidoCliente { get; set; }

    public int? Telefono { get; set; }

    public string? CorreoElectronico { get; set; }

	[Required]
	public string DireccionEnvio { get; set; }

	[Required]
    [DataType(DataType.Date), Display(Name = "Fecha compra")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public string FechaCompra { get; set; }

	[Required]
    [DataType(DataType.Currency)]
    [Display(Name = "Precio de compra")]
    public decimal PrecioTotal { get; set; }


    public List<CompraItem> CompraItem { get; set; }
    public TiposMetodoPago TipoMetodoPago { get; set; }
}
