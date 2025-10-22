using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlquilarController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<AlquilarController> _logger;

        public AlquilarController(ApplicationDbContext context, ILogger<AlquilarController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<AlquilerDetailDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAlquilerPorId(int id)
        {
            var alquiler = await _context.Alquiler
                .Where(a => a.Id == id)
                    .Include(a => a.AlquilarItems)
                        .ThenInclude(ai => ai.Herramienta)

                .Select(a => new AlquilerDetailDTO(a.Id,a.Usuario.Nombre,a.Usuario.Apellido,a.DireccionEnvio,
                        a.FechaAlquiler,a.PrecioTotal,a.FechaFin,a.FechaInicio, a.AlquilarItems
                            .Select(ai => new AlquilarItemDTO(ai.HerramientaId, ai.Herramienta.Nombre,
                                ai.Herramienta.Material, ai.Cantidad,
                                ai.Herramienta.Precio)).ToList<AlquilarItemDTO>()))
                .FirstOrDefaultAsync();

            if (alquiler == null)
            {
                _logger.LogError($"Error: Alquiler con id {id} no existe");
                return NotFound();
            }

            return Ok(alquiler);
        }
    }
}
