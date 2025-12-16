using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_OfertaHerramientas
{
    public class SelectHerramientasForAlquiler_PO : PageObject
    {
        By inputNombre = By.Id("inputNombre");
        By inputMaterial = By.Id("inputMaterial");
        By buttonSearchHerramientas = By.Id("SearchHerramientas");
        By tableOfHerramientasBy = By.Id("TableOfHerramientas");
        By errorShownBy = By.Id("ErrorsShown");
        By buttonAlquilerHerramienta = By.Id("alquilerHerramientaButton");
        By button_DialogOK = By.Id("Button_DialogOK");
        public SelectHerramientasForAlquiler_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchHerramientas(string nombre, string material)
        {
            WaitForBeingClickable(inputNombre);
            _driver.FindElement(inputNombre).SendKeys(nombre);

            //wait for the webelement to be clickable
            WaitForBeingClickable(inputMaterial);
            _driver.FindElement(inputMaterial).SendKeys(material);
            

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

        public void AddHerramientaToAlquilerCart(string herramientaTitle)
        {
            WaitForBeingClickable(By.Id("herramientaToAlquiler_" + herramientaTitle));

            _driver.FindElement(By.Id("herramientaToAlquiler_" + herramientaTitle)).Click();
        }

        public void guardarAlquilerDialog()
        {
            WaitForBeingClickable(button_DialogOK);
            _driver.FindElement(button_DialogOK).Click();
        }

        public void RemoveHerramientaFromAlquilerCart(string herramientaTitle)
        {
            WaitForBeingClickable(By.Id("removeHerramienta_" + herramientaTitle));
            _driver.FindElement(By.Id("removeHerramienta_" + herramientaTitle)).Click();
        }

        public bool AlquilerNotAvailable()
        {
            return _driver.FindElement(buttonAlquilerHerramienta).Displayed == false;
        }

        public void BotonAlquilerHerramienta()
        {
            WaitForBeingClickable(buttonAlquilerHerramienta);
            _driver.FindElement(buttonAlquilerHerramienta).Click();
        }

        public bool IsCrearAlquilerCarritoButtonActive()
        {
            var button = _driver.FindElements(By.Id("alquilerHerramientaButton"));

            var btn = button[0];

            // si está oculto por hidden del contenedor => Displayed = false
            if (!btn.Displayed) return false;

            if (btn.GetAttribute("disabled") != null) return false;

            return true;
        }

    }
}