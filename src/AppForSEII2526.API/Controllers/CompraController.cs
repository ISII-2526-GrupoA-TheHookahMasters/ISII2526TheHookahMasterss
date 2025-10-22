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
                .Where(o => o.Id == id)
                    .Include(o => o.CompraItems)
                        .ThenInclude(oi => oi.Herramienta)
                            .ThenInclude(h => h.Fabricante)

                .Select(o => new CompraDetailDTO(
                    o.Id,
                    o.Usuario.Nombre,
                    o.Usuario.Apellido,
                    o.DireccionEnvio,
                    o.FechaCompra,
                    o.PrecioTotal,
                    o.CompraItems   
                    
                        .Select(oi => new CompraItemDTO(
                            oi.CompraId,
                            oi.Herramienta.Nombre,
                            oi.Herramienta.Material,
                            oi.Precio,
                            oi.Descripcion,
                            oi.Cantidad)).ToList<CompraItemDTO>()))
                
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
