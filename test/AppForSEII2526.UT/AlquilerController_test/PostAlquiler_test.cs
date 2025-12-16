using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.AlquilerDTOs;
using AppForSEII2526.API.Models;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.AlquilerController_test
{
    public class PostAlquiler_test : AppForSEII25264SqliteUT
    {
        public PostAlquiler_test()
        {
            var fabricante = new Fabricante(1, "Bosch");

            var herramientas = new List<Herramienta>()
            {
                new Herramienta(1, "Martillo", "Acero", 120, 1, fabricante),
                new Herramienta(2, "Llave inglesa", "Hierro", 15, 1, fabricante),
            };

            ApplicationUser user = new ApplicationUser("Carlos", "Gomez", 123456789, "carlosgomez@gmail.com");
            var alquiler = new Alquiler(user, "Avd España 4", DateTime.Today, DateTime.Today.AddDays(5), DateTime.Today.AddDays(2), 120, new List<AlquilarItem>());
            alquiler.AlquilarItems.Add(new AlquilarItem(1, 120, alquiler, herramientas[0]));

            _context.Add(fabricante);
            _context.AddRange(herramientas);
            _context.Add(alquiler);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_CreatePurchase()
        {
            var alquilerNoItem = new AlquilerForCreateDTO("Carlos", "Gomez", "Avd España 4",
                DateTime.Today.AddDays(5), DateTime.Today.AddDays(2), new List<AlquilarItemDTO>(), TiposMetodoPago.PayPal, 123456789, "carlos@gmail.com");

            var alquilerItems = new List<AlquilarItemDTO>() { new AlquilarItemDTO(1, "Martillo", "Acero", 4, 120) };

            var alquilerFromBeforeToday = new AlquilerForCreateDTO("Carlos", "Gomez", "Avd España 4",
                DateTime.Today.AddDays(5), DateTime.Today, alquilerItems, TiposMetodoPago.PayPal, 123456789, "carlos@gmail.com");

            var alquilerToBeforeFrom = new AlquilerForCreateDTO("Carlos", "Gomez", "Avd España 4",
                DateTime.Today.AddDays(2), DateTime.Today.AddDays(5), alquilerItems, TiposMetodoPago.PayPal, 123456789, "carlos@gmail.com");

            var alquilarHerramientaNotAvailable = new AlquilerForCreateDTO("Carlos", "Gomez", "Avd España 4",
                DateTime.Today.AddDays(5), DateTime.Today.AddDays(2),new List<AlquilarItemDTO>() 
                    { new AlquilarItemDTO(1, "Destornillador", "Cristal", 4, 120) }, TiposMetodoPago.PayPal, 123456789, "carlos@gmail.com");

            var alquilerSinNombreCliente= new AlquilerForCreateDTO(null, "Gomez", "Avd España 4",
                DateTime.Today.AddDays(5), DateTime.Today.AddDays(2), alquilerItems, TiposMetodoPago.PayPal, 123456789, "carlos@gmail.com");

            var alquilerSinApellidoCliente = new AlquilerForCreateDTO("Carlos", null, "Avd España 4",
                DateTime.Today.AddDays(5), DateTime.Today.AddDays(2), alquilerItems, TiposMetodoPago.PayPal, 123456789, "carlos@gmail.com");

            var alquilerSinDireccionEnvio = new AlquilerForCreateDTO("Carlos", "Gomez", null,
                DateTime.Today.AddDays(5), DateTime.Today.AddDays(2), alquilerItems, TiposMetodoPago.PayPal, 123456789, "carlos@gmail.com");

            var alquilerSinCantidad = new AlquilerForCreateDTO("Carlos", "Gomez", "Avd España 4",
                DateTime.Today.AddDays(5), DateTime.Today.AddDays(2), new List<AlquilarItemDTO>() 
                    { new AlquilarItemDTO(1, "Martillo", "Acero", 0, 120) }, TiposMetodoPago.PayPal, 123456789, "carlos@gmail.com");


            var allTests = new List<object[]>
            {             //input for createpurchase - Error expected
                new object[] { alquilerNoItem, "Error! Debes incluir al menos una herramienta para ser alquilada",  },
                new object[] { alquilerFromBeforeToday, "Error! La fecha de inicio de tu alquiler debe ser posterior a hoy", },
                new object[] { alquilerToBeforeFrom, "Error! Tu alquiler debe terminar despues de que empiece", },
                new object[] { alquilarHerramientaNotAvailable, $"La herramienta con nombre {alquilarHerramientaNotAvailable.AlquilerItems[0].NombreHerramienta} no fue encontrada.", },
                new object[] { alquilerSinNombreCliente, "Error! El nombre es un campo obligatorio", },
                new object[] { alquilerSinApellidoCliente, "Error! El apellido es un campo obligatorio", },
                new object[] { alquilerSinCantidad, "Error! La cantidad no puede ser 0", },
                new object[] { alquilerSinDireccionEnvio, "Error! La direccion de envio es un campo obligatorio", },
            };

            return allTests;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CreatePurchase))]
        public async Task CreateAlquiler_Error_test(AlquilerForCreateDTO alquilerDTO, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<AlquilarController>>();
            ILogger<AlquilarController> logger = mock.Object;

            var controller = new AlquilarController(_context, logger);

            // Act
            var result = await controller.CreateAlquiler(alquilerDTO);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            Assert.StartsWith(errorExpected, errorActual);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreateAlquiler_Success_test()
        {
            // Arrange
            var mock = new Mock<ILogger<AlquilarController>>();
            ILogger<AlquilarController> logger = mock.Object;

            var controller = new AlquilarController(_context, logger);

            DateTime to = DateTime.Today.AddDays(6);
            DateTime from = DateTime.Today.AddDays(7);

            var alquilerDTO = new AlquilerForCreateDTO("Carlos", "Gomez", "Avd España 4",
                DateTime.Today.AddDays(5), DateTime.Today.AddDays(2),
                new List<AlquilarItemDTO>() { new AlquilarItemDTO(1, "Martillo", "Acero", 1, 120) }, TiposMetodoPago.PayPal, 123456789, "carlos@gmail.com");

            var expectedAlquilerDetailDTO = new AlquilerDetailDTO(2, "Carlos", "Gomez", "Avd España 4", DateTime.Today,
                DateTime.Today.AddDays(5), DateTime.Today.AddDays(2),
                new List<AlquilarItemDTO>() { new AlquilarItemDTO(1, "Martillo", "Acero", 1, 120) });

            // Act
            var result = await controller.CreateAlquiler(alquilerDTO);

            //Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualAlquilerDetailDTO = Assert.IsType<AlquilerDetailDTO>(createdResult.Value);

            Assert.Equal(expectedAlquilerDetailDTO, actualAlquilerDetailDTO);
        }
    }
}
