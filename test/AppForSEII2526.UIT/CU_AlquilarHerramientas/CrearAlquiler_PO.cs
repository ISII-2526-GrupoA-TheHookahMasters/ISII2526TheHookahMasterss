using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppForSEII2526.UIT.CU_AlquilarHerramientas
{
    public class CrearAlquiler_PO : PageObject
    {
        By inputNombre = By.Id("inputNombre");
        By inputApellido = By.Id("inputApellido");
        By inputDireccion = By.Id("inputDireccion");
        By selectMetodoPago = By.Id("selectMetodoPago");
        By inputFechaInicio = By.Id("inputFechaInicio");
        By inputFechaFin = By.Id("inputFechaFin");
        By inputCantidad = By.Id("inputCantidad");
        By buttonCreateAlquiler = By.Id("buttonCreateAlquiler");
        By button_DialogOK = By.Id("Button_DialogOK");
        By modifyHerramientasButton = By.Id("ModifyHerramientas");
        By tableOfHerramientasBy = By.Id("TableOfHerramientas");
        By errorShownBy = By.Id("ErrorsShown");
        By buttonAlquilerHerramienta = By.Id("alquilerHerramientaButton");
        By tableOfAlquilerItems = By.Id("TableOfAlquilerItems");
        public CrearAlquiler_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void crearAlquiler()
        {
            WaitForBeingClickable(buttonCreateAlquiler);
            _driver.FindElement(buttonCreateAlquiler).Click();
        }

        public void guardarAlquilerDialog()
        {
            WaitForBeingClickable(button_DialogOK);
            _driver.FindElement(button_DialogOK).Click();
        }

        public void modificarHerramientas()
        {
            WaitForBeingClickable(modifyHerramientasButton);
            _driver.FindElement(modifyHerramientasButton).Click();
        }

        public void addAtributosAlquiler(string herramientaId,string nombre,string apellido, string direccion, string fechaInicio, string fechaFinal, string metodoPago, string cantidad)
        {

            WaitForBeingClickable(inputNombre);
            _driver.FindElement(inputNombre).SendKeys(nombre);

            WaitForBeingClickable(inputApellido);
            _driver.FindElement(inputApellido).SendKeys(apellido);

            WaitForBeingClickable(inputDireccion);
            _driver.FindElement(inputDireccion).SendKeys(direccion);

            WaitForBeingClickable(By.Id("inputFechaInicio"));
            _driver.FindElement(By.Id("inputFechaInicio")).SendKeys(fechaInicio);

            WaitForBeingClickable(By.Id("inputFechaFin"));
            _driver.FindElement(By.Id("inputFechaFin")).SendKeys(fechaFinal);

            if (metodoPago == "") metodoPago = "Tarjeta de Crédito";
            SelectElement selectElement = new SelectElement(_driver.FindElement(By.Id("selectMetodoPago")));
            selectElement.SelectByText(metodoPago);

            

            var qty = _driver.FindElement(By.Id($"inputCantidad_{herramientaId}"));
            qty.SendKeys(Keys.Control + "a");
            qty.SendKeys(cantidad);
            qty.SendKeys(Keys.Tab);


        }
        public void pulsarCrearAlquiler()
        {
            WaitForBeingClickable(buttonCreateAlquiler);
            _driver.FindElement(buttonCreateAlquiler).Click();
        }

        public bool CheckListOfAlquilerItems(List<string[]> expectedAlquilerItems)
        {
            return CheckBodyTable(expectedAlquilerItems, tableOfAlquilerItems);
        }

        public bool CheckValidationError(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }


        // Assert.False(crearCompra_PO.IsSubmitEnabled());
        public bool IsSubmitEnabled()
        {
            var btn = _driver.FindElement(By.Id("buttonCreateAlquiler"));
            return btn.Enabled; // si tiene disabled => false
        }
    }
}
