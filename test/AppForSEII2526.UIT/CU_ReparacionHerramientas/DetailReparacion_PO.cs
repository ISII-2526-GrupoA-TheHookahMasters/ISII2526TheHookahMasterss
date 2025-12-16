using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_RepararHerramientas
{
    public class DetailReparacion_PO : PageObject
    {
        By tablaHerramientasReparadas = By.Id("HerramientasReparadas");

        public DetailReparacion_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public bool CheckReparacionDetail( string nombreCliente, string apellidosCliente, string fechaEntrega, string fechaRecogida, string precioFinal, string itemsCount)
        {
            WaitForBeingVisible(tablaHerramientasReparadas);
            bool result = true;
            result = result && _driver.FindElement(By.Id("NombreCliente")).Text.Contains(nombreCliente);
            result = result && _driver.FindElement(By.Id("ApellidosCliente")).Text.Contains(apellidosCliente);
            result = result && _driver.FindElement(By.Id("FechaEntrega")).Text.Contains(fechaEntrega);
            result = result && _driver.FindElement(By.Id("FechaRecogida")).Text.Contains(fechaRecogida);
            result = result && _driver.FindElement(By.Id("PrecioFinal")).Text.Contains(precioFinal);
            result = result && _driver.FindElement(By.Id("ItemsCount")).Text.Contains(itemsCount);
            return result;
        }


        public bool CheckListaHerramientas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tablaHerramientasReparadas);
        }
    }
}
