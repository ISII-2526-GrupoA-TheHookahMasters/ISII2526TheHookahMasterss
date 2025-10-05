using System;

public class AlquilarItem
{
    [Key]
    public int IdAlquiler;

    [Required]
    public int IdHerramienta;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Cantidad mínima de alquiler es 1")]
    public int Cantidad;

    [Required]
    [DataType(DataType.Currency)]
    [Display(Name = "Precio del alquiler")]
    public decimal Precio;
}
