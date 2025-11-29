using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricanteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private ILogger _logger;

        public FabricanteController(ApplicationDbContext context, ILogger<HerramientaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetFabricantes(string? fabricanteNombre)
        {
            IList<string> fabricantes = await _context.Fabricante
                .Where(fabricante => (fabricanteNombre == null || fabricante.Nombre.Contains(fabricanteNombre)))             
                .OrderBy(fabricante => fabricante.Nombre)
                .Select(fabricante => fabricante.Nombre)
                .ToListAsync();

            return Ok(fabricantes);
        }
    }
}
