using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.CU_RepararHerramientas;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_RepararHerramientas
{
    public class CU_ReparacionHerramientas_UIT : UC_UIT
    {
        private SelectHerramientasForReparacion_PO selectHerramientasForReparacion_PO;
        private DetailReparacion_PO detailReparacion_PO;
        private CrearReparacion_PO crearReparacion_PO;
        private const string herramientaId1 = "1";
        private const string herramientaNombre1 = "Taladro percutor Bosch GSB 13 RE";
        private const string herramientaMaterial1 = "Acero y plástico";
        private const string herramientaPrecio1 = "89,99";
        private const string herramientaFabricante1 = "Bosch";
        private const string herramientaTiempoReparacion1 = "5";
        private const string herramientaId2 = "2";
        private const string herramientaNombre2 = "Sierra circular Makita HS7601";
        private const string herramientaMaterial2 = "Aluminio y acero";
        private const string herramientaPrecio2 = "120,5";
        private const string herramientaFabricante2 = "Makita";
        private const string herramientaTiempoReparacion2 = "7";


        public CU_ReparacionHerramientas_UIT(ITestOutputHelper output) : base(output)
        {
            selectHerramientasForReparacion_PO = new SelectHerramientasForReparacion_PO(_driver, _output);
            detailReparacion_PO = new DetailReparacion_PO(_driver, _output);
            crearReparacion_PO = new CrearReparacion_PO(_driver, _output);
        }

        /*private void Precondition_perform_login()
        {
            Perform_login("elena@uclm.es", "Password1234%");
        }*/

        private void InitialStepsForReparacionHerramientas()
        {
            Initial_step_opening_the_web_page();

            //Precondition_perform_login();
            By id = By.Id("CrearReparacion");
            selectHerramientasForReparacion_PO.WaitForBeingVisible(id);

            Thread.Sleep(500);

            _driver.FindElement(id).Click();
        }

        [Theory]
        [InlineData(herramientaId1, herramientaNombre1, herramientaPrecio1)]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_1_BF_CrearReparacion(string herramientaId, string herramientaNombre, string herramientaPrecio1)
        {
            InitialStepsForReparacionHerramientas();
            Thread.Sleep(500);
            selectHerramientasForReparacion_PO.AddHerramientaToReparacionCart(herramientaId);
            Thread.Sleep(500);
            selectHerramientasForReparacion_PO.crearReparacionCarrito();
            Thread.Sleep(1000);
            crearReparacion_PO.addAtributosReparacion(herramientaId, herramientaNombre, "Carlos", "Gomez", DateTime.Today.AddDays(1).ToString(), "Tarjeta de Crédito", "1", "Rotura");
            Thread.Sleep(300);
            crearReparacion_PO.pulsarCrearReparacion();
            Thread.Sleep(1000);
            crearReparacion_PO.guardarReparacionDialog();
            Thread.Sleep(1000);
            Assert.True(detailReparacion_PO.CheckReparacionDetail("Carlos", "Gomez", DateTime.Today.AddDays(1).ToString("dd/MM/yyyy"), DateTime.Today.AddDays(6).ToString("dd/MM/yyyy"), herramientaPrecio1, "1"));
        }

        [Theory]
        [InlineData(herramientaId1, herramientaNombre1, herramientaMaterial1, herramientaPrecio1, herramientaFabricante1, "Taladro percutor Bosch GSB 13 RE", "")]
        [InlineData(herramientaId2, herramientaNombre2, herramientaMaterial2, herramientaPrecio2, herramientaFabricante2, "", "7")]
        [InlineData(herramientaId1, herramientaNombre1, herramientaMaterial1, herramientaPrecio1, herramientaFabricante1, "Taladro percutor Bosch GSB 13 RE", "5")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_2_3_4_AF0_filtroPorFabricantePrecio(string herramientaId, string herramientaNombre, string herramientaMaterial, string herramientaPrecio, string herramientaFabricante,
            string filtroNombre, string filtroTiempoReparacion)
        {
            InitialStepsForReparacionHerramientas();
            Thread.Sleep(1000);
            var expectedHerramientas = new List<string[]> { new string[] { herramientaId, herramientaNombre, herramientaMaterial, herramientaPrecio, herramientaFabricante }, };
            selectHerramientasForReparacion_PO.SearchHerramientas(filtroNombre, filtroTiempoReparacion);
            Thread.Sleep(1000);
            Assert.True(selectHerramientasForReparacion_PO.CheckListOfHerramientas(expectedHerramientas));
        }

        [Theory]
        [InlineData(herramientaId1, herramientaNombre1, "12/12/2025", "Error! La fecha de entrega debe ser posterior a hoy")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_5_6_7_AF1_FechasErroneas(string herramientaId, string herramientaNombre, string fechaEntrega, string expectedError)
        {
            InitialStepsForReparacionHerramientas();
            Thread.Sleep(1000);
            selectHerramientasForReparacion_PO.AddHerramientaToReparacionCart(herramientaId);
            Thread.Sleep(1000);
            selectHerramientasForReparacion_PO.crearReparacionCarrito();
            Thread.Sleep(1000);
            crearReparacion_PO.addAtributosReparacion(herramientaId, herramientaNombre, "Carlos", "Gomez", fechaEntrega, "Tarjeta de Crédito", "1", "Rotura");
            Thread.Sleep(300);
            crearReparacion_PO.pulsarCrearReparacion();
            Thread.Sleep(1000);
            crearReparacion_PO.guardarReparacionDialog();
            Thread.Sleep(1000);

            Assert.True(crearReparacion_PO.CheckValidationError(expectedError), $"Expected error: {expectedError}");
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_AF2_ModificarCarrito()
        {
            InitialStepsForReparacionHerramientas();
            selectHerramientasForReparacion_PO.AddHerramientaToReparacionCart(herramientaId1);
            selectHerramientasForReparacion_PO.AddHerramientaToReparacionCart(herramientaId2);
            Thread.Sleep(1000);
            selectHerramientasForReparacion_PO.crearReparacionCarrito();
            Thread.Sleep(500);
            crearReparacion_PO.modificarHerramientas();
            Thread.Sleep(500);
            selectHerramientasForReparacion_PO.RemoveHerramientaFromReparacionCart(herramientaNombre1);
            Thread.Sleep(500);
            selectHerramientasForReparacion_PO.crearReparacionCarrito();
            Thread.Sleep(500);
            var expectedReparacionItems = new List<string[]> { new string[] { herramientaNombre2, herramientaTiempoReparacion2, herramientaPrecio2 }, };

            Assert.True(crearReparacion_PO.CheckListOfReparacionItems(expectedReparacionItems));
        }





        [Theory]
        [InlineData(herramientaId1, herramientaNombre1, "", "Gomez", "20/12/2025", "2", "The NombreCliente field is required.")]
        [InlineData(herramientaId1, herramientaNombre1, "Carlos", "", "20/12/2025", "2", "The ApellidosCliente field is required.")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_11_12_AF5_CamposObligatorios(string herramientaId, string herramientaNombre, string nombreCliente, string apellidoCliente, string fechaEntrega, string cantidad, string expectedError)
        {
            InitialStepsForReparacionHerramientas();
            Thread.Sleep(1000);
            selectHerramientasForReparacion_PO.AddHerramientaToReparacionCart(herramientaId);
            Thread.Sleep(1000);
            selectHerramientasForReparacion_PO.crearReparacionCarrito();
            Thread.Sleep(1000);
            crearReparacion_PO.addAtributosReparacion(herramientaId, herramientaNombre, nombreCliente, apellidoCliente, fechaEntrega, "Tarjeta de Crédito", cantidad, "Rotura");
            Thread.Sleep(300);
            crearReparacion_PO.pulsarCrearReparacion();
            Thread.Sleep(1000);
            /*
                crearReparacion_PO.guardarReparacionDialog();
                Thread.Sleep(1000);
            */
            Assert.True(crearReparacion_PO.CheckValidationError(expectedError), $"Expected error: {expectedError}");
        }

        [Theory]
        [InlineData(herramientaId1, herramientaNombre1, "Carlos", "Gomez", "20/12/2025", "0")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_11_12_AF5_CantidadErronea(string herramientaId, string herramientaNombre, string nombreCliente, string apellidoCliente, string fechaEntrega, string cantidad)
        {
            InitialStepsForReparacionHerramientas();
            Thread.Sleep(2000);
            selectHerramientasForReparacion_PO.AddHerramientaToReparacionCart(herramientaId);
            Thread.Sleep(2000);
            selectHerramientasForReparacion_PO.crearReparacionCarrito();
            Thread.Sleep(2000);
            crearReparacion_PO.addAtributosReparacion(herramientaId, herramientaNombre, nombreCliente, apellidoCliente, fechaEntrega, "Tarjeta de Crédito", cantidad, "Rotura");
            Thread.Sleep(300);


            Assert.False(crearReparacion_PO.IsSubmitEnabled());
        }

        
        [Theory]
        [InlineData(herramientaId1, herramientaNombre1, herramientaMaterial1, herramientaPrecio1, herramientaFabricante1, "Taladro percutor Bosch GSB 13 RE", "7")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_ExamenSprint3_RepararHerramientas(string herramientaId, string herramientaNombre, string herramientaMaterial, string herramientaPrecio, string herramientaFabricante,
                string filtroNombre, string filtroTiempoReparacion)
        {
            InitialStepsForReparacionHerramientas();
            Thread.Sleep(1000);
            var expectedHerramientas1 = new List<string[]> { new string[] { herramientaId, herramientaNombre, herramientaMaterial, herramientaPrecio, herramientaFabricante }, };
            selectHerramientasForReparacion_PO.SearchHerramientas(filtroNombre, "");
            Thread.Sleep(1000);
            Assert.True(selectHerramientasForReparacion_PO.CheckListOfHerramientas(expectedHerramientas1));
            Thread.Sleep(1000);
            selectHerramientasForReparacion_PO.AddHerramientaToReparacionCart("1");

            var expectedHerramientas2 = new List<string[]> { new string[] { "2", "Sierra circular Makita HS7601", "Aluminio y acero", "120,5", "Makita" }, };
            selectHerramientasForReparacion_PO.SearchHerramientas("", filtroTiempoReparacion);
            Thread.Sleep(1000);
            Assert.True(selectHerramientasForReparacion_PO.CheckListOfHerramientas(expectedHerramientas2));
            Thread.Sleep(1000);
            selectHerramientasForReparacion_PO.AddHerramientaToReparacionCart("2");
            Thread.Sleep(1000);

            selectHerramientasForReparacion_PO.crearReparacionCarrito();
            Thread.Sleep(500);
            crearReparacion_PO.modificarHerramientas();
            Thread.Sleep(500);
            selectHerramientasForReparacion_PO.RemoveHerramientaFromReparacionCart(herramientaNombre1);
            Thread.Sleep(500);
            selectHerramientasForReparacion_PO.crearReparacionCarrito();
            Thread.Sleep(500);
            var expectedReparacionItems = new List<string[]> { new string[] { "Sierra circular Makita HS7601", "7", "120,5" }, };
            Assert.True(crearReparacion_PO.CheckListOfReparacionItems(expectedReparacionItems));
            Thread.Sleep(500);

            crearReparacion_PO.addAtributosReparacion("2", "Sierra circular Makita HS7601", "Carlos", "Gomez", DateTime.Today.AddDays(1).ToString(), "Tarjeta de Crédito", "1", "Rotura");
            Thread.Sleep(300);
            crearReparacion_PO.pulsarCrearReparacion();
            Thread.Sleep(1000);
            crearReparacion_PO.guardarReparacionDialog();
            Thread.Sleep(1000);
            Assert.True(detailReparacion_PO.CheckReparacionDetail("Carlos", "Gomez", DateTime.Today.AddDays(1).ToString("dd/MM/yyyy"), DateTime.Today.AddDays(8).ToString("dd/MM/yyyy"), "120,5", "1"));
        }
    }
}

