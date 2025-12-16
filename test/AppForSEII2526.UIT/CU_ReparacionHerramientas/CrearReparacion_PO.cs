using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_RepararHerramientas
{
    public class CrearReparacion_PO : PageObject
    {
        By buttonCrearReparacion = By.Id("crearReparacionButton");
        By button_DialogOK = By.Id("Button_DialogOK");
        By modifyHerramientasButton = By.Id("ModifyHerramientas");
        By tableOfReparacionItems = By.Id("TableOfReparacionItems");

        public CrearReparacion_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void crearReparacion()
        {
            WaitForBeingClickable(buttonCrearReparacion);
            _driver.FindElement(buttonCrearReparacion).Click();
        }

        public void guardarReparacionDialog()
        {
            WaitForBeingClickable(button_DialogOK);
            _driver.FindElement(button_DialogOK).Click();
        }

        public void modificarHerramientas()
        {
            WaitForBeingClickable(modifyHerramientasButton);
            _driver.FindElement(modifyHerramientasButton).Click();
        }

        public void addAtributosReparacion(string herramientaId, string herramientaNombre, string nombreCliente, string apellidoCliente, string fechaEntrega, string metodoPago, string cantidad, string descripcion)
        {
            WaitForBeingClickable(By.Id("inputNombre"));
            _driver.FindElement(By.Id("inputNombre")).SendKeys(nombreCliente);

            WaitForBeingClickable(By.Id("inputApellido"));
            _driver.FindElement(By.Id("inputApellido")).SendKeys(apellidoCliente);

            WaitForBeingClickable(By.Id("inputFechaEntrega"));
            _driver.FindElement(By.Id("inputFechaEntrega")).SendKeys(fechaEntrega);
            if (metodoPago == "") metodoPago = "Tarjeta de Crédito";
            SelectElement selectElement = new SelectElement(_driver.FindElement(By.Id("TipoMetodoPago")));
            selectElement.SelectByText(metodoPago);

            WaitForBeingClickable(By.Id($"Descripcion_{herramientaId}"));
            _driver.FindElement(By.Id($"Descripcion_{herramientaId}")).SendKeys(descripcion);

            var qty = _driver.FindElement(By.Id($"Cantidad_{herramientaId}"));
            qty.SendKeys(Keys.Control + "a");
            qty.SendKeys(cantidad);
            qty.SendKeys(Keys.Tab);
        }

        public bool CheckListOfReparacionItems(List<string[]> expectedReparacionItems)
        {
            return CheckBodyTable(expectedReparacionItems, tableOfReparacionItems);
        }

        public bool CheckValidationError(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }

        public bool IsSubmitEnabled()
        {
            var btn = _driver.FindElement(By.Id("crearReparacionButton"));
            return btn.Enabled; // si tiene disabled => false
        }

        public void pulsarCrearReparacion()
        {
            WaitForBeingClickable(buttonCrearReparacion);
            _driver.FindElement(buttonCrearReparacion).Click();
        }

    }
}
