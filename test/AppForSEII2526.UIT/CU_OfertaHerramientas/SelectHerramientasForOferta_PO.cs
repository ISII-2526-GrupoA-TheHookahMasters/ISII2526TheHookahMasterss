using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_OfertaHerramientas
{
    public class SelectHerramientasForOferta_PO : PageObject
    {
        By inputFabricante = By.Id("selectFabricante");
        By inputPrecio = By.Id("inputPrecio");
        By buttonSearchHerramientas = By.Id("searchHerramientas");
        By tableOfHerramientasBy = By.Id("TableOfHerramientas");
        By errorShownBy = By.Id("ErrorsShown");
        By buttonCrearOfertaCarrito = By.Id("crearOfertaCarritoButton");
         

        public SelectHerramientasForOferta_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchHerramientas(string fabricante, string precio)
        {
            //wait for the webelement to be clickable
            WaitForBeingClickable(inputPrecio);
            _driver.FindElement(inputPrecio).SendKeys(precio);

            if (fabricante == "") fabricante = "All";
            SelectElement selectElement = new SelectElement(_driver.FindElement(inputFabricante));
            selectElement.SelectByText(fabricante);

            _driver.FindElement(buttonSearchHerramientas).Click();

            _driver.FindElement(inputPrecio).Clear();
        }

        public void crearOfertaCarrito()
        {
            WaitForBeingClickable(buttonCrearOfertaCarrito);
            _driver.FindElement(buttonCrearOfertaCarrito).Click();
        }

        public bool CheckListOfHerramientas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tableOfHerramientasBy);
        }

        public bool CheckMessageError(string errorMessage)
        {
            IWebElement actualErrorShown = _driver.FindElement(errorShownBy);
            _output.WriteLine($"actual Message shown:{actualErrorShown.Text}");
            return actualErrorShown.Text.Contains(errorMessage);
        }

        public void AddHerramientaToOfertaCart(string herramientaNombre)
        {
            WaitForBeingClickable(By.Id("herramientaParaOferta_" + herramientaNombre));
            _driver.FindElement(By.Id("herramientaParaOferta_" + herramientaNombre)).Click();
        }

        public void RemoveHerramientaFromOfertaCart(string herramientaNombre)
        {
            WaitForBeingClickable(By.Id("removeHerramienta_" + herramientaNombre));
            _driver.FindElement(By.Id("removeHerramienta_" + herramientaNombre)).Click();   
        }

        public bool OfertaNotAvailable()
        {
            try
            {
                return _driver.FindElement(buttonCrearOfertaCarrito).Displayed == false;
            } 
            catch(Exception e)
            {
                return true;
            }
        }
    }
}
