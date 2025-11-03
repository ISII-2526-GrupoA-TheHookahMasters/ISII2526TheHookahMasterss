namespace AppForSEII2526.API.Models
{
    public class Compra
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text), Display(Name = "Dirección de envío")]
        public string DireccionEnvio { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha compra")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCompra { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency), Display(Name = "Precio de compra")]
        public float PrecioTotal { get; set; }

        //Relaciones
        public ApplicationUser Usuario { get; set; }

        public List<CompraItem> CompraItems { get; set; }

        [Required]
        public TiposMetodoPago TipoMetodoPago { get; set; }

        public Compra(ApplicationUser usuario, string direccionEnvio, DateTime fechaCompra, TiposMetodoPago tipoMetodoPago, List<CompraItem> compraItems)
        {
            Usuario = usuario;
            DireccionEnvio = direccionEnvio;
            FechaCompra = fechaCompra;
            TipoMetodoPago = tipoMetodoPago;
            CompraItems = compraItems;
        }

        public Compra()
        { 
        }
    }
}