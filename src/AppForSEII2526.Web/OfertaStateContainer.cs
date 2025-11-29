using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class OfertaStateContainer
    {
        public OfertaForCreateDTO Oferta { get; private set; } = new OfertaForCreateDTO()
        {
            OfertaItems = new List<OfertaItemDTO>()
        };

        public float PrecioFinal
        {
            get
            {
                return (float)Oferta.OfertaItems.Sum(oi => oi.PrecioFinal);
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();


        public void AddHerramientaToOferta(HerramientasParaOfertaDTO herramienta)
        {
            if (!Oferta.OfertaItems.Any(oi => oi.HerramientaId == herramienta.Id))
                Oferta.OfertaItems.Add(new OfertaItemDTO()
                {
                    HerramientaId = herramienta.Id,
                    NombreHerramienta = herramienta.Nombre,
                    Material = herramienta.Material,
                    Precio = herramienta.Precio,
                    Fabricante = herramienta.Fabricante,
                }
            );
        }

        public void RemoveOfertaItemToOferta(OfertaItemDTO item)
        {
            Oferta.OfertaItems.Remove(item);
        }

        public void ClearOfertaCart()
        {
            Oferta.OfertaItems.Clear();
        }

        public void OfertaProcessed()
        {
            Oferta = new OfertaForCreateDTO()
            {
                OfertaItems = new List<OfertaItemDTO>()
            };
        }
    }
}
