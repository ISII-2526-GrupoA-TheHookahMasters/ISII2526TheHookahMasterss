using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [ProducesResponseType(typeof(IList<Herramienta>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientasParaAlquilar()
        {
            IList<Herramienta> herramientas = await _context.Herramienta
                .ToListAsync();
            return Ok(herramientas);
        }


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientasParaAlquilarDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientasParaAlquilarDTO()
        {
            var herramientas = await _context.Herramienta
                .Select(h => new HerramientasParaAlquilarDTO(h.Id, h.Nombre, h.Material, h.Precio, h.Fabricante.Nombre))
                .ToListAsync();
            return Ok(herramientas);
        }


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientasParaAlquilarDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientasNombreMaterial(string? nombre, string? material)
        {
            var herramientas = await _context.Herramienta
                .Include(herramienta =>  herramienta.Fabricante)
                .Where(h=> h.Nombre.Equals(nombre) || h.Material.Equals(material))
                .Select(h => new HerramientasParaAlquilarDTO(h.Id, h.Nombre, h.Material, h.Precio, h.Fabricante.Nombre))
                .ToListAsync();
            return Ok(herramientas);
        }
    }


}




