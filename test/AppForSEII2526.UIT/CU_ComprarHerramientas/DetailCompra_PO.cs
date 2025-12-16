using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_OfertaHerramientas
{
    public class DetailCompra_PO : PageObject
    {
        By tablaHerramientasCompradas = By.Id("HerramientasCompradas");

        public DetailCompra_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public bool CheckCompraDetail(string nombreCliente, string apellidoCliente, string direccionEnvio, string fechaCompra, string itemsCompra)
        {
            WaitForBeingVisible(tablaHerramientasCompradas);
            bool result = true;
            result = result && _driver.FindElement(By.Id("NombreCliente")).Text.Contains(nombreCliente);
            result = result && _driver.FindElement(By.Id("ApellidoCliente")).Text.Contains(apellidoCliente);
            result = result && _driver.FindElement(By.Id("DireccionEnvio")).Text.Contains(direccionEnvio);  
            result = result && _driver.FindElement(By.Id("FechaCompra")).Text.Contains(fechaCompra);
            result = result && _driver.FindElement(By.Id("ItemsCompra")).Text.Contains(itemsCompra);
            return result;
        }


        public bool CheckListaHerramientas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tablaHerramientasCompradas);
        }
    }
}