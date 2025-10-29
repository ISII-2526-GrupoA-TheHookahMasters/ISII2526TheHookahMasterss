using AppForSEII2526;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.HerramientasController_test
{
    public class GetSeleccionAlquiler_test : AppForSEII25264SqliteUT
    {
        public GetSeleccionAlquiler_test()
        {
            var fabricante = new List<Fabricante>()
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


        public static IEnumerable<object[]> TestCasesFor_GetHerramientasNombreMaterial()
        {
            var herramientaDTOs = new List<HerramientasParaAlquilarDTO>()
            {
                new HerramientasParaAlquilarDTO(1, "Martillo", "Acero", 23.4f, "Bosch"),
                new HerramientasParaAlquilarDTO(2, "Llave inglesa", "Hierro", 15.9f, "Makita"),
                new HerramientasParaAlquilarDTO(3, "Destornillador", "Acero y Plastico", 20.0f, "Stanley")
            };

            var herramientaDTOsTC1 = new List<HerramientasParaAlquilarDTO>()
            {
                herramientaDTOs[0],
                herramientaDTOs[1],
                herramientaDTOs[2]

            };

            var herramientaDTOsTC2 = new List<HerramientasParaAlquilarDTO>()
            {
                herramientaDTOs[1]
            };

            var herramientaDTOsTC3 = new List<HerramientasParaAlquilarDTO>()
            {
                herramientaDTOs[2]
            };


            var allTest = new List<object[]>()
            {
                new object[]{null, null, herramientaDTOsTC1 },
                new object[]{ "Llave inglesa", null, herramientaDTOsTC2 },
                new object[]{null, "Acero y Plastico", herramientaDTOsTC3 }
            };

            return allTest;
        }


        [Theory]
        [MemberData(nameof(TestCasesFor_GetHerramientasNombreMaterial))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit testing")]
        public async Task GetHerramientasNombreMaterial_test(string nombre, string material, List<HerramientasParaAlquilarDTO> expectedHerramientas)
        {
            var controller = new HerramientaController(_context, null);

            var result = await controller.GetHerramientasNombreMaterial(nombre, material);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var herramientaDTOsActual = Assert.IsType<List<HerramientasParaAlquilarDTO>>(okResult.Value);
            Assert.Equal(expectedHerramientas, herramientaDTOsActual);
        }




    }
}