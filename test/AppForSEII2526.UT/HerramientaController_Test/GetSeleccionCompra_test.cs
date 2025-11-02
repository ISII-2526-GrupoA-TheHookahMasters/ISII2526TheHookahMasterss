using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.HerramientaController_Test
{
    public class GetSeleccionCompra_test: AppForSEII25264SqliteUT
    {
        public GetSeleccionCompra_test()
        {
            var fabricante = new List<Fabricante>()
            {
                new Fabricante(1, "Bosch"),
                new Fabricante(2, "Makita"),
                new Fabricante(3, "Stanley")
            };

            var herramientas = new List<Herramienta>()
            {
                new Herramienta(1, "Taladro", "Metal y Plástico", 75f, 7, fabricante[0]),
                new Herramienta(2, "Sierra Circular", "Metal y Madera", 120f, 10, fabricante[0]),
                new Herramienta(3, "Lijadora", "Plástico y Madera", 60f, 5, fabricante[0])
            };

            _context.AddRange(fabricante);
            _context.AddRange(herramientas);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_GetHerramientasPorMaterialPrecioComp()
        {
            var herramientaDTOs = new List<HerramientasParaComprarDTO>()
            {
                new HerramientasParaComprarDTO(1, "Taladro", "Metal y Plástico", 75f, "Bosch"),
                new HerramientasParaComprarDTO(2, "Sierra Circular", "Metal y Madera", 120f, "Bosch"),
                new HerramientasParaComprarDTO(3, "Lijadora", "Plástico y Madera", 60f, "Bosch")
            };

            var herramientaDTOsTC1 = new List<HerramientasParaComprarDTO>()
            {
                herramientaDTOs[0],
                herramientaDTOs[1],
                herramientaDTOs[2]
            };

            var herramientaDTOsTC2 = new List<HerramientasParaComprarDTO>()
            {
                herramientaDTOs[0]
            };

            var herramientaDTOsTC3 = new List<HerramientasParaComprarDTO>()
            {
                herramientaDTOs[1]
            };

            var allTest = new List<object[]>()
            {
                new object[] { null, null, herramientaDTOsTC1 },
                new object[] { "Metal y Plástico", null, herramientaDTOsTC2 },
                new object[] { null, 120f, herramientaDTOsTC3 },
            };

            return allTest;
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetHerramientasPorMaterialPrecioComp))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit testing")]
        public async Task GetHerramientasPorMaterialTiempoComp_test(string? material, float? precio, List<HerramientasParaComprarDTO> expectedHerramientas)
        {
            //Arrange
            var controller = new HerramientaController(_context, null);

            //Act
            var result = await controller.GetHerramientasMaterialPrecio(material, precio);
            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var herramientaDTOsActual = Assert.IsType<List<HerramientasParaComprarDTO>>(okResult.Value);
            Assert.Equal(expectedHerramientas, herramientaDTOsActual);
        }

    }
}
