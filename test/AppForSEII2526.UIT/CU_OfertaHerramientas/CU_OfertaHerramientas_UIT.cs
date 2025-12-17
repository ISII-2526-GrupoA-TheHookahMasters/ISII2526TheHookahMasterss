using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.CU_OfertaHerramientas;
using OpenQA.Selenium.DevTools.V137.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_OfertaHerramientas_UIT
{
    public class CU_OfertaHerramientas_UIT : UC_UIT
    {
        private SelectHerramientasForOferta_PO selectHerramientasForOferta_PO;
        private DetailOferta_PO detailOferta_PO;
        private CrearOferta_PO crearOferta_PO;
        private const string herramientaId1 = "1";
        private const string herramientaNombre1 = "Taladro percutor Bosch GSB 13 RE";
        private const string herramientaMaterial1 = "Acero y plástico";
        private const string herramientaPrecio1 = "89,99";
        private const string herramientaFabricante1 = "Bosch";
        private const string herramientaId2 = "2";
        private const string herramientaNombre2 = "Sierra circular Makita HS7601";
        private const string herramientaMaterial2 = "Aluminio y acero";
        private const string herramientaPrecio2 = "120,5";
        private const string herramientaFabricante2 = "Makita";
        private const string porcentaje1 = "-4";
        private const string porcentaje2 = "105";

        public CU_OfertaHerramientas_UIT(ITestOutputHelper output) : base(output)
        {
            selectHerramientasForOferta_PO = new SelectHerramientasForOferta_PO(_driver, _output);
            detailOferta_PO = new DetailOferta_PO(_driver, _output);
            crearOferta_PO = new CrearOferta_PO(_driver, _output);
        }

        /*private void Precondition_perform_login()
        {
            Perform_login("elena@uclm.es", "Password1234%");
        }*/

        private void InitialStepsForOfertaHerramientas()
        {
            Initial_step_opening_the_web_page();

            //Precondition_perform_login();
            By id = By.Id("CreateOferta");
            selectHerramientasForOferta_PO.WaitForBeingVisible(id);

            Thread.Sleep(500);

            _driver.FindElement(id).Click();
        }

        [Theory]
        [InlineData(herramientaId1, herramientaNombre1)]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_1_BF_CrearOferta(string herramientaId, string herramientaNombre)
        {
            InitialStepsForOfertaHerramientas();
            Thread.Sleep(500);
            selectHerramientasForOferta_PO.AddHerramientaToOfertaCart(herramientaNombre);
            Thread.Sleep(500);
            selectHerramientasForOferta_PO.crearOfertaCarrito();
            Thread.Sleep(1000);
            crearOferta_PO.addAtributosOferta(herramientaId, DateTime.Today.AddDays(3).ToString(), DateTime.Today.AddDays(11).ToString(), "PayPal", "Socios", "10");
            Thread.Sleep(1000);
            crearOferta_PO.guardarOfertaDialog();
            Thread.Sleep(1000);
            Assert.True(detailOferta_PO.CheckOfertaDetail(DateTime.Today.AddDays(3), DateTime.Today.AddDays(11), DateTime.Today, "PayPal", "Socios", 1));
        }

        [Theory]
        [InlineData(herramientaId1, herramientaNombre1, herramientaMaterial1, herramientaPrecio1, herramientaFabricante1, "Bosch", "")]
        [InlineData(herramientaId2, herramientaNombre2, herramientaMaterial2, herramientaPrecio2, herramientaFabricante2, "", "120,5")]
        [InlineData(herramientaId1, herramientaNombre1, herramientaMaterial1, herramientaPrecio1, herramientaFabricante1, "Bosch", "89,99")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_2_3_4_AF0_filtroPorFabricantePrecio(string herramientaId, string herramientaNombre, string herramientaMaterial, string herramientaPrecio, string herramientaFabricante,
            string filtroFabricante, string filtroPrecio)
        {
            InitialStepsForOfertaHerramientas();
            var expectedHerramientas = new List<string[]> { new string[] { herramientaId, herramientaNombre, herramientaMaterial, herramientaPrecio, herramientaFabricante }, };
            selectHerramientasForOferta_PO.SearchHerramientas(filtroFabricante, filtroPrecio);
            Thread.Sleep(500);
            Assert.True(selectHerramientasForOferta_PO.CheckListOfHerramientas(expectedHerramientas));
        }

        [Theory]
        [InlineData(herramientaId1, herramientaNombre1, "26/12/2025", "14/12/2025", "Error! Tu oferta debe terminar después de que empiece")]
        [InlineData(herramientaId1, herramientaNombre1, "09/12/2025", "14/12/2025", "Error! La fecha de inicio de tu oferta debe ser posterior a hoy")]
        [InlineData(herramientaId1, herramientaNombre1, "14/12/2025", "17/12/2025", "¡Error!, la oferta debe durar al menos una semana")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_5_6_7_AF1_FechasErroneas(string herramientaId, string herramientaNombre, string fechaInicio, string fechaFinal, string expectedError)
        {
            InitialStepsForOfertaHerramientas();
            Thread.Sleep(2000);
            selectHerramientasForOferta_PO.AddHerramientaToOfertaCart(herramientaNombre);
            Thread.Sleep(2000);
            selectHerramientasForOferta_PO.crearOfertaCarrito();
            Thread.Sleep(2000);
            crearOferta_PO.addAtributosOferta(herramientaId, fechaInicio, fechaFinal, "Efectivo", "Socios", "10");
            Thread.Sleep(2000);
            crearOferta_PO.guardarOfertaDialog();
            Thread.Sleep(1000);

            Assert.True(crearOferta_PO.CheckValidationError(expectedError), $"Expected error: {expectedError}");
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_AF2_ModificarCarrito()
        {
            InitialStepsForOfertaHerramientas();
            selectHerramientasForOferta_PO.AddHerramientaToOfertaCart(herramientaNombre1);
            selectHerramientasForOferta_PO.AddHerramientaToOfertaCart(herramientaNombre2);
            Thread.Sleep(1000);
            selectHerramientasForOferta_PO.crearOfertaCarrito();
            Thread.Sleep(500);
            crearOferta_PO.modificarHerramientas();
            Thread.Sleep(500);
            selectHerramientasForOferta_PO.RemoveHerramientaFromOfertaCart(herramientaNombre1);
            Thread.Sleep(500);
            selectHerramientasForOferta_PO.crearOfertaCarrito();
            Thread.Sleep(500);
            var expectedOfertaItems = new List<string[]> { new string[] { herramientaNombre2, herramientaMaterial2, herramientaPrecio2 }, };

            Assert.True(crearOferta_PO.CheckListOfOfertaItems(expectedOfertaItems));
        }

        [Theory]
        [InlineData(herramientaId1, herramientaNombre1, porcentaje1, "El porcentaje debe estar entre 0 y 100")]
        [InlineData(herramientaId1, herramientaNombre1, porcentaje2, "El porcentaje debe estar entre 0 y 100")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_8_9_AF3_PorcentajeErroneo(string herramientaId, string herramientaNombre, string porcentaje, string expectedError)
        {
            InitialStepsForOfertaHerramientas();
            Thread.Sleep(500);
            selectHerramientasForOferta_PO.AddHerramientaToOfertaCart(herramientaNombre);
            Thread.Sleep(500);
            selectHerramientasForOferta_PO.crearOfertaCarrito();
            Thread.Sleep(2000);
            crearOferta_PO.addAtributosOferta(herramientaId, DateTime.Today.AddDays(5).ToString("dd/MM/yyyy"),
                DateTime.Today.AddDays(15).ToString("dd/MM/yyyy"), "Tarjeta de Crédito", "Clientes", porcentaje);
            Thread.Sleep(2000);
            crearOferta_PO.guardarOfertaDialog();
            Thread.Sleep(1000);

            Assert.True(crearOferta_PO.CheckValidationError(expectedError), $"Expected error: {expectedError}");
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_10_AF4_CarritoVacio()
        {
            InitialStepsForOfertaHerramientas();
            Thread.Sleep(500);

            Assert.True(selectHerramientasForOferta_PO.OfertaNotAvailable());
        }

        [Theory]
        [InlineData(herramientaId1, herramientaNombre1, "20/12/2025", "", "10", "Error! Fecha Final es un campo obligatorio")]
        [InlineData(herramientaId1, herramientaNombre1, "", "30/12/2025", "10", "Error! Fecha Inicio es un campo obligatorio")]
        [InlineData(herramientaId1, herramientaNombre1, "20/12/2025", "30/12/2025", "0", "Error: El porcentaje es un campo obligatorio")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_11_12_AF5_CamposObligatorios(string herramientaId, string herramientaNombre, string fechaInicio, string fechaFinal, string porcentaje, string expectedError)
        {
            InitialStepsForOfertaHerramientas();
            Thread.Sleep(2000);
            selectHerramientasForOferta_PO.AddHerramientaToOfertaCart(herramientaNombre);
            Thread.Sleep(2000);
            selectHerramientasForOferta_PO.crearOfertaCarrito();
            Thread.Sleep(2000);
            crearOferta_PO.addAtributosOferta(herramientaId, fechaInicio, fechaFinal, "Efectivo", "Socios", porcentaje);
            Thread.Sleep(2000);
            crearOferta_PO.guardarOfertaDialog();
            Thread.Sleep(1000);

            Assert.True(crearOferta_PO.CheckValidationError(expectedError), $"Expected error: {expectedError}");
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_examen()
        {
            string fabricante = herramientaFabricante1;
            string precio = herramientaPrecio2;
            float precio2 = float.Parse(precio);
            int porcentaje = 10;

            InitialStepsForOfertaHerramientas();
            Thread.Sleep(1000);
            selectHerramientasForOferta_PO.SearchHerramientas(fabricante, "");
            Thread.Sleep(1000);
            selectHerramientasForOferta_PO.AddHerramientaToOfertaCart(herramientaNombre1);
            Thread.Sleep(1000);
            selectHerramientasForOferta_PO.SearchHerramientas("", precio);
            Thread.Sleep(1000);
            selectHerramientasForOferta_PO.AddHerramientaToOfertaCart(herramientaNombre2);
            Thread.Sleep(1000);
            selectHerramientasForOferta_PO.crearOfertaCarrito();
            Thread.Sleep(1000);
            crearOferta_PO.modificarHerramientas();
            Thread.Sleep(1000);
            selectHerramientasForOferta_PO.RemoveHerramientaFromOfertaCart(herramientaNombre1);
            Thread.Sleep(1000);
            selectHerramientasForOferta_PO.crearOfertaCarrito();
            Thread.Sleep(1000);
            var expectedOfertaItems = new List<string[]> { new string[] { herramientaNombre2, herramientaMaterial2, herramientaPrecio2 }, };
            Assert.True(crearOferta_PO.CheckListOfOfertaItems(expectedOfertaItems));

            crearOferta_PO.addAtributosOferta(herramientaId2, DateTime.Today.AddDays(3).ToString(), DateTime.Today.AddDays(11).ToString(), "PayPal", "Socios", "10");
            Thread.Sleep(1000);
            crearOferta_PO.guardarOfertaDialog();
            Thread.Sleep(1000);
            Assert.True(detailOferta_PO.CheckOfertaDetail(DateTime.Today.AddDays(3), DateTime.Today.AddDays(11), DateTime.Today, "PayPal", "Socios", 1));

            float precioTotal = precio2 * (1 - (porcentaje / 100.0f));

            var expectedOfertaItems2 = new List<string[]> { new string[] { herramientaId2, herramientaNombre2, herramientaMaterial2, herramientaFabricante2, $"{precio2.ToString("0.00")} €", $"{porcentaje.ToString()} %", $"{precioTotal.ToString()} €" }, };

            Assert.True(detailOferta_PO.CheckListaHerramientas(expectedOfertaItems2));
        }
    }
}
