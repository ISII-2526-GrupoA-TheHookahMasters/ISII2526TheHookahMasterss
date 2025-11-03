using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;

namespace AppForSEII2526.UT.HerramientasController_Test
{
    public class GetSeleccionOferta_test : AppForSEII25264SqliteUT
    {
        public GetSeleccionOferta_test()
        {
            var fabricante = new List<Fabricante>
            {
                new Fabricante(1, "Bosch"),
                new Fabricante(2, "Makita"),
                new Fabricante(3, "Stanley"),
            };

            var herramientas = new List<Herramienta>()
            {
                new Herramienta(1, "Martillo", "Acero", 23.4f, 1, fabricante[0]),
                new Herramienta(2, "Llave inglesa", "Hierro", 15.9f, 1, fabricante[1]),
                new Herramienta(3, "Destornillador", "Acero y Plastico", 20.0f, 1, fabricante[2])
            };

            _context.AddRange(fabricante);
            _context.AddRange(herramientas);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_GetHerramientasPorFabricantePrecio()
        {
            var herramientaDTOs = new List<HerramientasParaOfertaDTO>()
            {
                new HerramientasParaOfertaDTO(1, "Martillo", "Acero", 23.4f, "Bosch"),
                new HerramientasParaOfertaDTO(2, "Llave inglesa", "Hierro", 15.9f, "Makita"),
                new HerramientasParaOfertaDTO(3, "Destornillador", "Acero y Plastico", 20.0f, "Stanley")
            };

            var herramientaDTOsTC1 = new List<HerramientasParaOfertaDTO>()
            {
                herramientaDTOs[0],
                herramientaDTOs[1],
                herramientaDTOs[2]
            };

            var herramientaDTOsTC2 = new List<HerramientasParaOfertaDTO>()
            {
                herramientaDTOs[1]
            };

            var herramientaDTOsTC3 = new List<HerramientasParaOfertaDTO>()
            {
                herramientaDTOs[2]
            };

            var allTest = new List<object[]>()
            {
                new object[] { null, null, herramientaDTOsTC1 },
                new object[] { "Makita", null, herramientaDTOsTC2 },
                new object[] { null, 20.0f, herramientaDTOsTC3 }
            };

            return allTest;
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetHerramientasPorFabricantePrecio))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit testing")]
        public async Task GetHerramientasPorFabricantePrecio_test(string? fabricante, float? precio, List<HerramientasParaOfertaDTO> expectedHerramientas)
        {
            // Arrange
            var controller = new HerramientaController(_context, null);

            // Act
            var result = await controller.GetHerramientasPorFabricantePrecio(fabricante, precio);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var herramientaDTOsActual = Assert.IsType<List<HerramientasParaOfertaDTO>>(okResult.Value);
            Assert.Equal(expectedHerramientas, herramientaDTOsActual);
        }
    }

}
