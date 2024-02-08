namespace TestRutManager
{

    [TestFixture(Author = "Jorge Rojas", Category = "RutManager", TestName = "Ruts: Casos de uso")]
    public class RutManagerUnitTests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void TestBorderCondition_MinFail_Negative()
        {
            var rm = new RutManager.RutManager();

            Assert.That(() =>
            {
                var ver = rm.GeneraRut(-2);
            }, Throws.TypeOf<RutOutOfRangeException>().And.Message.Contains("within"));
        }



        [Test]
        public void TestBorderCondition_MinPass_Zero()
        {
            var rm = new RutManager.RutManager();

            var ver = rm.GeneraRut(0);
            Assert.That<string>(ver, Is.EqualTo("0-0"));
        }


        [Test]
        public void TestBorderCondition_MinPass_One()
        {
            var rm = new RutManager.RutManager();

            var ver = rm.GeneraRut(1);

            Assert.That<string>(ver, Is.EqualTo("1-9"));
        }


        [Test]
        public void TestBorderCondition_MinPass_One_Separator()
        {
            var rm = new RutManager.RutManager();

            var ver = rm.GeneraRut(1, conSeparadores:true);

            Assert.That<string>(ver, Is.EqualTo("1-9"));
        }


        [Test]
        public void TestRecord_Get()
        {
            var rm = new RutManager.RutManager();

            var ver = rm.GeneraRutRecord(1, conSeparadores: true);

            Assert.That<string>(ver.ToString(), Is.EqualTo("1-9"));
        }

        /// <summary> Valida UN rut real
        /// </summary>
        /// <remarks>
        /// Caso típico de prepara-ejecuta-valida
        /// </remarks>
        [Test(Description = "Valida un caso real con dv numérico")]
        public void TestCasoReal_ConOSinPuntos()
        {
            // Prepara
            var rm = new RutManager.RutManager();
            var sRut = "76113195-8";

            // Ejecuta
            var result = rm.ValidaRut(sRut, RutManager.TipoValidacionSeparadorEnum.ConOSinPuntos);

            // Compara
            Assert.That(result, Is.True);
        }



        /// <summary> Valida UN rut real
        /// </summary>
        /// <remarks>
        /// Caso típico de prepara-ejecuta-valida
        /// </remarks>
        [Test(Description = "Valida un caso real con dv numérico")]
        public void TestCasoReal_RequerirPuntos()
        {
            // Prepara
            var rm = new RutManager.RutManager();
            var sRut = "76.113.195-8";

            // Ejecuta
            var result = rm.ValidaRut(sRut, RutManager.TipoValidacionSeparadorEnum.RequerirPuntos);

            // Compara
            Assert.That(result, Is.True);
        }



        /// <summary> Valida UN rut real
        /// </summary>
        /// <remarks>
        /// Caso típico de prepara-ejecuta-valida
        /// </remarks>
        [Test(Description = "Valida un caso real con dv numérico")]
        public void TestCasoReal_DenegarPuntos()
        {
            // Prepara
            var rm = new RutManager.RutManager();
            var sRut = "76113195-8";

            // Ejecuta
            var result = rm.ValidaRut(sRut, RutManager.TipoValidacionSeparadorEnum.DenegarPuntos);

            // Compara
            Assert.That(result, Is.True);
        }




        /// <summary> Valida UN rut real
        /// </summary>
        /// <remarks>
        /// Caso típico de prepara-ejecuta-valida
        /// </remarks>
        [Test(Description = "Valida un caso real con dv numérico")]
        public void TestCasoReal_RequerirPuntos_fail()
        {
            // Prepara
            var rm = new RutManager.RutManager();
            var sRut = "76113195-8";

            // Ejecuta
            var result = rm.ValidaRut(sRut, RutManager.TipoValidacionSeparadorEnum.RequerirPuntos);

            // Compara
            Assert.That(result, Is.False);
        }



        /// <summary> Valida UN rut real
        /// </summary>
        /// <remarks>
        /// Caso típico de prepara-ejecuta-valida
        /// </remarks>
        [Test(Description = "Valida un caso real con dv numérico")]
        public void TestCasoReal_DenegarPuntos_fail()
        {
            // Prepara
            var rm = new RutManager.RutManager();
            var sRut = "76.113.195-8";

            // Ejecuta
            var result = rm.ValidaRut(sRut, RutManager.TipoValidacionSeparadorEnum.DenegarPuntos);

            // Compara
            Assert.That(result, Is.False);
        }


        /// <summary> Varias pruebas con data por atributo TestCase
        /// </summary>
        /// <param name="input">Primer parámetro, sRut</param>
        /// <param name="expectedResult">Si es válido o inválido según def de negocios</param>
        [Test(Description = "Valida los valores extremos")]
        [TestCase("0-0", false, Category = "Borde", TestName = "Condiciones de Borde - Inferior")]
        [TestCase("1-9", true, Category = "Borde", TestName = "Condiciones de Borde - Inferior+1")]
        [TestCase("2-7", true, Category = "Borde", TestName = "Condiciones de Borde - Inferior+2")]
        [TestCase("99.999.999-9", true, Category = "Borde", TestName = "Condiciones de Borde - 100K-1")]
        [TestCase("100.000.000-7", true, Category = "Borde", TestName = "Condiciones de Borde - 100K")]
        [TestCase("100.000.001-5", true, Category = "Borde", TestName = "Condiciones de Borde - 100K+1")]
        [TestCase("999.999.999-6", true, Category = "Borde", TestName = "Condiciones de Borde - Superior")]
        [TestCase("1.000.000.000-6", false, Category = "Borde", TestName = "Condiciones de Borde - Superior+1")]
        [TestCase("1.000.000.001-4", false, Category = "Borde", TestName = "Condiciones de Borde - Superior+2")]
        public void ValidaExtremosTest(string input, bool expectedResult)
        {
            var rm = new RutManager.RutManager(1,9);

            bool result = rm.ValidaRut(input, RutManager.TipoValidacionSeparadorEnum.ConOSinPuntos);

            Assert.That(expectedResult, Is.EqualTo(result));
        }



        /// <summary> Valida distintos valores generads masivamente desde una función IEnumerable
        /// </summary>
        /// <param name="toTest">Cargado por cada ítem devuelto por la función del TestCase</param>
        [Test(Description = "Pruebas masivas"), TestCaseSource(nameof(GetValidarFormatosData))]
        public void TestValidarFormatos((string sRut, bool expected) toTest)
        {
            var rm = new RutManager.RutManager(1,9);

            var result = rm.ValidaRut(toTest.sRut, soloFormato:true);

            Assert.That(result, Is.EqualTo(toTest.expected));
        }


        /// <summary>
        /// Listado de datos de validación
        /// </summary>
        /// <remarks>Puede cargarse desde un archuvo modificando Support.cs sin tocar este código</remarks>
        /// <returns>Ienumerable (númeero, módulo)</returns>
        public static IEnumerable<(string, bool)> GetValidarFormatosData()
        {
            return Support.GetValidarFormatosData();
        }

    }
}