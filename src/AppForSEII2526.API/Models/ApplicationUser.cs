namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser {
    [Required]
    public string Nombre { get; set; }

    [Required]
    public string Apellidos { get; set; }

    [Required]
    public string Correo { get; set; }
}