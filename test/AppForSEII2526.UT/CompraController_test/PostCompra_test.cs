using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.CompraDTOs;
using AppForSEII2526.API.Models;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.CompraController_test
{
    public class PostCompra_test : AppForSEII25264SqliteUT
    {
        public PostCompra_test()
        {
            var fabricante = new Fabricante(1, "Bosch");

            var herramientas = new List<Herramienta>()
            {
                new Herramienta(1, "Martillo", "Acero", 20, 1, fabricante),
                new Herramienta(2, "Llave inglesa", "Hierro", 15, 1, fabricante),
            };

            var usuario = new ApplicationUser("Juan", "Perez", 12345678, "jperez@gmail.com");

            var compra = new Compra(usuario, "Avenida España 12", DateTime.Today, TiposMetodoPago.PayPal, new List<CompraItem>());

            compra.CompraItems.Add(new CompraItem(3, 20, "Martillo de carpintero", herramientas[0], compra));

            _context.Add(fabricante);
            _context.AddRange(herramientas);
            _context.Add(usuario);
            _context.Add(compra);
            _context.SaveChanges();
        }
    

        public static IEnumerable<object[]> TestCasesFor_CreateCompra()
        {
            var compraNoItem = new CompraForCreateDTO("Juan", "Perez",
                "Avenida España 12", TiposMetodoPago.PayPal, new List<CompraItemDTO>(), "juan.nuevo@gmail.com", 612345678);

            var compraItems = new List<CompraItemDTO>() { new CompraItemDTO(3, "Martillo", "Acero", 20, "Martillo de carpintero", 3) };

            var compraNoNombre = new CompraForCreateDTO(null, "Perez",
                "Avenida España 12", TiposMetodoPago.PayPal, compraItems, "juan.nuevo@gmail.com", 612345678);

            var compraNoApellido = new CompraForCreateDTO("Juan", null,
                "Avenida España 12", TiposMetodoPago.PayPal, compraItems, "juan.nuevo@gmail.com", 612345678);

            var compraNoDireccion = new CompraForCreateDTO("Juan", "Perez", 
                null, TiposMetodoPago.PayPal, compraItems, "juan.nuevo@gmail.com", 612345678);

            var compraCantidadCero = new CompraForCreateDTO("Juan", "Perez",
                "Avenida España 12", TiposMetodoPago.PayPal,
                new List<CompraItemDTO>() { new CompraItemDTO(3, "Martillo", "Acero", 20, "Martillo de carpintero", 0) }, "juan.nuevo@gmail.com", 612345678);

            var compraHerramientaNoExiste = new CompraForCreateDTO("Juan", "Perez",
                "Avenida España 12", TiposMetodoPago.PayPal,
                new List<CompraItemDTO>() { new CompraItemDTO(9, "Llave de pico", "Acero", 20, "Llave de pico de loro mango rojo", 3) }, "juan.nuevo@gmail.com", 612345678);

            var compraNoDescripcion = new CompraForCreateDTO("Juan", "Perez",
                "Avenida España 12", TiposMetodoPago.PayPal,
                new List<CompraItemDTO>() { new CompraItemDTO(3, "Martillo", "Acero", 20, null, 2) }, "juan.nuevo@gmail.com", 612345678);

            var compraHerramientaSinDescripcion = new CompraForCreateDTO("Juan", "Perez",
                "Avenida España 12", TiposMetodoPago.PayPal,
                new List<CompraItemDTO>() { new CompraItemDTO(3, "Martillo", "Acero", 20, null, 3) }, "juan.nuevo@gmail.com", 612345678);

            var allTests = new List<object[]>
            {             //input for createpurchase - Error expected
                new object[] { compraNoItem, "Error! La compra debe contener al menos un ítem.",  },
                new object[] { compraNoNombre, "Error! El nombre del cliente es obligatorio",  },
                new object[] { compraNoApellido, "Error! Los apellidos del cliente son obligatorios",  },
                new object[] { compraNoDireccion, "Error! La dirección de envío es obligatoria",  },
                new object[] { compraCantidadCero, "Error! La cantidad no puede ser 0",  },
                new object[] { compraHerramientaNoExiste, $"Error! La herramienta con nombre {compraHerramientaNoExiste.CompraItems[0].NombreHerramienta} no fue encontrada.",  },
                new object[] { compraNoDescripcion, "Error! La descripción es un campo obligatorio",  },
                new object[] { compraHerramientaSinDescripcion, "Error!, Estas comprando demasiadas herramientas sin descripcion"}
            };

            return allTests;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CreateCompra))]
        public async Task CreateCompra_Error_test(CompraForCreateDTO compraDTO, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<CompraController>>();
            ILogger<CompraController> logger = mock.Object;

            var controller = new CompraController(_context, logger);

            // Act
            var result = await controller.CrearCompra(compraDTO);

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
        public async Task CreateCompra_Success_test()
        {
            // Arrange
            var mock = new Mock<ILogger<CompraController>>();
            ILogger<CompraController> logger = mock.Object;

            var controller = new CompraController(_context, logger);

            var compraDTO = new CompraForCreateDTO("Juan", "Perez",
                "Avenida España 12", TiposMetodoPago.PayPal,
                new List<CompraItemDTO>() { new CompraItemDTO(1, "Martillo", "Acero", 20, "Martillo de carpintero", 3) },
                "juan.nuevo@gmail.com",
                612345678);

            var expectedCompraDetailDTO = new CompraDetailDTO(2, "Juan", "Perez",
                "Avenida España 12", DateTime.Today,
                new List<CompraItemDTO>() { new CompraItemDTO(1, "Martillo", "Acero", 20, "Martillo de carpintero", 3) });

            // Act
            var result = await controller.CrearCompra(compraDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualCompraDetailDTO = Assert.IsType<CompraDetailDTO>(createdResult.Value);

            Assert.Equal(expectedCompraDetailDTO, actualCompraDetailDTO);

        }

    }
}
