using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.CompraDTOs;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        [ProducesResponseType(typeof(CompraDetailDTO), (int)HttpStatusCode.OK)]
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

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(CompraDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CrearCompra(CompraForCreateDTO compraForCreate)
        {

            if (compraForCreate.CompraItems == null || compraForCreate.CompraItems.Count == 0)
            {
                ModelState.AddModelError("CompraItemsDTO", "Error! La compra debe contener al menos un ítem.");
            }

            if(compraForCreate.NombreCliente == null)
                ModelState.AddModelError("NombreCliente", "Error! El nombre del cliente es obligatorio");

            if (compraForCreate.ApellidoCliente == null)
                ModelState.AddModelError("ApellidosCliente", "Error! Los apellidos del cliente son obligatorios");

            if(compraForCreate.DireccionEnvio == null)
                ModelState.AddModelError("DireccionEnvio", "Error! La dirección de envío es obligatoria");



            var usuario = _context.Users.FirstOrDefault(u => u.Nombre == compraForCreate.NombreCliente && u.Apellido == compraForCreate.ApellidoCliente);
            
            if (usuario == null)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            
            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var herramientasNombre = compraForCreate.CompraItems.Select(ri => ri.NombreHerramienta).ToList();
            var herramientas = await _context.Herramienta
                .Include(h => h.Fabricante)
                .Where(h => herramientasNombre.Contains(h.Nombre))
                .ToListAsync();

            var nuevaCompra = new Compra(usuario, compraForCreate.DireccionEnvio, DateTime.Today, compraForCreate.TipoMetodoPago, new List<CompraItem>());

            foreach (var compraItem in compraForCreate.CompraItems)
            {
                var herramienta = herramientas.FirstOrDefault(h => h.Nombre == compraItem.NombreHerramienta);

                if (herramienta == null)
                {
                    ModelState.AddModelError("CompraItems", $"Error! La herramienta con nombre {compraItem.NombreHerramienta} no fue encontrada.");
                    continue;
                }
                if (compraItem.Descripcion.IsNullOrEmpty() && compraItem.Cantidad == 3)
                {
                    ModelState.AddModelError("HerramientaSinDescripcion", "Error!, Estas comprando demasiadas herramientas sin descripcion");
                }
                
                else if (compraItem.Descripcion == null)
                {
                    ModelState.AddModelError("Descripcion", $"Error! La descripción es un campo obligatorio");
                }
                
                if (compraItem.Cantidad == 0)
                {
                    ModelState.AddModelError("Cantidad", $"Error! La cantidad no puede ser 0");
                }

                else
                {
                    nuevaCompra.PrecioTotal += herramienta.Precio * compraItem.Cantidad;
                    nuevaCompra.CompraItems.Add(new CompraItem(compraItem.Cantidad, compraItem.Precio, compraItem.Descripcion, herramienta, nuevaCompra));
                }
            }
            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            _context.Compra.Add(nuevaCompra);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Compra", "Error! Ha habido un problema al crear la nueva Compra");
                return Conflict("Error" + ex.Message);
            }

            var compraCreada = new CompraDetailDTO(nuevaCompra.Id, nuevaCompra.Usuario.Nombre, nuevaCompra.Usuario.Apellido,
                                                        nuevaCompra.DireccionEnvio, nuevaCompra.FechaCompra,
                                                        nuevaCompra.CompraItems.Select(ci => new CompraItemDTO(
                                                            ci.HerramientaId,
                                                            ci.Herramienta.Nombre,
                                                            ci.Herramienta.Material,
                                                            ci.Herramienta.Precio,
                                                            ci.Descripcion,
                                                            ci.Cantidad
                                                        ))
                                                        .ToList<CompraItemDTO>());

            return CreatedAtAction("GetComprasPorId", new { id = nuevaCompra.Id }, compraCreada);


        }

    }
    
}
