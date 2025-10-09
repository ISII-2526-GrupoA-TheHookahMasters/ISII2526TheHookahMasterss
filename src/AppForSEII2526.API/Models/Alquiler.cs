namespace AppForSEII2526.API.Models
{
    public class Alquiler
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text), Display(Name = "Dirección de envío")]
        public string DireccionEnvio { get; set; }

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
        public int Periodo => (FechaFin - FechaInicio).Days;

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio total")]
        public decimal PrecioTotal { get; set; }

        //Relaciones
        public List<AlquilarItem> AlquilarItem { get; set; }

        [Required]
        public TiposMetodoPago TiposMetodoPago { get; set; }
    }
}
