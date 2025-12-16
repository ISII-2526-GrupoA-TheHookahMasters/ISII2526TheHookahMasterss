using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.OfertaDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.OfertaController_test
{
    public class PostOferta_test : AppForSEII25264SqliteUT
    {
        public PostOferta_test()
        {
            var fabricante = new Fabricante(1, "Bosch");

            var herramientas = new List<Herramienta>()
            {
                new Herramienta(1, "Martillo", "Acero", 20, 1, fabricante),
                new Herramienta(2, "Llave inglesa", "Hierro", 15, 1, fabricante),
            };

            var oferta = new Oferta(DateTime.Today.AddDays(5), DateTime.Today.AddDays(2), DateTime.Today, TiposMetodoPago.PayPal, TiposDirigidaOferta.Clientes, new List<OfertaItem>());
            oferta.OfertaItems.Add(new OfertaItem(50, 10, oferta, herramientas[0]));

            _context.Add(fabricante);
            _context.AddRange(herramientas);
            _context.Add(oferta);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_CreateOferta()
        {

            var ofertaToBeforeFrom = new OfertaForCreateDTO(DateTime.Today.AddDays(2), DateTime.Today.AddDays(11),
                TiposMetodoPago.PayPal, TiposDirigidaOferta.Clientes, new List<OfertaItemDTO>()
                { new OfertaItemDTO(1, "Martillo", "Acero", "Bosch", 20, 50) });

            var ofertaSinFechaFinal = new OfertaForCreateDTO(DateTime.MinValue, DateTime.Today.AddDays(10),
                TiposMetodoPago.PayPal, TiposDirigidaOferta.Clientes,
                new List<OfertaItemDTO>()
                { new OfertaItemDTO(1, "Martillo", "Acero", "Bosch", 20, 50) });

            var ofertaNoItem = new OfertaForCreateDTO(DateTime.Today.AddDays(11), DateTime.Today.AddDays(2),
                TiposMetodoPago.PayPal, TiposDirigidaOferta.Clientes, new List<OfertaItemDTO>());

            var ofertaItems = new List<OfertaItemDTO>() { new OfertaItemDTO(1, "Martillo", "Acero", "Bosch", 20, 50) };

            var ofertaFromBeforeToday = new OfertaForCreateDTO(DateTime.Today.AddDays(8), DateTime.Today,
                TiposMetodoPago.PayPal, TiposDirigidaOferta.Clientes, ofertaItems);

            var ofertaHerramientaNoDisponible = new OfertaForCreateDTO(DateTime.Today.AddDays(11), DateTime.Today.AddDays(2),
                TiposMetodoPago.PayPal, TiposDirigidaOferta.Clientes,
                new List<OfertaItemDTO>()
                { new OfertaItemDTO(2, "Destornillador", "Acero y Plástico", "Bosch", 40, 50) });

            var ofertaPorcentajeNoValido = new OfertaForCreateDTO(DateTime.Today.AddDays(11), DateTime.Today.AddDays(2),
                TiposMetodoPago.PayPal, TiposDirigidaOferta.Clientes,
                new List<OfertaItemDTO>()
                { new OfertaItemDTO(1, "Martillo", "Acero", "Bosch", 20, 150) });

            var ofertaSinFechaInicio = new OfertaForCreateDTO(DateTime.Today.AddDays(5), DateTime.MinValue,
                TiposMetodoPago.PayPal, TiposDirigidaOferta.Clientes,
                new List<OfertaItemDTO>()
                { new OfertaItemDTO(1, "Martillo", "Acero", "Bosch", 20, 50) });

            var ofertaMayorQueUnaSemana = new OfertaForCreateDTO(DateTime.Today.AddDays(3), DateTime.Today.AddDays(2),
               TiposMetodoPago.PayPal, TiposDirigidaOferta.Clientes,
               new List<OfertaItemDTO>()
               { new OfertaItemDTO(1, "Martillo", "Acero", "Bosch", 20, 50) });

            var ofertaPorcentajeCero = new OfertaForCreateDTO(DateTime.Today.AddDays(11), DateTime.Today.AddDays(2),
                TiposMetodoPago.PayPal, TiposDirigidaOferta.Clientes,
                new List<OfertaItemDTO>()
                { new OfertaItemDTO(1, "Martillo", "Acero", "Bosch", 20, 0) });

            var allTests = new List<object[]>
            {
                new object[] { ofertaToBeforeFrom, "Error! Tu oferta debe terminar después de que empiece", },
                new object[] { ofertaSinFechaFinal, "Error! Fecha Final es un campo obligatorio", },
                new object[] { ofertaNoItem, "Error! Tienes que incluir al menos una herramienta para aplicar una oferta",  },
                new object[] { ofertaFromBeforeToday, "Error! La fecha de inicio de tu oferta debe ser posterior a hoy", },
                new object[] { ofertaHerramientaNoDisponible, $"La herramienta con nombre {ofertaHerramientaNoDisponible.OfertaItems[0].NombreHerramienta} no fue encontrada", },
                new object[] { ofertaPorcentajeNoValido, "Error: El porcentaje debe estar entre 0 y 100", },
                new object[] { ofertaSinFechaInicio, "Error! Fecha Inicio es un campo obligatorio", },
                new object[] { ofertaMayorQueUnaSemana, "¡Error!, la oferta debe durar al menos una semana", },
                new object[] { ofertaPorcentajeCero, "Error: El porcentaje es un campo obligatorio", },

            };

            return allTests;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CreateOferta))]
        public async Task CreateOferta_Error_test(OfertaForCreateDTO ofertaDTO, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<OfertaController>>();
            ILogger<OfertaController> logger = mock.Object;

            var controller = new OfertaController(_context, logger);

            // Act
            var result = await controller.CreateOferta(ofertaDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            Assert.StartsWith(errorExpected, errorActual);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreateOferta_Success_test()
        {
            // Arrange
            var mock = new Mock<ILogger<OfertaController>>();
            ILogger<OfertaController> logger = mock.Object;

            var controller = new OfertaController(_context, logger);

            var ofertaDTO = new OfertaForCreateDTO(DateTime.Today.AddDays(10), DateTime.Today.AddDays(2),
                TiposMetodoPago.PayPal, TiposDirigidaOferta.Clientes,
                new List<OfertaItemDTO>()
                { new OfertaItemDTO(1, "Martillo", "Acero", "Bosch", 20, 50) });

            var expectedOfertaDetailDTO = new OfertaDetailDTO(2, DateTime.Today.AddDays(10), DateTime.Today.AddDays(2),
                DateTime.Today, TiposMetodoPago.PayPal, TiposDirigidaOferta.Clientes,
                new List<OfertaItemDTO>()
                { new OfertaItemDTO(1, "Martillo", "Acero", "Bosch", 20, 50) });

            // Act
            var result = await controller.CreateOferta(ofertaDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualOfertaDetailDTO = Assert.IsType<OfertaDetailDTO>(createdResult.Value);

            Assert.Equal(expectedOfertaDetailDTO, actualOfertaDetailDTO);

        }
    }
}

