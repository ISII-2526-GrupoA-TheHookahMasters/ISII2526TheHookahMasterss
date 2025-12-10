using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_CompraHerramientas
{
    public class SelectHerramientasForCompra_PO : PageObject
    {
        By inputMaterial = By.Id("inputMaterial");
        By inputPrecio = By.Id("inputPrecio");
        By buttonSearchHerramientas = By.Id("searchHerramientas");
        By tableOfHerramientasBy = By.Id("TableOfHerramientas");
        By errorShownBy = By.Id("ErrorsShown");
        By buttonCompraHerramienta = By.Id("comprarHerramientaButton");

        public SelectHerramientasForCompra_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchHerramientas(string material, string precio)
        {
            //wait for the webelement to be clickable
            WaitForBeingClickable(inputMaterial);
            _driver.FindElement(inputMaterial).SendKeys(material);

            WaitForBeingClickable(inputPrecio);
            _driver.FindElement(inputPrecio).SendKeys(precio);

            _driver.FindElement(buttonSearchHerramientas).Click();
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

        public void AddHerramientaToCompraCart(string herramientaTitle)
        {
            WaitForBeingClickable(By.Id("herramientaToCompra_" + herramientaTitle));

            _driver.FindElement(By.Id("herramientaToCompra" + herramientaTitle)).Click();
        }

        public void RemoveHerramientaFromCompraCart(string herramientaTitle)
        {
            WaitForBeingClickable(By.Id("removeHerramienta_" + herramientaTitle));
            _driver.FindElement(By.Id("removeHerramienta" + herramientaTitle)).Click();
        }

        public bool CompraNotAvailable()
        {
            return _driver.FindElement(buttonCompraHerramienta).Displayed == false;
        }
    }
}