using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.OfertaDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net.WebSockets;
using System.Runtime.InteropServices;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfertaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<OfertaController> _logger;

        public OfertaController(ApplicationDbContext context, ILogger<OfertaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<OfertaDetailDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetOfertasPorId(int id)
        {
            var oferta = await _context.Oferta
                .Where(o => o.Id == id)
                    .Include(o => o.OfertaItems)
                        .ThenInclude(oi => oi.Herramienta)
                            .ThenInclude(h => h.Fabricante)

                .Select(o => new OfertaDetailDTO(o.Id, o.FechaFinal, o.FechaInicio, o.FechaOferta,
                o.TipoMetodoPago, o.TipoDirigidaOferta, o.OfertaItems
                            .Select(oi => new OfertaItemDTO(oi.HerramientaId, oi.PrecioFinal, oi.Herramienta.Nombre,
                                oi.Herramienta.Precio, oi.Herramienta.Material,
                                oi.Herramienta.Fabricante.Nombre)).ToList<OfertaItemDTO>()))
                .FirstOrDefaultAsync();

            if (oferta == null)
            {
                _logger.LogError($"Error: Oferta con id {id} no existe");
                return NotFound();
            }

            return Ok(oferta);
        }


        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(OfertaDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateOferta(OfertaForCreateDTO ofertaForCreate)
        {
            if (ofertaForCreate.FechaInicio <= DateTime.Today)
                ModelState.AddModelError("FechaInicio", "Error! La fecha de inicio de tu oferta debe ser posterior a hoy");

            if (ofertaForCreate.FechaInicio >= ofertaForCreate.FechaFinal)
                ModelState.AddModelError("FechaInicio&FechaFinal", "Error! Tu oferta debe terminar después de que empiece");

            if (ofertaForCreate.OfertaItems.Count() == 0 || ofertaForCreate.OfertaItems == null)
                ModelState.AddModelError("OfertaItems", "Error! Tienes que incluir al menos una herramienta para aplicar una oferta");
            
            if (ofertaForCreate.FechaInicio == DateTime.MinValue)
                ModelState.AddModelError("FechaInicio", "Error! Fecha Inicio es un campo obligatorio");

            if (ofertaForCreate.FechaFinal == DateTime.MinValue)
                ModelState.AddModelError("FechaFinal", "Error! Fecha Final es un campo obligatorio");

            if (ofertaForCreate.TipoMetodoPago == null)
                ModelState.AddModelError("TipoMetodoPago", "Error! El tipo de método de pago es un campo obligatorio");

            //Si se ha producido alguno de los errores anteriores, terminamos la ejecucion del metodo 
            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));


            var herramientaNombre = ofertaForCreate.OfertaItems.Select(h => h.NombreHerramienta).Distinct().ToList();

            var herramientas = await _context.Herramienta
                .Include(f => f.Fabricante)
                .Where(h => herramientaNombre.Contains(h.Nombre))
                .ToListAsync();

            var nuevaOferta = new Oferta(ofertaForCreate.FechaFinal, ofertaForCreate.FechaInicio, DateTime.Today, 
                                        ofertaForCreate.TipoMetodoPago, ofertaForCreate.TipoDirigidaOferta, new List<OfertaItem>());


            foreach (var ofertaItem in ofertaForCreate.OfertaItems)
            {
                var herramienta = herramientas.FirstOrDefault(h => h.Nombre == ofertaItem.NombreHerramienta);

                if (herramienta == null)
                {
                    ModelState.AddModelError("OfertaItems", $"La herramienta con ID {ofertaItem.HerramientaId} no fue encontrada.");
                    continue;
                }

                if (ofertaForCreate.Porcentaje < 0 || ofertaForCreate.Porcentaje > 100)
                    ModelState.AddModelError("Porcentaje", "Error: El porcentaje debe estar entre 0 y 100");
                else
                {
                    float precioFinal = herramienta.Precio * (1 - (ofertaForCreate.Porcentaje / 100.0f));
                    nuevaOferta.OfertaItems.Add(new OfertaItem(ofertaForCreate.Porcentaje, precioFinal, nuevaOferta, herramienta));
                }
            }

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            _context.Oferta.Add(nuevaOferta);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Oferta", $"Error! Ha habido un problema al guardar la nueva Oferta");
                return Conflict("Error" + ex.Message);
            }

            var ofertaCreada = new OfertaDetailDTO(nuevaOferta.Id, nuevaOferta.FechaFinal, nuevaOferta.FechaInicio, nuevaOferta.FechaOferta,
                                                    nuevaOferta.TipoMetodoPago, nuevaOferta.TipoDirigidaOferta, 
                                                    nuevaOferta.OfertaItems.Select(oi => new OfertaItemDTO(
                                                        oi.HerramientaId,
                                                        oi.PrecioFinal,
                                                        oi.Herramienta.Nombre,
                                                        oi.Herramienta.Precio,
                                                        oi.Herramienta.Material,
                                                        oi.Herramienta.Fabricante.Nombre
                                                    ))
                                                    .ToList());

            return CreatedAtAction("GetOfertasPorId", new { id = nuevaOferta.Id }, ofertaCreada);
        }
    }
}