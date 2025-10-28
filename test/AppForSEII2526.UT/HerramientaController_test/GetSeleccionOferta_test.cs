using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;

namespace AppForSEII2526.UT.HerramientasController_Test
{
    public class GetSeleccionOferta_test : AppForSEII25264SqliteUT
    {
        public GetSeleccionOferta_test()
        {
            var fabricante = new Fabricante(1, "Bosch");

            var herramientas = new List<Herramienta>()
            {
                new Herramienta(1, "Martillo", "Acero", 23.44f, 3),
                new Herramienta(2, "Llave inglesa", "Hierro", 23.44f, 3),
                new Herramienta(3, "Martillo", "Acero", 23.44f, 3)
            };

            var ofertas = new List<Oferta>()
            {
                new Oferta(1, new DateTime(2025,05,12), new DateTime(2025,05,12), new DateTime(2025,05,12), TiposMetodoPago.PayPal, TiposDirigidaOferta.Clientes),
                new Oferta(2, new DateTime(2025,05,12), new DateTime(2025,05,12), new DateTime(2025,05,12), TiposMetodoPago.PayPal, TiposDirigidaOferta.Clientes),
                new Oferta(3, new DateTime(2025,05,12), new DateTime(2025,05,12), new DateTime(2025,05,12), TiposMetodoPago.PayPal, TiposDirigidaOferta.Clientes)
            };


            var oferta = new Oferta(1, new DateTime(2025, 05, 12), new DateTime(2025, 05, 12), new DateTime(2025, 05, 12), TiposMetodoPago.PayPal, TiposDirigidaOferta.Clientes);

            oferta.OfertaItems.Add(new OfertaItem(oferta, herramientas[0]));

            _context.Add(oferta);
            _context.Add(fabricante);
            _context.AddRange(herramientas);
            _context.AddRange(ofertas);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_GetHerramientasPorFabricantePrecio()
        {
            var herramientaDTOs = new List<HerramientasParaOfertaDTO>()
            {
                new HerramientasParaOfertaDTO(1, "Martillo", "Acero", 23.4f, "Bosch"),
                new HerramientasParaOfertaDTO(2, "Llave inglesa", "Hierro", 15.9f, "Siemens"),
                new HerramientasParaOfertaDTO(2, "Martillo", "Acero", 23.4f, "Bosch")
            };

            var herramientaDTOsTC1 = new List<HerramientasParaOfertaDTO>()
            {
                herramientaDTOs[0],
                herramientaDTOs[1]

            };

            var herramientaDTOsTC2 = new List<HerramientasParaOfertaDTO>()
            {
                herramientaDTOs[1]
            };

            var herramientaDTOsTC3 = new List<HerramientasParaOfertaDTO>()
            {
                herramientaDTOs[2]
            };

            var herramientaDTOsTC4 = new List<HerramientasParaOfertaDTO>()
            {
                herramientaDTOs[0],
                herramientaDTOs[1],
                herramientaDTOs[2]
            };

            var allTest = new List<object>()
            {

            };

            return allTest;
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetHerramientasPorFabricantePrecio))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit testing")]
        public async Task GetHerramientasPorFabricantePrecio_test(string fabricante, int precio, IList<HerramientasParaOfertaDTO> expectedHerramientas)
        {
            var controller = new HerramientaController(_context, null);

            var result = await controller.GetHerramientasPorFabricantePrecio(fabricante, precio);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var herramientaDTOsActual = Assert.IsType<List<HerramientasParaOfertaDTO>>(okResult.Value);
            Assert.Equal(expectedHerramientas, herramientaDTOsActual);
        }
    }
}
