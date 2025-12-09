using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.CU_OfertaHerramientas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_OfertaHerramientas_UIT
{
    public class CU_OfertaHerramientas_UIT : UC_UIT
    {
        private SelectHerramientasForOferta_PO selectHerramientasForOferta_PO;
        private const string herramientaId1 = "1";
        private const string herramientaNombre1 = "Taladro percutor Bosch GSB 13 RE";
        private const string herramientaMaterial1 = "Acero y plástico";
        private const string herramientaPrecio1 = "89,99";
        private const string herramientaFabricante1 = "Bosch";
        private const string herramientaId2 = "2";
        private const string herramientaNombre2 = "Sierra circular Makita HS7601";
        private const string herramientaMaterial2 = "Aluminio y acero";
        private const string herramientaPrecio2 = "120,5";
        private const string herramientaFabricante2 = "Makita";

        public CU_OfertaHerramientas_UIT(ITestOutputHelper output) : base(output)
        {
            selectHerramientasForOferta_PO = new SelectHerramientasForOferta_PO(_driver, _output);
        }

        /*private void Precondition_perform_login()
        {
            Perform_login("elena@uclm.es", "Password1234%");
        }*/

        private void InitialStepsForOfertaHerramientas()
        {
            Initial_step_opening_the_web_page();

            //Precondition_perform_login();
            By id = By.Id("CreateOferta");
            selectHerramientasForOferta_PO.WaitForBeingVisible(id);

            Thread.Sleep(500);

            _driver.FindElement(id).Click();
        }

        [Theory]
        [InlineData(herramientaId1, herramientaNombre1, herramientaMaterial1, herramientaPrecio1, herramientaFabricante1, "Bosch", "")]
        [InlineData(herramientaId2, herramientaNombre2, herramientaMaterial2, herramientaPrecio2, herramientaFabricante2, "", "120,5")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_2_3_AF0_filteringPorPrecioFabricante(string herramientaId, string herramientaNombre, string herramientaMaterial, string herramientaPrecio, string herramientaFabricante,
            string filtroFabricante, string filtroPrecio)
        {
            //Arrange
            InitialStepsForOfertaHerramientas();
            var expectedHerramientas = new List<string[]> { new string[] { herramientaId, herramientaNombre, herramientaMaterial, herramientaPrecio, herramientaFabricante }, };

            //Act
            selectHerramientasForOferta_PO.SearchHerramientas(filtroFabricante, filtroPrecio);

            Thread.Sleep(500);

            //Assert
            Assert.True(selectHerramientasForOferta_PO.CheckListOfHerramientas(expectedHerramientas));
        }
    }
}
