using System;

public class Herramienta
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo Nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "Longitud máxima de 100 caracteres superada")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El campo Material es obligatorio")]
    [StringLength(100, ErrorMessage = "Longitud máxima de 100 caracteres superada")]
    public string Material { get; set; }

    [Required(ErrorMessage = "El campo Precio es obligatorio")]
    [Range(0, double.MaxValue, ErrorMessage = "El Precio no puede ser negativo")]
    public decimal Precio { get; set; }

    public int TiempoReparacion { get; set; }
}
