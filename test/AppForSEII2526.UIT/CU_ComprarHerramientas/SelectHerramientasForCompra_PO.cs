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
        By tableOfCompraItems = By.Id("TableOfHerramientas");
        By errorShownBy = By.Id("ErrorsShown");
        By buttonCompraHerramienta = By.Id("comprarHerramientaButton");
        By buttonCrearCompraCarrito = By.Id("crearCompraCarritoButton");

        public SelectHerramientasForCompra_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchHerramientas(string material, string precio)
        {
            //wait for the webelement to be clickable
            WaitForBeingClickable(inputMaterial);
            _driver.FindElement(inputMaterial).Clear();
            _driver.FindElement(inputMaterial).SendKeys(material);

            WaitForBeingClickable(inputPrecio);
            _driver.FindElement(inputPrecio).Clear();
            _driver.FindElement(inputPrecio).SendKeys(precio);

            _driver.FindElement(buttonSearchHerramientas).Click();
        }

        public void crearCompraCarrito()
        {
            WaitForBeingClickable(buttonCrearCompraCarrito);

            _driver.FindElement(buttonCrearCompraCarrito).Click();
        }

        public bool IsCrearCompraCarritoButtonActive()
        {
            var button = _driver.FindElements(By.Id("crearCompraCarritoButton"));

            var btn = button[0];

            // si está oculto por hidden del contenedor => Displayed = false
            if (!btn.Displayed) return false;

            if (btn.GetAttribute("disabled") != null) return false;

            return true;
        }

        public bool CheckListOfHerramientas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tableOfCompraItems);
        }

        public bool CheckMessageError(string errorMessage)
        {
            IWebElement actualErrorShown = _driver.FindElement(errorShownBy);
            _output.WriteLine($"actual Message shown:{actualErrorShown.Text}");
            return actualErrorShown.Text.Contains(errorMessage);
        }

        public void AddHerramientaToCompraCart(string herramientaNombre)
        {
            WaitForBeingClickable(By.Id("herramientaParaCompra_" + herramientaNombre));

            _driver.FindElement(By.Id("herramientaParaCompra_" + herramientaNombre)).Click();
        }

        public void RemoveHerramientaFromCompraCart(string herramientaTitle)
        {
            WaitForBeingClickable(By.Id("removeHerramienta_" + herramientaTitle));
            _driver.FindElement(By.Id("removeHerramienta_" + herramientaTitle)).Click();
        }

        public bool CompraNotAvailable()
        {
            try
            {
                return _driver.FindElement(buttonCrearCompraCarrito).Displayed == false;
            }
            catch (Exception e)
            {
                return true;
            }
        }
    }
}