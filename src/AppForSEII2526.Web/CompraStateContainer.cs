using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class CompraStateContainer
    {
        public CompraForCreateDTO Compra { get; private set; } = new CompraForCreateDTO()
        {
            CompraItems = new List<CompraItemDTO>()
        };

        public float PrecioTotal
        {
            get
            {
                return (float)Compra.CompraItems.Sum(ci => ci.PrecioHerramienta * ci.Cantidad);
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();


        public void AddHerramientaToCompra(HerramientasParaComprarDTO herramienta)
        {
            if (!Compra.CompraItems.Any(ri => ri.HerramientaId == herramienta.Id))
                Compra.CompraItems.Add(new CompraItemDTO()
                {
                    HerramientaId = herramienta.Id,
                    NombreHerramienta = herramienta.Nombre,
                    MaterialHerramienta = herramienta.Material,
                    PrecioHerramienta = herramienta.Precio,
                }
            );
        }



        public void RemoveCompraItemToCompra(CompraItemDTO item)
        {
            Compra.CompraItems.Remove(item);

        }

        public void ClearCompraCart()
        {
            Compra.CompraItems.Clear();
        }

        public void CompraProcessed()
        {

            Compra = new CompraForCreateDTO()
            {
                CompraItems = new List<CompraItemDTO>()
            };
        }

    }
}