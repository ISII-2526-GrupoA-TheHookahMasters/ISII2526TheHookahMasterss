using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_OfertaHerramientas
{
    public class CrearOferta_PO : PageObject
    {
        By buttonCrearOferta = By.Id("crearOfertaButton");
        By button_DialogOK = By.Id("Button_DialogOK");
        By modifyHerramientasButton = By.Id("modifyHerramientasButton");
        By tableOfOfertaItems = By.Id("TableOfOfertaItems");

        public CrearOferta_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void crearOferta()
        {
            WaitForBeingClickable(buttonCrearOferta);
            _driver.FindElement(buttonCrearOferta).Click();
        }

        public void guardarOfertaDialog()
        {
            WaitForBeingClickable(button_DialogOK);
            _driver.FindElement(button_DialogOK).Click();
        }

        public void modificarHerramientas()
        {
            WaitForBeingClickable(modifyHerramientasButton);
            _driver.FindElement(modifyHerramientasButton).Click();
        }

        public void addAtributosOferta(string herramientaId, string fechaInicio, string fechaFinal, string metodoPago, string dirigidaA, string porcentaje)
        {
            WaitForBeingClickable(By.Id("inputFechaInicio"));
            _driver.FindElement(By.Id("inputFechaInicio")).SendKeys(fechaInicio);

            WaitForBeingClickable(By.Id("inputFechaFinal"));
            _driver.FindElement(By.Id("inputFechaFinal")).SendKeys(fechaFinal);

            if (metodoPago == "") metodoPago = "Tarjeta de Crédito";
            SelectElement selectElement = new SelectElement(_driver.FindElement(By.Id("selectTipoMetodoPago")));
            selectElement.SelectByText(metodoPago);

            if (dirigidaA == "") dirigidaA = "Clientes";
            SelectElement selectElement2 = new SelectElement(_driver.FindElement(By.Id("selectTipoDirigidaOferta")));
            selectElement2.SelectByText(dirigidaA);

            WaitForBeingClickable(By.Id($"porcentaje_{herramientaId}"));
            //var inputPorcentaje = 
            _driver.FindElement(By.Id($"porcentaje_{herramientaId}")).Clear();
            _driver.FindElement(By.Id($"porcentaje_{herramientaId}")).SendKeys(porcentaje);


            _driver.FindElement(buttonCrearOferta).Click();

        }

        public bool CheckListOfOfertaItems(List<string[]> expectedOfertaItems)
        {
            return CheckBodyTable(expectedOfertaItems, tableOfOfertaItems);
        }

        public bool CheckValidationError(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }
    }
}