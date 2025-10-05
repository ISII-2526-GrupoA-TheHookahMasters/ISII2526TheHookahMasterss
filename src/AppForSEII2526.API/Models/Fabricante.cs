using System;

public class Fabricante
{
	[Key]
	public int Id { get; set; }

    [Required(ErrorMessage = "El campo Nombre es obligatorio")] 
    [StringLength(100, ErrorMessage = "Error: Longitud máxima permitida de 100 caracteres")]
    public string Nombre { get; set; }
}
