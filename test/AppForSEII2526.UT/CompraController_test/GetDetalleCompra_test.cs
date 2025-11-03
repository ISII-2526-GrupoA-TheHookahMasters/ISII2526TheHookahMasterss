using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.CompraDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.CompraController_test
{
    public class GetDetalleCompra_test: AppForSEII25264SqliteUT
    {
        public GetDetalleCompra_test()
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
            _context.Add(compra);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit testing")]
        public async Task GetComprasPorId_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<CompraController>>();
            ILogger<CompraController> logger = mock.Object;

            var controller = new CompraController(_context, logger);

            // Act
            var result = await controller.GetComprasPorId(0);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit testing")]
        public async Task GetComprasPorId_Found_test()
        {
            // Arrange
            var mock = new Mock<ILogger<CompraController>>();
            ILogger<CompraController> logger = mock.Object;
            var controller = new CompraController(_context, logger);

            var expectedCompra = new CompraDetailDTO(1, "Juan", "Perez", "Avenida España 12", DateTime.Today, new List<CompraItemDTO>());
            expectedCompra.CompraItems.Add(new CompraItemDTO(1, "Martillo", "Acero", 20, "Martillo de carpintero", 3));

            // Act 
            var result = await controller.GetComprasPorId(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var compraDTOActual = Assert.IsType<CompraDetailDTO>(okResult.Value);
            var eq = expectedCompra.Equals(compraDTOActual);

            Assert.Equal(expectedCompra, compraDTOActual);
        }
    }
}
