using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.CU_RepararHerramientas;
using OpenQA.Selenium.DevTools.V137.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_RepararHerramientas
{
    public class SelectHerramientasForReparacion_PO : PageObject
    {
        By inputTiempoReparacion = By.Id("inputTiempoReparacion");
        By inputNombreHerramienta = By.Id("inputNombreHerramienta");
        By buttonSearchHerramientas = By.Id("searchHerramientas");
        By tableOfHerramientasBy = By.Id("TableOfHerramientas");
        By errorShownBy = By.Id("ErrorsShown");
        By buttonCrearReparacionCarrito = By.Id("crearReparacionCarritoButton");


        public SelectHerramientasForReparacion_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchHerramientas(string nombreHerramienta, string tiempoReparacion)
        {
            //wait for the webelement to be clickable
            WaitForBeingClickable(inputTiempoReparacion);
            _driver.FindElement(inputTiempoReparacion).SendKeys(tiempoReparacion);
            Thread.Sleep(1000);
            WaitForBeingClickable(inputNombreHerramienta);
            _driver.FindElement(inputNombreHerramienta).SendKeys(nombreHerramienta);
            Thread.Sleep(1000);
            _driver.FindElement(buttonSearchHerramientas).Click();
        }

        public void crearReparacionCarrito()
        {
            WaitForBeingClickable(buttonCrearReparacionCarrito);
            _driver.FindElement(buttonCrearReparacionCarrito).Click();
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

        public void AddHerramientaToReparacionCart(string herramientaId)
        {
            WaitForBeingClickable(By.Id("herramientaParaReparar_" + herramientaId));
            _driver.FindElement(By.Id("herramientaParaReparar_" + herramientaId)).Click();
        }

        public void RemoveHerramientaFromReparacionCart(string herramientaNombre)
        {
            WaitForBeingClickable(By.Id("removeHerramienta_" + herramientaNombre));
            _driver.FindElement(By.Id("removeHerramienta_" + herramientaNombre)).Click();
        }

        public bool ReparacionNotAvailable()
        {
            try
            {
                return _driver.FindElement(buttonCrearReparacionCarrito).Displayed == false;
            }
            catch (Exception e)
            {
                return true;
            }
        }
    }
}
