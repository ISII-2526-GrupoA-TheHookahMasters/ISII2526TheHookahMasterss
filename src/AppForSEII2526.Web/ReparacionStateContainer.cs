using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class ReparacionStateContainer
    {
        public ReparacionForCreateDTO Reparacion { get; private set; } = new ReparacionForCreateDTO()
        {
            ReparacionItems = new List<ReparacionItemDTO>()
        };


        public float PrecioTotal
        {
            get
            {
                return (float)Reparacion.ReparacionItems.Sum(h => h.Precio * h.Cantidad);
            }
        }


        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();


        public void AddHerramientaToReparacion(HerramientasParaRepararDTO herramienta)
        {
            if (!Reparacion.ReparacionItems.Any(ri => ri.HerramientaId == herramienta.Id))
                Reparacion.ReparacionItems.Add(new ReparacionItemDTO()
                {
                    HerramientaId = herramienta.Id,
                    NombreHerramienta = herramienta.Nombre,
                    Precio = herramienta.Precio,
                    TiempoReparacion = herramienta.TiempoReparacion,
                }
            );
        }



        public void RemoveReparacionItemToReparar(ReparacionItemDTO item)
        {
            Reparacion.ReparacionItems.Remove(item);

        }

        public void ClearReparacionCart()
        {
            Reparacion.ReparacionItems.Clear();
        }

        public void ReparacionProcessed()
        {
            //we have finished the rental process so we create a new object without data
            Reparacion = new ReparacionForCreateDTO()
            {
                ReparacionItems = new List<ReparacionItemDTO>()
            };
        }

    }
}
