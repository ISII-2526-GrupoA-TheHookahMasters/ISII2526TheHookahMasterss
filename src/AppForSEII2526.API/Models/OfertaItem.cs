using System;

public class OfertaItem
{
	[Key]
	public int IdOferta { get; set; }

	[Required]
    public int IdHerramienta { get; set; }

	[Required]
    [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
    public int Porcentaje { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    [Display(Name = "Precio final de la oferta")]
    public decimal PrecioFinal { get; set; }

    //Relaciones
    public Oferta Oferta { get; set; }
    public Herramienta Herramienta { get; set; }
}
