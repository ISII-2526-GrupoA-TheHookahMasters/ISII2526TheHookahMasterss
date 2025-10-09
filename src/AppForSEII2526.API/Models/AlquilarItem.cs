namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(IdAlquiler), nameof(IdHerramienta))]
    public class AlquilarItem
    { 
        public int IdAlquiler { get; set; }

        public int IdHerramienta { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cantidad mínima de alquiler es 1")]
        public int Cantidad { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio del alquiler")]
        public decimal Precio { get; set; }

        //Relaciones
        public Alquiler Alquiler { get; set; }
        public Herramienta Herramienta { get; set; }
    }
}
