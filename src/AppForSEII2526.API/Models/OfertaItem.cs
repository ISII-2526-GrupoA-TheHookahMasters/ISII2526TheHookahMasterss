using System;

public class OfertaItem
{
	[Key]
	public int IdOferta { get; set; }

	[Required]
    public int IdHerramienta { get; set; }

	[Required]
    public int Porcentaje { get; set; }

	public float PrecioFinal { get; set; }
}
