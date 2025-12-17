using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.CU_AlquilarHerramientas;
using AppForSEII2526.UIT.CU_CompraHerramientas;
using AppForSEII2526.UIT.CU_OfertaHerramientas;
using Microsoft.VisualStudio.TestPlatform.Utilities;

public class CUAlquilarHerramientas_UIT : UC_UIT
{
    private CrearAlquiler_PO crearAlquiler_PO;
    private SelectHerramientasForAlquiler_PO selectHerramientasForAqluilerPO;
    private DetailAlquiler_PO detailAlquiler_PO;
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

    public CUAlquilarHerramientas_UIT(ITestOutputHelper output) : base(output)
    {
        selectHerramientasForAqluilerPO = new SelectHerramientasForAlquiler_PO(_driver, _output);
        crearAlquiler_PO = new CrearAlquiler_PO(_driver, _output);
        detailAlquiler_PO = new DetailAlquiler_PO(_driver, _output);
    }

    /*private void Precondition_perform_login()
    {
        Perform_login("elena@uclm.es", "Password1234%");
    }*/

    private void InitialStepsForAlquilarHerramientas()
    {
        Initial_step_opening_the_web_page();

        //Precondition_perform_login();
        By id = By.Id("CreateAlquiler");
        selectHerramientasForAqluilerPO.WaitForBeingVisible(id);

        Thread.Sleep(500);

        _driver.FindElement(id).Click();
    }

    [Theory]
    [InlineData(herramientaId1, herramientaNombre1, herramientaMaterial1, herramientaPrecio1, herramientaFabricante1, "Taladro percutor Bosch GSB 13 RE", "")]
    [InlineData(herramientaId2, herramientaNombre2, herramientaMaterial2, herramientaPrecio2, herramientaFabricante2, "", "Aluminio y acero")]
    [InlineData(herramientaId1, herramientaNombre1, herramientaMaterial1, herramientaPrecio1, herramientaFabricante1, "Taladro percutor Bosch GSB 13 RE", "Acero y plástico")]
    [Trait("LevelTesting", "Funcional Testing")]
    public void UC4_2_3_AF0_filteringPorNombreMaterial(string herramientaId, string herramientaNombre, string herramientaMaterial, string herramientaPrecio, string herramientaFabricante,
        string filtroNombre, string filtroMaterial)
    {
        //Arrange
        InitialStepsForAlquilarHerramientas();
        var expectedHerramientas = new List<string[]> { new string[] { herramientaId, herramientaNombre, herramientaMaterial, herramientaPrecio, herramientaFabricante }, };

        //Act
        selectHerramientasForAqluilerPO.SearchHerramientas(filtroNombre, filtroMaterial);

        Thread.Sleep(500);

        //Assert
        Assert.True(selectHerramientasForAqluilerPO.CheckListOfHerramientas(expectedHerramientas));
    }

    [Theory]
    [InlineData(herramientaId1, herramientaNombre1)]
    [Trait("LevelTesting", "Funcional Testing")]
    public void UC4_AF0_CrearAlquiler(string herramientaId, string herramientaNombre)
    {
        InitialStepsForAlquilarHerramientas();
        Thread.Sleep(500);
        selectHerramientasForAqluilerPO.AddHerramientaToAlquilerCart(herramientaNombre);
        Thread.Sleep(500);
        selectHerramientasForAqluilerPO.BotonAlquilerHerramienta();
        Thread.Sleep(1000);
        crearAlquiler_PO.addAtributosAlquiler(herramientaId,"Carlos", "Gomez","Avd España 4" , "22/12/2025", "31/12/2025", "PayPal","10");
        Thread.Sleep(1000);
        crearAlquiler_PO.pulsarCrearAlquiler();
        Thread.Sleep(2000);
        crearAlquiler_PO.guardarAlquilerDialog();
        Thread.Sleep(1000);
    }


    [Theory]
    [InlineData(herramientaId1, herramientaNombre1, "26/12/2025", "14/12/2025", "Error! Tu alquiler debe terminar despues de que empiece")]
    [InlineData(herramientaId1, herramientaNombre1, "09/12/2025", "14/12/2025", "Error! La fecha de inicio de tu alquiler debe ser posterior a hoy")]
    [Trait("LevelTesting", "Funcional Testing")]
    public void UC4_5_6_7_AF1_FechasErroneas(string herramientaId, string herramientaNombre, string fechaInicio, string fechaFinal, string expectedError)
    {
        InitialStepsForAlquilarHerramientas();
        Thread.Sleep(2000);
        selectHerramientasForAqluilerPO.AddHerramientaToAlquilerCart(herramientaNombre);
        Thread.Sleep(2000);
        selectHerramientasForAqluilerPO.BotonAlquilerHerramienta();
        Thread.Sleep(2000);
        crearAlquiler_PO.addAtributosAlquiler(herramientaId,"Carlos", "Gomez", "Avd España", fechaInicio, fechaFinal, "Efectivo", "10");
        Thread.Sleep(2000);
        crearAlquiler_PO.pulsarCrearAlquiler();
        Thread.Sleep(2000);
        crearAlquiler_PO.guardarAlquilerDialog();
        Thread.Sleep(1000);


        Assert.True(crearAlquiler_PO.CheckValidationError(expectedError), $"Expected error: {expectedError}");
    }


    [Fact]
    [Trait("LevelTesting", "Funcional Testing")]
    public void UC4_AF2_ModificarCarrito()
    {
        InitialStepsForAlquilarHerramientas();
        selectHerramientasForAqluilerPO.AddHerramientaToAlquilerCart(herramientaNombre1);
        selectHerramientasForAqluilerPO.AddHerramientaToAlquilerCart(herramientaNombre2);
        Thread.Sleep(1000);
        selectHerramientasForAqluilerPO.BotonAlquilerHerramienta();
        Thread.Sleep(500);
        crearAlquiler_PO.modificarHerramientas();
        Thread.Sleep(500);
        selectHerramientasForAqluilerPO.RemoveHerramientaFromAlquilerCart(herramientaNombre1);
        Thread.Sleep(500);
        selectHerramientasForAqluilerPO.BotonAlquilerHerramienta();
        Thread.Sleep(500);
        var expectedOfertaItems = new List<string[]> { new string[] { herramientaNombre2, herramientaMaterial2, herramientaPrecio2 }, };

        Assert.True(crearAlquiler_PO.CheckListOfAlquilerItems(expectedOfertaItems));
    }

    [Theory]

    [InlineData(herramientaId1, herramientaNombre1, "", "Gomez", "Avd España","16/12/2025", "26/12/2025","10", "The NombreCliente field is required.")]
    [InlineData(herramientaId1, herramientaNombre1, "Carlos", "", "Avd España", "16/12/2025", "26/12/2025", "10", "The ApellidosCliente field is required.")]
    [InlineData(herramientaId1, herramientaNombre1, "Carlos", "Gomez", "", "16/12/2025", "26/12/2025", "10", "The DireccionEnvio field is required.")]

    [Trait("LevelTesting", "Funcional Testing")]
    public void UC4_11_12_AF5_CamposObligatorios(string herramientaId, string herramientaNombre, string nombre, string apellido, string direccion, string fechaInicio, string fechaFinal,string cantidad, string expectedError)
    {
        InitialStepsForAlquilarHerramientas();
        Thread.Sleep(2000);
        selectHerramientasForAqluilerPO.AddHerramientaToAlquilerCart(herramientaNombre);
        Thread.Sleep(2000);
        selectHerramientasForAqluilerPO.BotonAlquilerHerramienta();
        Thread.Sleep(2000);
        crearAlquiler_PO.addAtributosAlquiler(herramientaId, nombre, apellido, direccion, fechaInicio, fechaFinal, "Efectivo", cantidad);
        Thread.Sleep(2000);
        crearAlquiler_PO.pulsarCrearAlquiler();
        Thread.Sleep(2000);
        /*
        crearAlquiler_PO.guardarAlquilerDialog();
        Thread.Sleep(5000);
        */
        Assert.True(crearAlquiler_PO.CheckValidationError(expectedError), $"Expected error: {expectedError}");
    }

    [Fact]
    [Trait("LevelTesting", "Funcional Testing")]
    public void UC1_AF3_noHerramientasEnCarrito()
    {
        // Arrange
        InitialStepsForAlquilarHerramientas();

        Thread.Sleep(1000);

        // Assert: el botón no está activo (no visible o no existe)
        Assert.False(selectHerramientasForAqluilerPO.IsCrearAlquilerCarritoButtonActive());
    }


    // Si el sistema detecta que la cantidad que el usuario desea comprar de cualquier herramienta es 0, la opción de continuar el proceso no estará activa.
    [Theory]
    [InlineData(herramientaId1, herramientaNombre1)]
    [Trait("LevelTesting", "Funcional Testing")]
    public void UC1_AF5_cantidadCero(string herramientaId, string herramientaNombre)
    {
        //Arrange
        InitialStepsForAlquilarHerramientas();
        Thread.Sleep(500);
        selectHerramientasForAqluilerPO.AddHerramientaToAlquilerCart(herramientaNombre);
        Thread.Sleep(500);
        selectHerramientasForAqluilerPO.BotonAlquilerHerramienta();
        Thread.Sleep(1000);
        crearAlquiler_PO.addAtributosAlquiler(herramientaId, "Carlos", "Gomez", "Avenida de España, 12", "25/12/2025","30/12/2025","Efectivo", "0");
        Thread.Sleep(1000);

        //Assert
        Assert.False(crearAlquiler_PO.IsSubmitEnabled());

    }


    [Theory]
    
    [InlineData(herramientaId2, herramientaNombre2, herramientaMaterial2, herramientaPrecio2, herramientaFabricante2, "", "Aluminio y acero")]
    [Trait("LevelTesting", "Funcional Testing")]
    public void Examen(string herramientaId, string herramientaNombre, string herramientaMaterial, string herramientaPrecio, string herramientaFabricante,
        string filtroNombre, string filtroMaterial)
    {
        //Arrange
        InitialStepsForAlquilarHerramientas();
        var expectedHerramientas = new List<string[]> { new string[] { herramientaId, herramientaNombre, herramientaMaterial, herramientaPrecio, herramientaFabricante }, };

        //Act
        selectHerramientasForAqluilerPO.SearchHerramientas(filtroNombre, "");
        Thread.Sleep(500);
        selectHerramientasForAqluilerPO.AddHerramientaToAlquilerCart(herramientaNombre1);
        Thread.Sleep(500);
        selectHerramientasForAqluilerPO.SearchHerramientas("", filtroMaterial);
        Thread.Sleep(500);
        selectHerramientasForAqluilerPO.AddHerramientaToAlquilerCart(herramientaNombre2);
        Thread.Sleep(1000);
        
        Thread.Sleep(1000);
        selectHerramientasForAqluilerPO.BotonAlquilerHerramienta();
        Thread.Sleep(500);
        crearAlquiler_PO.modificarHerramientas();
        Thread.Sleep(500);
        selectHerramientasForAqluilerPO.RemoveHerramientaFromAlquilerCart(herramientaNombre1);
        Thread.Sleep(500);
        
        Thread.Sleep(500);
        selectHerramientasForAqluilerPO.BotonAlquilerHerramienta();
        Thread.Sleep(500);
        var expectedAlquilerItems = new List<string[]> { new string[] { herramientaNombre2, herramientaMaterial2, herramientaPrecio2 }, };

        crearAlquiler_PO.addAtributosAlquiler(herramientaId, "Carlos", "Gomez", "Avd España 4", "22/12/2025", "31/12/2025", "Efectivo", "10");
        Thread.Sleep(1000);

        crearAlquiler_PO.pulsarCrearAlquiler();
        Thread.Sleep(2000);

        crearAlquiler_PO.guardarAlquilerDialog();
        Thread.Sleep(10000);
        
    }

}