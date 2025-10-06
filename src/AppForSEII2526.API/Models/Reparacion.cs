namespace AppForSEII2526.API.Models
{
    public class Reparacion
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "El apellido no puede ser mas largo de 100 caracteres.")]
        public string NombreCliente { get; set; }

        [StringLength(100, ErrorMessage = "El nombre no puede ser mas largo de 100 caracteres.")]
        public string ApellidoCliente { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha de Entrega")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaEntrega { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha de Recogida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaRecogida { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.PhoneNumber), Display(Name = "Número de teléfono")]
        public string NumTelefono { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio Total")]
        public decimal PrecioTotal { get; set; }

        //Relaciones
        [Required]
        public TiposMetodoPago TiposMetodoPago { get; set; }

        public List<ReparacionItem> ReparacionItem { get; set; }
    }
}
