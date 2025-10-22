using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReparacionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<ReparacionController> _logger;

        public ReparacionController(ApplicationDbContext context, ILogger<ReparacionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<ReparacionDetailDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetReparacionesPorId(int id)
        {
            var reparacion = await _context.Reparacion
                .Where(r => r.Id == id)
                    .Include(r => r.ReparacionItems)
                        .ThenInclude(ri => ri.Herramienta)
                            .ThenInclude(h => h.Fabricante)

                .Select(r => new ReparacionDetailDTO(r.Id, r.Usuario.Nombre, r.Usuario.Apellido, r.FechaEntrega, r.FechaRecogida, r.PrecioTotal, r.ReparacionItems
                            .Select(ri => new ReparacionItemDTO(ri.ReparacionId, ri.Herramienta.Nombre, ri.Precio, ri.Cantidad, ri.Descripcion)).ToList<ReparacionItemDTO>()))
                .FirstOrDefaultAsync();

            if (reparacion == null)
            {
                _logger.LogError($"Error: Reparacion con id {id} no existe");
                return NotFound();
            }

            return Ok(reparacion);
        }
    }
}
