using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.CU_CompraHerramientas;
using AppForSEII2526.UIT.CU_ComprarHerramientas;
using AppForSEII2526.UIT.CU_OfertaHerramientas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_CompraHerramientas_UIT
{
    public class CU_CompraHerramientas_UIT : UC_UIT
    {
        private SelectHerramientasForCompra_PO selectHerramientasForCompra_PO;
        private CrearCompra_PO crearCompra_PO;
        private DetailCompra_PO detailCompra_PO;
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

        public CU_CompraHerramientas_UIT(ITestOutputHelper output) : base(output)
        {
            selectHerramientasForCompra_PO = new SelectHerramientasForCompra_PO(_driver, _output);
            crearCompra_PO = new CrearCompra_PO(_driver, _output);
            detailCompra_PO = new DetailCompra_PO(_driver, _output);
        }

        /*private void Precondition_perform_login()
        {
            Perform_login("elena@uclm.es", "Password1234%");
        }*/

        private void InitialStepsForCompraHerramientas()
        {
            Initial_step_opening_the_web_page();

            //Precondition_perform_login();
            By id = By.Id("CrearCompra");
            selectHerramientasForCompra_PO.WaitForBeingVisible(id);

            Thread.Sleep(500);

            _driver.FindElement(id).Click();
        }

        [Theory]
        [InlineData(herramientaId1, herramientaNombre1)]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_BF_CrearCompra(string herramientaId, string herramientaNombre)
        {
            //Arrange
            InitialStepsForCompraHerramientas();

            Thread.Sleep(500);

            selectHerramientasForCompra_PO.AddHerramientaToCompraCart(herramientaNombre);

            Thread.Sleep(500);

            selectHerramientasForCompra_PO.crearCompraCarrito();

            Thread.Sleep(1000);

            crearCompra_PO.addAtributosCompra(herramientaId, "Carlos", "Gomez", "Avenida de España, 12", "Efectivo", "123456789", "carlosg@gmail.com", "Descripcion", "2");

            crearCompra_PO.pulsarCrearCompra();
            Thread.Sleep(500);

            Thread.Sleep(1000);

            crearCompra_PO.guardarCompraDialog();

            Thread.Sleep(5000);

            //Assert
            Assert.True(detailCompra_PO.CheckCompraDetail("Carlos", "Gomez", "Avenida de España, 12", DateTime.Now.ToString("dd/MM/yyyy"), "1"));
        }

        [Theory]
        [InlineData(herramientaId1, herramientaNombre1, herramientaMaterial1, herramientaPrecio1, herramientaFabricante1, "Acero y plástico", "")]
        [InlineData(herramientaId2, herramientaNombre2, herramientaMaterial2, herramientaPrecio2, herramientaFabricante2, "", "120,5")]
        [InlineData(herramientaId1, herramientaNombre1, herramientaMaterial1, herramientaPrecio1, herramientaFabricante1, "Acero y plástico", "89,99")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_AF1_filteringPorMaterialPrecio(string herramientaId, string herramientaNombre, string herramientaMaterial, string herramientaPrecio, string herramientaFabricante,
            string filtroMaterial, string filtroPrecio)
        {
            //Arrange
            InitialStepsForCompraHerramientas();
            var expectedHerramientas = new List<string[]> { new string[] { herramientaId, herramientaNombre, herramientaMaterial, herramientaPrecio, herramientaFabricante }, };

            //Act
            selectHerramientasForCompra_PO.SearchHerramientas(filtroMaterial, filtroPrecio);

            Thread.Sleep(500);

            //Assert
            Assert.True(selectHerramientasForCompra_PO.CheckListOfHerramientas(expectedHerramientas));
        }

        // El cliente elige modificar el carrito de compras para borrar aquellas herramientas que no le interesan.Automáticamente, el sistema actualiza el precio total del contenido del carrito de acuerdo con el precio de compra de las herramientas seleccionadas.
        [Theory]
        [InlineData(herramientaId1, herramientaNombre1, herramientaMaterial1, herramientaPrecio1, herramientaFabricante1,
            herramientaId2, herramientaNombre2, herramientaMaterial2, herramientaPrecio2, herramientaFabricante2)]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_AF2_modificarCarritoDeCompras(string herramientaId1, string herramientaNombre1, string herramientaMaterial1, string herramientaPrecio1, string herramientaFabricante1,
            string herramientaId2, string herramientaNombre2, string herramientaMaterial2, string herramientaPrecio2, string herramientaFabricante2)
        {
            //Arrange
            InitialStepsForCompraHerramientas();

            var expectedHerramientas = new List<string[]>
            {
                new string[] { herramientaNombre2, herramientaMaterial2, herramientaPrecio2 }
            };

            //Act
            selectHerramientasForCompra_PO.AddHerramientaToCompraCart(herramientaNombre1);
            Thread.Sleep(500);
            selectHerramientasForCompra_PO.AddHerramientaToCompraCart(herramientaNombre2);
            Thread.Sleep(500);

            selectHerramientasForCompra_PO.crearCompraCarrito();
            Thread.Sleep(1000);

            crearCompra_PO.modificarHerramientas();
            Thread.Sleep(1000);

            selectHerramientasForCompra_PO.RemoveHerramientaFromCompraCart(herramientaNombre1);
            Thread.Sleep(500);

            selectHerramientasForCompra_PO.crearCompraCarrito();
            Thread.Sleep(1000);

            //Assert
            Assert.True(crearCompra_PO.CheckListOfHerramientas(expectedHerramientas));
        }

        // Si el sistema detecta que no hay en el carrito ninguna herramienta para comprar, la opción para continuar el proceso no estará activa.
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_AF3_noHerramientasEnCarrito()
        {
            // Arrange
            InitialStepsForCompraHerramientas();

            Thread.Sleep(1000);

            // Assert: el botón no está activo (no visible o no existe)
            Assert.False(selectHerramientasForCompra_PO.IsCrearCompraCarritoButtonActive());
        }

        // Si el sistema detecta que algún dato obligatorio no se ha rellenado, notificará al usuario y volverá al paso 5.
        [Theory]
        // Nombre
        [InlineData(herramientaId1, herramientaNombre1, "", "Gomez", "Avenida de España, 12", "Efectivo", "123456789", "carlosg@gmail.com", "Descripcion", "2", "NombreCliente")]
        // Apellido
        [InlineData(herramientaId1, herramientaNombre1, "Carlos", "", "Avenida de España, 12", "Efectivo", "123456789", "carlosg@gmail.com", "Descripcion", "2", "ApellidoCliente")]
        // Dirección
        [InlineData(herramientaId1, herramientaNombre1, "Carlos", "Gomez", "", "Efectivo", "123456789", "carlosg@gmail.com", "Descripcion", "2", "DireccionEnvio")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_AF4_1_noDatoObligatorio_unico(
            string herramientaId, string herramientaNombre,
            string nombreCliente, string apellidoCliente, string direccionEnvio,
            string tipoMetodoPago, string telefono, string correoElectronico,
            string descripcion, string cantidad,
            string expectedKey)
        {
            // Arrange
            InitialStepsForCompraHerramientas();
            Thread.Sleep(500);

            selectHerramientasForCompra_PO.AddHerramientaToCompraCart(herramientaNombre); 
            Thread.Sleep(500);

            selectHerramientasForCompra_PO.crearCompraCarrito(); Thread.Sleep(1000);

            // Act
            crearCompra_PO.addAtributosCompra(herramientaId, nombreCliente, apellidoCliente, direccionEnvio, tipoMetodoPago, telefono, correoElectronico, descripcion, cantidad);
            Thread.Sleep(500);

            crearCompra_PO.pulsarCrearCompra();
            Thread.Sleep(500);

            // Assert
            Assert.True(crearCompra_PO.CheckValidationMessageExists(expectedKey));

        }

        // Si el sistema detecta que algún dato obligatorio no se ha rellenado, notificará al usuario y volverá al paso 5.
        [Theory]
        // Descripción
        [InlineData(herramientaId1, herramientaNombre1, "Carlos", "Gomez", "Avenida de España, 12", "Efectivo", "123456789", "carlosg@gmail.com", "", "2", "Errors: () The Descripcion field is required.")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_AF4_2_noDatoObligatorio_unico(
            string herramientaId, string herramientaNombre,
            string nombreCliente, string apellidoCliente, string direccionEnvio,
            string tipoMetodoPago, string telefono, string correoElectronico,
            string descripcion, string cantidad,
            string expectedError)
        {
            // Arrange
            InitialStepsForCompraHerramientas();
            Thread.Sleep(500);

            selectHerramientasForCompra_PO.AddHerramientaToCompraCart(herramientaNombre);
            Thread.Sleep(500);

            selectHerramientasForCompra_PO.crearCompraCarrito(); Thread.Sleep(1000);

            // Act
            crearCompra_PO.addAtributosCompra(herramientaId, nombreCliente, apellidoCliente, direccionEnvio, tipoMetodoPago, telefono, correoElectronico, descripcion, cantidad);
            Thread.Sleep(500);

            crearCompra_PO.pulsarCrearCompra();
            Thread.Sleep(500);

            crearCompra_PO.guardarCompraDialog();
            Thread.Sleep(500);

            // Assert
            Assert.True(crearCompra_PO.CheckMessageError(expectedError));

        }


        // Si el sistema detecta que la cantidad que el usuario desea comprar de cualquier herramienta es 0, la opción de continuar el proceso no estará activa.
        [Theory]
        [InlineData(herramientaId1, herramientaNombre1)]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_AF5_cantidadCero(string herramientaId, string herramientaNombre)
        {
            //Arrange
            InitialStepsForCompraHerramientas();
            Thread.Sleep(500);
            selectHerramientasForCompra_PO.AddHerramientaToCompraCart(herramientaNombre);
            Thread.Sleep(500);
            selectHerramientasForCompra_PO.crearCompraCarrito();
            Thread.Sleep(1000);
            crearCompra_PO.addAtributosCompra(herramientaId, "Carlos", "Gomez", "Avenida de España, 12", "Efectivo", "123456789", "carlosg@gmail.com", "Descripcion", "0");
            Thread.Sleep(1000);

            //Assert
            Assert.False(crearCompra_PO.IsSubmitEnabled());

        }
    }

}