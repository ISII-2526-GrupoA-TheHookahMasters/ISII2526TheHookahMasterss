using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.AlquilerDTOs;
using AppForSEII2526.API.DTOs.OfertaDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.AlquilerController_test
{
    public class GetDetalleAlquiler_test : AppForSEII25264SqliteUT
    {

        public GetDetalleAlquiler_test()
        {
            var fabricante = new Fabricante(1, "Bosch");

            var herramientas = new List<Herramienta>()
            {
                new Herramienta(1, "Martillo", "Acero", 40, 1, fabricante),
                new Herramienta(2, "Llave inglesa", "Hierro", 15, 1, fabricante),
            };

            ApplicationUser user= new ApplicationUser("Carlos", "Gomez", 123456789, "carlosgomez@gmail.com");
            var oferta = new Alquiler(user, "Avd España 4", DateTime.Today,DateTime.Today.AddDays(5),DateTime.Today.AddDays(2), 120, new List<AlquilarItem>());
            oferta.AlquilarItems.Add(new AlquilarItem(3, 40, oferta, herramientas[0]));

            _context.Add(fabricante);
            _context.AddRange(herramientas);
            _context.Add(oferta);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit testing")]
        public async Task GetAlquilerPorId_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<AlquilarController>>();
            ILogger<AlquilarController> logger = mock.Object;

            var controller = new AlquilarController(_context, logger);

            // Act
            var result = await controller.GetAlquilerPorId(0);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit testing")]
        public async Task GetAlquilerPorId_Found_test()
        {
            // Arrange
            var mock = new Mock<ILogger<AlquilarController>>();
            ILogger<AlquilarController> logger = mock.Object;
            var controller = new AlquilarController(_context, logger);

            var expectedAlquiler = new AlquilerDetailDTO(1, "Carlos", "Gomez",
                        "Avd España 4", DateTime.Today, DateTime.Today.AddDays(5), DateTime.Today.AddDays(2),
                        new List<AlquilarItemDTO>());
            expectedAlquiler.AlquilerItems.Add(new AlquilarItemDTO(1,"Martillo","Acero", 3, 40));

            // Act 
            var result = await controller.GetAlquilerPorId(1);

            //Assert
            //we check that the response type is OK and obtain the rental
            var okResult = Assert.IsType<OkObjectResult>(result);
            var alquilerDTOActual = Assert.IsType<AlquilerDetailDTO>(okResult.Value);
            var eq = expectedAlquiler.Equals(alquilerDTOActual);
            //we check that the expected and actual are the same
            Assert.Equal(expectedAlquiler, alquilerDTOActual);
        }
    }
}
