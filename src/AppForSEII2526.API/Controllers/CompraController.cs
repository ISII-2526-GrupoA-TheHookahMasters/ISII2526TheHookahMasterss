using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<CompraController> _logger;

        public CompraController(ApplicationDbContext context, ILogger<CompraController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<CompraDetailDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetComprasPorId(int id)
        {
            var compra = await _context.Compra
                .Where(c => c.Id == id)
                    .Include(c => c.CompraItems)
                        .ThenInclude(ci => ci.Herramienta)
                            .ThenInclude(h => h.Fabricante)

                .Select(c => new CompraDetailDTO(
                    c.Id,
                    c.Usuario.Nombre,
                    c.Usuario.Apellido,
                    c.DireccionEnvio,
                    c.FechaCompra,
                    c.PrecioTotal,
                    c.CompraItems   
                    
                        .Select(ci => new CompraItemDTO(
                            ci.HerramientaId,
                            ci.Herramienta.Nombre,
                            ci.Herramienta.Material,
                            ci.Precio,
                            ci.Descripcion,
                            ci.Cantidad)).ToList<CompraItemDTO>()))
                
                .FirstOrDefaultAsync();

            if (compra == null)
            {
                _logger.LogError($"Error: Compra con id {id} no existe");
                return NotFound();
            }

            return Ok(compra);
        }
    }
}
