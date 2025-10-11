namespace AppForSEII2526.API.Models
{
    public class Oferta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha Final")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha Inicio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)] 
        public DateTime FechaInicio { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha Oferta")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)] 
        public DateTime FechaOferta { get; set; }

        //Relaciones 
        public List<OfertaItem> OfertaItems { get; set; }

        public ApplicationUser Usuario { get; set; }

        [Required]
        public TiposMetodoPago TiposMetodoPago { get; set; }

        public TiposDirigidaOferta? TiposDirigidaOferta { get; set; }
    }
}
