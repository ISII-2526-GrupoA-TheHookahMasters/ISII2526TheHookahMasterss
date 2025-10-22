using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfertaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<OfertaController> _logger;

        public OfertaController(ApplicationDbContext context, ILogger<OfertaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<OfertaDetailDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetOfertasPorId(int id)
        {
            var oferta = await _context.Oferta
                .Where(o => o.Id == id)
                    .Include(o => o.OfertaItems)
                        .ThenInclude(oi => oi.Herramienta)
                            .ThenInclude(h => h.Fabricante)
                            
                .Select(o => new OfertaDetailDTO(o.Id, o.FechaFinal, o.FechaInicio, o.FechaOferta,
                o.TipoMetodoPago, o.TipoDirigidaOferta, o.OfertaItems
                            .Select(oi => new OfertaItemDTO(oi.HerramientaId, oi.PrecioFinal, oi.Herramienta.Nombre, 
                                oi.Herramienta.Precio, oi.Herramienta.Material, 
                                oi.Herramienta.Fabricante.Nombre)).ToList<OfertaItemDTO>()))
                .FirstOrDefaultAsync();

            if (oferta == null)
            {
                _logger.LogError($"Error: Oferta con id {id} no existe");
                return NotFound();
            }

            return Ok(oferta);
        }
    }
}