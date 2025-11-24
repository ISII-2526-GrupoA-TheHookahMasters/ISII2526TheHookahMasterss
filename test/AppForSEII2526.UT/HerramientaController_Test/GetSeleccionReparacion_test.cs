using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.HerramientaController_test
{
    public class GetSeleccionReparacion : AppForSEII25264SqliteUT
    {
        public GetSeleccionReparacion()
        {
            var fabricante = new List<Fabricante>()
            {
                new Fabricante(1, "Bosch"),
                new Fabricante(2, "Makita"),
                new Fabricante(3, "Stanley")
            };

            var herramientas = new List<Herramienta>()
            {
                new Herramienta(1, "Taladro", "Metal y Plástico", 75.0f, 7, fabricante[0]),
                new Herramienta(2, "Sierra Circular", "Metal y Madera", 120.0f, 10, fabricante[0]),
                new Herramienta(3, "Lijadora", "Plástico y Madera", 60.0f, 5, fabricante[0])
            };

            _context.AddRange(fabricante);
            _context.AddRange(herramientas);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_GetHerramientasPorNombreTiempoRep()
        {
            var herramientaDTOs = new List<HerramientasParaRepararDTO>()
            {
            new HerramientasParaRepararDTO(1, "Taladro", "Metal y Plástico", 75.0f, "Bosch", 7),
            new HerramientasParaRepararDTO(2, "Sierra Circular", "Metal y Madera", 120.0f, "Bosch", 10),
            new HerramientasParaRepararDTO(3, "Lijadora", "Plástico y Madera", 60.0f, "Bosch",5)
            };

            var herramientaDTOsTC1 = new List<HerramientasParaRepararDTO>()
            {
            herramientaDTOs[0],
            herramientaDTOs[1],
            herramientaDTOs[2]
            };

            var herramientaDTOsTC2 = new List<HerramientasParaRepararDTO>()
            {
            herramientaDTOs[0]
            };

            var herramientaDTOsTC3 = new List<HerramientasParaRepararDTO>()
            {
            herramientaDTOs[1]
            };

            var allTest = new List<object[]>()
            {
                new object[] { null, null, herramientaDTOsTC1 },
                new object[] { "Taladro", null, herramientaDTOsTC2 },
                new object[] { null, 10, herramientaDTOsTC3 },
            };

            return allTest;
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetHerramientasPorNombreTiempoRep))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit testing")]
        public async Task GetHerramientasPorNombreTiempoRep_test(string? nombre, int? tiempoRep, List<HerramientasParaRepararDTO> expectedHerramientas)
        {
            //Arrange
            var controller = new HerramientaController(_context, null);

            //Act
            var result = await controller.GetHerramientasPorNombreTiempoRep(nombre, tiempoRep);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var herramientaDTOsActual = Assert.IsType<List<HerramientasParaRepararDTO>>(okResult.Value);
            Assert.Equal(expectedHerramientas, herramientaDTOsActual);
        }


    }
}
