using System;

public class Reparacion
{
    [Key]
    public int Id { get; set; }

    [StringLength(100, ErrorMessage = "El apellido no puede ser mas largo de 100 caracteres.")]
    public string NombreCliente { get; set; }

    [StringLength(100, ErrorMessage = "El nombre no puede ser mas largo de 100 caracteres.")]
    public string ApellidoCliente { get; set; }

    [DataType(DataType.Date), Display(Name = "Fecha de Entrega")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public string FechaEntrega { get; set; }

    [DataType(DataType.Date), Display(Name = "Fecha de Recogida")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public string FechaRecogida { get; set; }

    public string NumTelefono { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Precio Total")]
    public string PrecioTotal { get; set; }

    //Relaciones
    [Required]
    public TiposMetodoPago TiposMetodoPago { get; set; }

    public List<ReparacionItem> ReparacionItem { get; set; }
}
