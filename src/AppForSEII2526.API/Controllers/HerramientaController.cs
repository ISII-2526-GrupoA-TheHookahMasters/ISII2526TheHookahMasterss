using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HerramientaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<HerramientaController> _logger;

        public HerramientaController(ApplicationDbContext context, ILogger<HerramientaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientasParaOfertaDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientasParaOferta()
        {
            var herramientas = await _context.Herramienta
                .Select(h => new HerramientasParaOfertaDTO(h.Id, h.Nombre, h.Material, h.Precio, h.Fabricante.Nombre))
                .ToListAsync();
            return Ok(herramientas);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientasParaRepararDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientasParaReparar()
        {
            var herramientas = await _context.Herramienta
                .Select(h => new HerramientasParaRepararDTO(h.Id, h.Nombre, h.Material, h.Precio, h.Fabricante.Nombre, h.TiempoReparacion))
                .ToListAsync();
            return Ok(herramientas);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientasParaRepararDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientasPorNombreTiempoRep(string nombre, int tiemporeparacion)
        {
            var herramientas = await _context.Herramienta
                .Include(herramienta => herramienta.Fabricante)
                .Where(h => h.Nombre.Contains(nombre) || h.TiempoReparacion == tiemporeparacion)
                .Select(h => new HerramientasParaRepararDTO(h.Id, h.Nombre, h.Material, h.Precio, h.Fabricante.Nombre, h.TiempoReparacion))
                .ToListAsync();
            return Ok(herramientas);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientasParaOfertaDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientasPorFabricantePrecio(string? fabricante, float? precio)
        {
            var herramientas = await _context.Herramienta
                .Include(herramienta => herramienta.Fabricante)
                .Where(h => h.Fabricante.Nombre.Contains(fabricante) || h.Precio == precio)
                .Select(h => new HerramientasParaOfertaDTO(h.Id, h.Nombre, h.Material, h.Precio, h.Fabricante.Nombre))
                .ToListAsync();
            return Ok(herramientas);
        }
    }
}
