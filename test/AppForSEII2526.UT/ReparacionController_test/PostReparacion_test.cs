using AppForSEII2526.API.DTOs.ReparacionDTOs;
using AppForSEII2526.API.Models;
using AppForSEII2526.API.Controllers;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ReparacionController_test
{
    public class PostReparacion_test : AppForSEII25264SqliteUT
    {
        public PostReparacion_test()
        {
            var fabricante = new Fabricante(1, "Bosch");

            var herramientas = new List<Herramienta>()
        {
                new Herramienta(1, "Martillo", "Acero", 20, 1, fabricante),
                new Herramienta(2, "Llave inglesa", "Hierro", 15, 1, fabricante),
        };

            var usuario = new ApplicationUser("Antonio", "Ortiz", 12345678, "antortiz@gmail.com");

            var reparacion = new Reparacion(usuario, DateTime.Today, DateTime.Today.AddDays(2), TiposMetodoPago.PayPal, new List<ReparacionItem>());

            reparacion.ReparacionItems.Add(new ReparacionItem(3, "Llave inglesa de cabezal grande", 15, reparacion, herramientas[1]));

            _context.Add(fabricante);
            _context.AddRange(herramientas);
            _context.Add(usuario);
            _context.Add(reparacion);
            _context.SaveChanges();
        } 
    
        public static IEnumerable<object[]> TestCasesFor_CreateReparacion()
        {

            var reparacionNoItems = new ReparacionForCreateDTO("Antonio", "Ortiz", DateTime.Today.AddDays(1),
                                         TiposMetodoPago.PayPal, 12345678, new List<ReparacionItemDTO>());

            var reparacionItems = new List<ReparacionItemDTO>(){ new ReparacionItemDTO(2, "Llave inglesa", 15, 1, "Llave inglesa de cabezal grande", 1) };

            var reparacionFechaEntregaNC = new ReparacionForCreateDTO("Antonio", "Ortiz", DateTime.Today, 
                                             TiposMetodoPago.PayPal, 12345678, reparacionItems);

            var reparacionNoNombre = new ReparacionForCreateDTO(null, "Ortiz", DateTime.Today.AddDays(1),
                                         TiposMetodoPago.PayPal, 12345678, reparacionItems);

            var reparacionNoApellido = new ReparacionForCreateDTO("Antonio", null, DateTime.Today.AddDays(1),
                                         TiposMetodoPago.PayPal, 12345678, reparacionItems);

            var reparacionNoFechaEntrega = new ReparacionForCreateDTO("Antonio", "Ortiz", DateTime.MinValue,
                                         TiposMetodoPago.PayPal, 12345678, reparacionItems);

            var reparacionNoFechaRecogida = new ReparacionForCreateDTO("Antonio", "Ortiz", DateTime.Today.AddDays(1),
                                         TiposMetodoPago.PayPal, 12345678, reparacionItems);

            var reparacionCantidadCero = new ReparacionForCreateDTO("Antonio", "Ortiz", DateTime.Today.AddDays(1),
                                         TiposMetodoPago.PayPal, 12345678, new List<ReparacionItemDTO>() {
                                             new ReparacionItemDTO(2, "Llave inglesa", 15, 0, "Llave inglesa de cabezal grande", 1)});

            var reparacionHerramientaNoExiste = new ReparacionForCreateDTO("Antonio", "Ortiz", DateTime.Today.AddDays(1),
                                                     TiposMetodoPago.PayPal, 12345678, new List<ReparacionItemDTO>() {
                                             new ReparacionItemDTO(2, "Serrucho", 15, 1, "Serrucho grande", 1)});


            var allTests = new List<object[]>
            {             //input for createpurchase - Error expected
                new object[] { reparacionNoItems, "Error! Tienes que tener al menos una herramienta para reparar en el carrito" ,  },
                new object[] { reparacionFechaEntregaNC, "Error! La fecha de entrega debe ser posterior a hoy", },
                new object[] { reparacionNoNombre, "Error! El nombre del cliente es obligatorio", },
                new object[] { reparacionNoApellido, "Error! Los apellidos del cliente son obligatorios", },
                new object[] { reparacionNoFechaEntrega, "Error! La fecha de entrega es obligatoria", },
                new object[] { reparacionCantidadCero, "Error! La cantidad no puede ser 0", },
                new object[] { reparacionHerramientaNoExiste, $"Error: La herramienta con nombre {reparacionHerramientaNoExiste.ReparacionItems[0].NombreHerramienta} no fue encontrada.", },
            };

            return allTests;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CreateReparacion))]
        public async Task CreateReparacion_Error_test(ReparacionForCreateDTO reparacionDTO, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<ReparacionController>>();
            ILogger<ReparacionController> logger = mock.Object;

            var controller = new ReparacionController(_context, logger);

            // Act
            var result = await controller.CreateReparacion(reparacionDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            //we check that the expected error message and actual are the same
            Assert.StartsWith(errorExpected, errorActual);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreateReparacion_Success_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ReparacionController>>();
            ILogger<ReparacionController> logger = mock.Object;

            var controller = new ReparacionController(_context, logger);

            var reparacionDTO = new ReparacionForCreateDTO("Antonio", "Ortiz", DateTime.Today.AddDays(1),
                                                 TiposMetodoPago.PayPal, 12345678, new List<ReparacionItemDTO>{
                                                  new ReparacionItemDTO(2, "Llave inglesa", 15, 1, "Llave inglesa de cabezal grande", 1)});

            var expectedreparacionDetailDTO = new ReparacionDetailDTO(2, "Antonio", "Ortiz", DateTime.Today.AddDays(1),
                                                DateTime.Today.AddDays(2), new List<ReparacionItemDTO>{
                                                  new ReparacionItemDTO(2, "Llave inglesa", 15, 1, "Llave inglesa de cabezal grande", 1)}); 

            // Act
            var result = await controller.CreateReparacion(reparacionDTO);

            //Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualReparacionDetailDTO = Assert.IsType<ReparacionDetailDTO>(createdResult.Value);

            Assert.Equal(expectedreparacionDetailDTO, actualReparacionDetailDTO);

        }

    }
}
