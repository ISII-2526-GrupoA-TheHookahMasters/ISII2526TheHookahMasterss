namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(ReparacionId), nameof(HerramientaId))]
    public class ReparacionItem
    {
        public int ReparacionId { get; set; }

        public int HerramientaId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cantidad minima de compra es 1")]
        public int Cantidad { get; set; }

        public string? Descripcion { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio")]
        public float Precio { get; set; }

        //Relaciones
        public Reparacion Reparacion { get; set; }
        public Herramienta Herramienta { get; set; }

        public ReparacionItem(int reparacionId, int herramientaId, int cantidad, float precio, string? descripcion = null)
        {
            ReparacionId = reparacionId;
            HerramientaId = herramientaId;
            Cantidad = cantidad;
            Precio = precio;
            Descripcion = descripcion;
        }

        public ReparacionItem()
        {
        }
    }
}