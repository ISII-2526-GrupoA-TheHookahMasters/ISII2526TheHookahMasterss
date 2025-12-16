using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.ReparacionDTOs;


namespace AppForSEII2526.UT.ReparacionController_test
{
    public class GetDetalleReparacion_test: AppForSEII25264SqliteUT
    {
        public GetDetalleReparacion_test()
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
            _context.Add(reparacion);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit testing")]
        public async Task GetReparacionesPorId_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ReparacionController>>();
            ILogger<ReparacionController> logger = mock.Object;

            var controller = new ReparacionController(_context, logger);

            // Act
            var result = await controller.GetReparacionesPorId(0);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit testing")]
        public async Task GetReparacionesPorId_Found_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ReparacionController>>();
            ILogger<ReparacionController> logger = mock.Object;
            var controller = new ReparacionController(_context, logger);

            var expectedReparacion = new ReparacionDetailDTO(1, "Antonio", "Ortiz", DateTime.Today, DateTime.Today.AddDays(2), new List<ReparacionItemDTO>());
            expectedReparacion.ReparacionItems.Add(new ReparacionItemDTO(2, "Llave inglesa", 15, 3, "Llave inglesa de cabezal grande", 1));

            // Act 
            var result = await controller.GetReparacionesPorId(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var reparacionDTOActual = Assert.IsType<ReparacionDetailDTO>(okResult.Value);
            var eq = expectedReparacion.Equals(reparacionDTOActual);

            Assert.Equal(expectedReparacion, reparacionDTOActual);
        }
    }
}
