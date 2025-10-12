namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(AlquilerId), nameof(HerramientaId))]
    public class AlquilarItem
    { 
        public int AlquilerId { get; set; }

        public int HerramientaId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cantidad mínima de alquiler es 1")]
        public int Cantidad { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio del alquiler")]
        public float Precio { get; set; }

        //Relaciones
        public Alquiler Alquiler { get; set; }
        public Herramienta Herramienta { get; set; }
    }
}
