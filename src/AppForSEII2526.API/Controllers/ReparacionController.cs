using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.ReparacionDTOs;
using AppForSEII2526.API.Models;
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
        [ProducesResponseType(typeof(ReparacionDetailDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetReparacionesPorId(int id)
        {
            var reparacion = await _context.Reparacion
                .Where(r => r.Id == id)
                    .Include(r => r.ReparacionItems)
                        .ThenInclude(ri => ri.Herramienta)
                            .ThenInclude(h => h.Fabricante)

                .Select(r => new ReparacionDetailDTO(r.Id, r.Usuario.Nombre, r.Usuario.Apellido, r.FechaEntrega, r.FechaRecogida, r.ReparacionItems
                            .Select(ri => new ReparacionItemDTO(ri.HerramientaId, ri.Herramienta.Nombre, ri.Precio, ri.Cantidad, ri.Descripcion)).ToList<ReparacionItemDTO>()))
                .FirstOrDefaultAsync();

            if (reparacion == null)
            {
                _logger.LogError($"Error: Reparacion con id {id} no existe");
                return NotFound();
            }

            return Ok(reparacion);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReparacionDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateReparacion(ReparacionForCreateDTO reparacionForCreate)
        {

            if (reparacionForCreate.FechaEntrega == DateTime.MinValue)
                ModelState.AddModelError("FechaEntrega", "Error! La fecha de entrega es obligatoria");

            if (reparacionForCreate.FechaRecogida == DateTime.MinValue)
                ModelState.AddModelError("FechaRecogida", "Error! La fecha de recogida es obligatoria");

            if (reparacionForCreate.FechaEntrega <= DateTime.Today)
                ModelState.AddModelError("FechaEntrega", "Error! La fecha de entrega debe ser posterior a hoy");

            if (reparacionForCreate.ReparacionItems.Count() == 0 || reparacionForCreate.ReparacionItems == null)
                ModelState.AddModelError("ReparacionItems", "Error! Tienes que tener al menos una herramienta para reparar en el carrito");

            if (reparacionForCreate.NombreCliente == null)
                ModelState.AddModelError("NombreCliente", "Error! El nombre del cliente es obligatorio");

            if (reparacionForCreate.ApellidosCliente == null)
                ModelState.AddModelError("ApellidosCliente", "Error! Los apellidos del cliente son obligatorios");

            var usuario = _context.Users.FirstOrDefault(u => u.Nombre == reparacionForCreate.NombreCliente && u.Apellido == reparacionForCreate.ApellidosCliente);
            if (usuario == null)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var herramientasNombre = reparacionForCreate.ReparacionItems.Select(ri => ri.NombreHerramienta).ToList();
            var herramientas = await _context.Herramienta
                .Include(h => h.Fabricante)
                .Where(h => herramientasNombre.Contains(h.Nombre))
                .ToListAsync();

            var nuevaReparacion = new Reparacion(usuario, reparacionForCreate.FechaRecogida, reparacionForCreate.FechaEntrega, reparacionForCreate.TipoMetodoPago, new List<ReparacionItem>());

            foreach (var reparacionItem in reparacionForCreate.ReparacionItems)
            {
                var herramienta = herramientas.FirstOrDefault(h => h.Nombre == reparacionItem.NombreHerramienta);

                if (herramienta == null)
                {
                    ModelState.AddModelError("ReparacionItems", $"La herramienta con nombre {reparacionItem.NombreHerramienta} no fue encontrada.");
                    continue;
                }

                if (reparacionItem.Cantidad == 0)
                {
                    ModelState.AddModelError("Cantidad", $"Error! La cantidad no puede ser 0");
                }
                else
                {
           
                    nuevaReparacion.ReparacionItems.Add(new ReparacionItem(reparacionItem.Cantidad, reparacionItem.Descripcion, reparacionItem.Precio, nuevaReparacion, herramienta));
                    nuevaReparacion.PrecioTotal += herramienta.Precio * reparacionItem.Cantidad;
                }
            }

            if (ModelState.ErrorCount > 0)
            return BadRequest(new ValidationProblemDetails(ModelState));

            _context.Reparacion.Add(nuevaReparacion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
               _logger.LogError(ex.Message);
                ModelState.AddModelError("Reparacion", "Error! Ha habido un problema al crear la nueva Reparacion");
                return Conflict("Error" + ex.Message);
            }

            var reparacionCreada = new ReparacionDetailDTO(nuevaReparacion.Id, nuevaReparacion.Usuario.Nombre, nuevaReparacion.Usuario.Apellido, 
                                                        nuevaReparacion.FechaEntrega, nuevaReparacion.FechaRecogida,
                                                        nuevaReparacion.ReparacionItems.Select(ri => new ReparacionItemDTO(
                                                            ri.HerramientaId, 
                                                            ri.Herramienta.Nombre, 
                                                            ri.Herramienta.Precio, 
                                                            ri.Cantidad, 
                                                            ri.Descripcion
                                                        ))
                                                        .ToList<ReparacionItemDTO>());

            return CreatedAtAction("GetReparacionesPorId", new { id = nuevaReparacion.Id }, reparacionCreada);

        }
    }
}
