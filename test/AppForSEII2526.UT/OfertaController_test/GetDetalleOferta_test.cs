using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.OfertaDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.OfertaController_test
{
    public class GetDetalleOferta_test : AppForSEII25264SqliteUT
    {
        public GetDetalleOferta_test()
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

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit testing")]
        public async Task GetOfertasPorId_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<OfertaController>>();
            ILogger<OfertaController> logger = mock.Object;

            var controller = new OfertaController(_context, logger);

            // Act
            var result = await controller.GetOfertasPorId(0);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit testing")]
        public async Task GetOfertasPorId_Found_test()
        {
            // Arrange
            var mock = new Mock<ILogger<OfertaController>>();
            ILogger<OfertaController> logger = mock.Object;
            var controller = new OfertaController(_context, logger);

            var expectedOferta = new OfertaDetailDTO(1, DateTime.Today.AddDays(5), DateTime.Today.AddDays(2), 
                        DateTime.Today, TiposMetodoPago.PayPal, TiposDirigidaOferta.Clientes,
                        new List<OfertaItemDTO>());
            expectedOferta.OfertaItems.Add(new OfertaItemDTO(1, "Martillo", "Acero", "Bosch", 20, 50));

            // Act 
            var result = await controller.GetOfertasPorId(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var ofertaDTOActual = Assert.IsType<OfertaDetailDTO>(okResult.Value);
            var eq = expectedOferta.Equals(ofertaDTOActual);

            Assert.Equal(expectedOferta, ofertaDTOActual);
        }
    }
}
