using System;

public class Alquiler{
    [Key]
    public int Id { get; set; }

    [Required]
    public string NombreCliente { get; set; }

    [Required]
    public string ApellidoCliente { get; set; }

    [Required]
    public string DireccionEnvio { get; set; }

    [Required]
    public string Correo { get; set; }

    [Required]
    public int NumeroTelefono { get; set; }

    [Required]
    [DataType(System.ComponentModel.DataAnnotations.DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime FechaAlquiler { get; set; }

    [Required]
    [DataType(System.ComponentModel.DataAnnotations.DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime FechaFin { get; set; }

    [Required]
    [DataType(System.ComponentModel.DataAnnotations.DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime FechaInicio { get; set; }

    [Required]
    public int Periodo { get; set; }

    [Required]
    public decimal PrecioTotal { get; set; }
}
