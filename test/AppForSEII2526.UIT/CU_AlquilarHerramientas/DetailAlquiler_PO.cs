using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_AlquilarHerramientas
{
    internal class DetailAlquiler_PO : PageObject
    {
        By tablaHerramientasCompradas = By.Id("HerramientasAlquiladas");

        public DetailAlquiler_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public bool CheckAlquilerDetail(string nombreCliente, string apellidoCliente, string direccionEnvio, string fechaAlquiler, string periodo, string itemsAlquiler)
        {
            WaitForBeingVisible(tablaHerramientasCompradas);
            bool result = true;
            result = result && _driver.FindElement(By.Id("nombreCliente")).Text.Contains(nombreCliente);
            result = result && _driver.FindElement(By.Id("apellidoCliente")).Text.Contains(apellidoCliente);
            result = result && _driver.FindElement(By.Id("direccionEnvio")).Text.Contains(direccionEnvio);
            result = result && _driver.FindElement(By.Id("fechaAlquiler")).Text.Contains(fechaAlquiler);
            result = result && _driver.FindElement(By.Id("periodo")).Text.Contains(periodo);
            result = result && _driver.FindElement(By.Id("itemsAlquiler")).Text.Contains(itemsAlquiler);
            return result;
        }


        public bool CheckListaHerramientas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tablaHerramientasCompradas);
        }
    }
}

