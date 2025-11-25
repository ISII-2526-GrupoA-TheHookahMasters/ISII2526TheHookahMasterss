using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class AlquilerStateContainer
    {
        public AlquilerForCreateDTO Alquiler { get; private set; } = new AlquilerForCreateDTO()
        {
            AlquilerItems = new List<AlquilarItemDTO>()
        };

        public float PrecioTotal
        {
            get
            {
                int numberOfDays = (Alquiler.FechaFin - Alquiler.FechaInicio).Days;
                return (float) Alquiler.AlquilerItems.Sum(ai => ai.Precio * numberOfDays);
            }
        }

        public event Action? OnChange;


        private void NotifyStateChanged() => OnChange?.Invoke();


        public void AddHerramientaToAlquiler(HerramientasParaAlquilarDTO herramienta)
        {
            if (!Alquiler.AlquilerItems.Any(ai => ai.HerramientaId == herramienta.Id))
                Alquiler.AlquilerItems.Add(new AlquilarItemDTO()
                {
                    HerramientaId = herramienta.Id,
                    NombreHerramienta= herramienta.Nombre,
                    Material = herramienta.Material,
                    Precio = herramienta.Precio,
                }
            );
        }


        public void RemoveRentalItemToRent(AlquilarItemDTO item)
        {
            Alquiler.AlquilerItems.Remove(item);
        }

        public void ClearRentingCart()
        {
            Alquiler.AlquilerItems.Clear();
        }

        public void RentalProcessed()
        {
            //we have finished the rental process so we create a new object without data
            Alquiler = new AlquilerForCreateDTO()
            {
                AlquilerItems = new List<AlquilarItemDTO>()
            };
        }

    }
}
