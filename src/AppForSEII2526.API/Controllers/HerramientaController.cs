using AppForSEII2526.API.Data;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Data;
using AppForSEII2526.API.DTOs.ReparacionDTOs;

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
        [ProducesResponseType(typeof(IList<HerramientasParaAlquilarDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientasParaAlquilar()
        {
            var herramientas = await _context.Herramienta
                .Select(h => new HerramientasParaAlquilarDTO(h.Id, h.Nombre, h.Material, h.Precio, h.Fabricante.Nombre))
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

        // Caso de Uso Alquilar Herramientas
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientasParaAlquilarDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientasNombreMaterial(string? nombre, string? material)
        {
            var herramientas = await _context.Herramienta
                .Include(herramienta =>  herramienta.Fabricante)
                .Where(h => (h.Nombre.Contains(nombre) || nombre == null)
                && (h.Material == material || material == null))
                .Select(h => new HerramientasParaAlquilarDTO(h.Id, h.Nombre, h.Material, h.Precio, h.Fabricante.Nombre))
                .ToListAsync();
            return Ok(herramientas);
        }
        
        // Caso de Uso Comprar Herramientas
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientasParaComprarDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientasMaterialPrecio(string? filtroMaterial, double? filtroPrecio)
        {
            IList<HerramientasParaComprarDTO> herramientas = await _context.Herramienta
                .Include(h => h.Fabricante)
                .Where(h => (filtroMaterial == null || h.Material.Contains(filtroMaterial)) 
                    && (filtroPrecio == null || h.Precio == filtroPrecio))
                .Select(h => new HerramientasParaComprarDTO(h.Id, h.Nombre, h.Material, h.Precio, h.Fabricante.Nombre))
                .ToListAsync();
            return Ok(herramientas);
        }
                
        // Caso de Uso Reparar Herramientas
        [HttpGet]
        [Route("[action]")]        
        [ProducesResponseType(typeof(IList<HerramientasParaRepararDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientasPorNombreTiempoRep(string? nombre, int? tiemporeparacion)
        {
            var herramientas = await _context.Herramienta
                .Include(herramienta => herramienta.Fabricante)
                .Where(h => (h.Nombre.Contains(nombre) || nombre == null)
                    && (h.TiempoReparacion == tiemporeparacion || tiemporeparacion == null))
                .Select(h => new HerramientasParaRepararDTO(h.Id, h.Nombre, h.Material, h.Precio, h.Fabricante.Nombre, h.TiempoReparacion))
                .ToListAsync();
            return Ok(herramientas);
        }

        // Caso de Uso Oferta Herramientas
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientasParaOfertaDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientasPorFabricantePrecio(string? fabricante, float? precio)
        {
            var herramientas = await _context.Herramienta
                .Include(herramienta => herramienta.Fabricante)
                .Where(h => (h.Fabricante.Nombre.Contains(fabricante) || fabricante == null)
                         && (h.Precio == precio || precio == null))
                .Select(h => new HerramientasParaOfertaDTO(h.Id, h.Nombre, h.Material, h.Precio, h.Fabricante.Nombre))
                .ToListAsync();
            return Ok(herramientas);
        }
    }
}
