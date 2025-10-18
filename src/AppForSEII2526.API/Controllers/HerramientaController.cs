using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppForSEII2526.API.Data;
using AppForSEII2526.API.DTOs;

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

        // Devuelve la informacion directamente de la BBDD
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<Herramienta>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientas()
        {
            IList<Herramienta> herramientas = await _context.Herramienta
                .ToListAsync();
            return Ok(herramientas);
        }

        // Devuelve la informacion mapeada a DTOs
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientasParaComprarDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientas_conDTOs()
        {
            var herramientas = await _context.Herramienta
                .Select(h => new HerramientasParaComprarDTO(h.Id, h.Nombre, h.Material, h.Precio, h.Fabricante.Nombre))
                .ToListAsync();
            return Ok(herramientas);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientasParaComprarDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientas_filtroMaterialPrecioDTOs(string? filtroMaterial, double? filtroPrecio)
        {
            IList<HerramientasParaComprarDTO> herramientas = await _context.Herramienta
                .Include(h => h.Fabricante)
                .Where(h => (filtroMaterial == null || h.Material.Contains(filtroMaterial)) 
                    && (filtroPrecio == null || h.Precio <= filtroPrecio))
                .OrderBy(h => h.Precio)
                .Select(h => new HerramientasParaComprarDTO(h.Id, h.Nombre, h.Material, h.Precio, h.Fabricante.Nombre))
                .ToListAsync();
            return Ok(herramientas);
        }

    }
}
