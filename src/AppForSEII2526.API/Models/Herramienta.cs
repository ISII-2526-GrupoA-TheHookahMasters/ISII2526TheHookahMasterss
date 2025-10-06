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

    [DataType(DataType.Currency)]
    [Display(Name = "Precio de la Herramienta")]
    public decimal Precio { get; set; }

    public int TiempoReparacion { get; set; }

    //Relaciones
    public List<CompraItem> CompraItem { get; set; }
    public List<AlquilarItem> AlquilarItem { get; set; }
    public List<OfertaItem> OfertaItem { get; set; }
    public List<ReparacionItem> ReparacionItem { get; set; }
    public Fabricante Fabricante { get; set; }
}
