using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.AlquilerDTOs;
using AppForSEII2526.API.Models;
using Humanizer.DateTimeHumanizeStrategy;
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
        [ProducesResponseType(typeof(AlquilerDetailDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAlquilerPorId(int id)
        {
            var alquiler = await _context.Alquiler
                .Where(a => a.Id == id)
                    .Include(a => a.AlquilarItems)
                        .ThenInclude(ai => ai.Herramienta)

                .Select(a => new AlquilerDetailDTO(a.Id,a.Usuario.Nombre,a.Usuario.Apellido,a.DireccionEnvio,
                        a.FechaAlquiler, a.FechaFin, a.FechaInicio, a.AlquilarItems
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


        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(AlquilerDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]

        public async Task<ActionResult> CreateAlquiler(AlquilerForCreateDTO alquilerForCreate)
        {

            if (alquilerForCreate.FechaInicio <= DateTime.Today)
                ModelState.AddModelError("FechaInicio", "Error! La fecha de inicio de tu alquiler debe ser posterior a hoy");

            if (alquilerForCreate.FechaInicio >= alquilerForCreate.FechaFin)
                ModelState.AddModelError("FechaInicio&FechaFin", "Error! Tu alquiler debe terminar despues de que empiece");

            if (alquilerForCreate.AlquilerItems.Count == 0)
                ModelState.AddModelError("AlquilerItems", "Error! Debes incluir al menos una herramienta para ser alquilada");

            if (alquilerForCreate.NombreCliente == null)
                ModelState.AddModelError("NombreCliente", "Error! El nombre es un campo obligatorio");

            if (alquilerForCreate.ApellidosCliente == null)
                ModelState.AddModelError("ApellidosCliente", "Error! El apellido es un campo obligatorio");

            if (alquilerForCreate.DireccionEnvio == null)
                ModelState.AddModelError("DireccionEnvio", "Error! La direccion de envio es un campo obligatorio");

            if (alquilerForCreate.AlquilerItems.Count() == 0 || alquilerForCreate.AlquilerItems == null)
                ModelState.AddModelError("AlquilerItems", "Error! Tienes que incluir al menos una herramienta para aplicar un alquiler");

            var usuario = _context.Users.FirstOrDefault(u => u.Nombre == alquilerForCreate.NombreCliente && u.Apellido == alquilerForCreate.ApellidosCliente);
            if (usuario == null)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));


            var herramientaNombre = alquilerForCreate.AlquilerItems.Select(a => a.NombreHerramienta).ToList();
            var herramientas = await _context.Herramienta
                .Include(f=>f.Fabricante)
                .Where(h => herramientaNombre.Contains(h.Nombre))
                .ToListAsync();

            var nuevoAlquiler = new Alquiler(usuario,alquilerForCreate.DireccionEnvio, DateTime.Today, alquilerForCreate.FechaFin, alquilerForCreate.FechaInicio, alquilerForCreate.PrecioTotal, new List<AlquilarItem>());

            foreach (var alquilerItem in alquilerForCreate.AlquilerItems)
            {
                var herramienta = herramientas.FirstOrDefault(h => h.Nombre == alquilerItem.NombreHerramienta);

                if (herramienta == null)
                {
                    ModelState.AddModelError("AlquilerItems", $"La herramienta con nombre {alquilerItem.NombreHerramienta} no fue encontrada.");
                    continue;
                }

                if (alquilerItem.Cantidad == 0)
                {
                    ModelState.AddModelError("Cantidad", "Error! La cantidad no puede ser 0");
                }
                else
                {
                    nuevoAlquiler.PrecioTotal += herramienta.Precio * nuevoAlquiler.Periodo;
                    nuevoAlquiler.AlquilarItems.Add(new AlquilarItem(alquilerItem.Cantidad, alquilerItem.Precio, nuevoAlquiler, herramienta));
                }
            }


            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));


            _context.Alquiler.Add(nuevoAlquiler);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Alquiler", $"Error! Ha habido un problema al guardar el nuevo Alquiler");
                return Conflict("Error" + ex.Message);

            }

            var alquilerCreado = new AlquilerDetailDTO(nuevoAlquiler.Id, nuevoAlquiler.Usuario.Nombre, nuevoAlquiler.Usuario.Apellido, nuevoAlquiler.DireccionEnvio,
                                                    nuevoAlquiler.FechaAlquiler, nuevoAlquiler.FechaFin, nuevoAlquiler.FechaInicio,
                                                    nuevoAlquiler.AlquilarItems.Select(ai => new AlquilarItemDTO(
                                                        ai.HerramientaId,
                                                        ai.Herramienta.Nombre,
                                                        ai.Herramienta.Material,
                                                        ai.Cantidad,
                                                        ai.Herramienta.Precio
                                                    ))
                                                    .ToList());

            return CreatedAtAction("GetAlquilerPorId", new { id = nuevoAlquiler.Id }, alquilerCreado);

        }


    }
}
