using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_ComprarHerramientas
{
    public class CrearCompra_PO : PageObject
    {
        By buttonCrearCompra = By.Id("crearCompraButton");
        By button_DialogOK = By.Id("Button_DialogOK");
        By modifyHerramientasButton = By.Id("modifyHerramientasButton");
        By tableOfComprasItemsBy = By.Id("TableOfCompraItems");
        public CrearCompra_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void crearCompra()
        {
            WaitForBeingClickable(buttonCrearCompra);
            _driver.FindElement(buttonCrearCompra).Click();
        }

        public void guardarCompraDialog()
        {
            WaitForBeingClickable(button_DialogOK);
            _driver.FindElement(button_DialogOK).Click();
        }
        public void modificarHerramientas()
        {
            WaitForBeingClickable(modifyHerramientasButton);
            _driver.FindElement(modifyHerramientasButton).Click();
        }

        public void addAtributosCompra(string herramientaId, string nombreCliente, string apellidoCliente, string direccionEnvio, string metodoPago, string telefono, string correoElectronico, string descripcion, string cantidad)
        {
            WaitForBeingClickable(By.Id("Nombre"));
            _driver.FindElement(By.Id("Nombre")).SendKeys(nombreCliente);

            WaitForBeingClickable(By.Id("Apellido"));
            _driver.FindElement(By.Id("Apellido")).SendKeys(apellidoCliente);

            WaitForBeingClickable(By.Id("DireccionEnvio"));
            _driver.FindElement(By.Id("DireccionEnvio")).SendKeys(direccionEnvio);

            if (metodoPago == "") metodoPago = "Efectivo";
            SelectElement selectElement2 = new SelectElement(_driver.FindElement(By.Id("MetodoPago")));
            selectElement2.SelectByText(metodoPago);

            WaitForBeingClickable(By.Id("NumTelefono"));
            _driver.FindElement(By.Id("NumTelefono")).SendKeys(telefono);

            WaitForBeingClickable(By.Id("CorreoElectronico"));
            _driver.FindElement(By.Id("CorreoElectronico")).SendKeys(correoElectronico);

            WaitForBeingClickable(By.Id($"description_{herramientaId}"));
            _driver.FindElement(By.Id($"description_{herramientaId}")).SendKeys(descripcion);

            var qty = _driver.FindElement(By.Id($"quantity_{herramientaId}"));
            qty.SendKeys(Keys.Control + "a");
            qty.SendKeys(cantidad);
            qty.SendKeys(Keys.Tab);   // <- fuerza onchange (actualiza el modelo)

        }

        public void pulsarCrearCompra()
        {
            WaitForBeingClickable(buttonCrearCompra);
            _driver.FindElement(buttonCrearCompra).Click();
        }

        public bool CheckListOfHerramientas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tableOfComprasItemsBy);
        }

        // CheckValidationMessageExists(expectedKey)
        public bool CheckValidationMessageExists(string expectedKey)
        {
            By validationMessageBy = By.Id("ValidationSummary");
            WaitForBeingVisible(validationMessageBy);
            var validationMessagesElement = _driver.FindElement(validationMessageBy);
            var validationMessages = validationMessagesElement.FindElements(By.TagName("li"));
            foreach (var message in validationMessages)
            {
                if (message.Text.Contains(expectedKey))
                {
                    return true;
                }
            }
            return false;
        }

        // Assert.False(crearCompra_PO.IsSubmitEnabled());
        public bool IsSubmitEnabled()
        {
            var btn = _driver.FindElement(By.Id("crearCompraButton"));
            return btn.Enabled; // si tiene disabled => false
        }

        public bool CheckMessageError(string errorMessage)
        {
            IWebElement actualErrorShown = _driver.FindElement(By.Id("ErrorsShown"));
            _output.WriteLine($"actual Message shown:{actualErrorShown.Text}");
            return actualErrorShown.Text.Contains(errorMessage);
        }

    }
}
