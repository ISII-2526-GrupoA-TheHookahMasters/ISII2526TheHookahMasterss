namespace AppForSEII2526.API.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text), Display(Name = "Nombre de Cliente")]
        public string NombreCliente { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text), Display(Name = "Apellido de Cliente")]
        public string ApellidoCliente { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.PhoneNumber), Display(Name = "Número de teléfono")]
        public int? Telefono { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress), Display(Name = "Correo electrónico")]
        public string? CorreoElectronico { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text), Display(Name = "Dirección de envío")]
        public string DireccionEnvio { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha compra")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaCompra { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio de compra")]
        public decimal PrecioTotal { get; set; }

        //Relaciones
        public List<CompraItem> CompraItem { get; set; }
        public TiposMetodoPago TipoMetodoPago { get; set; }
    }
}