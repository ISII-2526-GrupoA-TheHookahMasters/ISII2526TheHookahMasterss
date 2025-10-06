using System;

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
    [DataType(DataType.Currency)]
    [Display(Name = "Precio")]
    public float Precio { get; set; }

    //Relaciones
    public Reparacion Reparacion { get; set; }

    public Herramienta Herramienta { get; set; }
}
