namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser {
    [Required]
    [DataType(System.ComponentModel.DataAnnotations.DataType.Text), Display(Name = "Nombre de Cliente")]
    public string Nombre { get; set; }

    [Required]
    [DataType(System.ComponentModel.DataAnnotations.DataType.Text), Display(Name = "Apellido de Cliente")]
    public string Apellido { get; set; }

    [DataType(System.ComponentModel.DataAnnotations.DataType.PhoneNumber), Display(Name = "Número de teléfono")]
    public int? Telefono { get; set; }

    [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress), Display(Name = "Correo electrónico")]
    public string? CorreoElectronico { get; set; }

    //Relaciones
    public List<Compra> Compras { get; set; }
    public List<Reparacion> Reparaciones { get; set; }
    public List<Oferta> Ofertas { get; set; }
    public List<Alquiler> Alquileres { get; set; }
}