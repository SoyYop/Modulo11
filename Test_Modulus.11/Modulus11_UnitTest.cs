using Microsoft.VisualStudio.TestPlatform.ObjectModel.InProcDataCollector;

using Modulo11;

using NUnit.Framework;


namespace TestModulus11
{
    [TestFixture(Author = "Jorge Rojas", Category = "Modulus11", TestName = "Módulo 11: Casos de uso")]
    public class Modulus11_UnitTest
    {
        // internal TestContext TestContext { get; set; }

        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void TestBorderCondition_MinFail_Negative()
        {
            var m11 = new Modulo11.Modulus11("k");

            Assert.That(() =>
            {
                var ver = m11.GetModulus(-2);
            }, Throws.TypeOf<ModulusNegativeException>().And.Message.Contains("negative"));
        }



        [Test]
        public void TestBorderCondition_MinPass_Zero()
        {
            var m11 = new Modulo11.Modulus11("k");

            var ver = m11.GetModulus(0);
            Assert.That<string>(ver, Is.EqualTo("0"));


            var rec = m11.GetModulusRecord(0);

            Assert.That<string>(rec.Digit, Is.EqualTo("0"));
        }



        [Test]
        public void TestBorderCondition_ModulusIs_10()
        {
            var m11 = new Modulo11.Modulus11("10");

            var ver = m11.GetModulus(11111191);
            Assert.That<string>(ver, Is.EqualTo("10"));


            var rec = m11.GetModulusRecord(11111191);

            Assert.That<string>(rec.Digit, Is.EqualTo("10"));
        }



        [Test]
        public void TestBorderCondition_ModulusIs_11()
        {
            var m11 = new Modulo11.Modulus11(charFor10Value: "10", charFor11Value: "11");

            var ver = m11.GetModulus(11111151);
            Assert.That<string>(ver, Is.EqualTo("11"));


            var rec = m11.GetModulusRecord(11111151);

            Assert.That<string>(rec.Digit, Is.EqualTo("11"));
        }


       


        /// <summary>
        /// Formatos del record
        /// </summary>
        /// <param name="input">Número</param>
        /// <param name="hyphen">Separador de número/verificador</param>
        /// <param name="groupSeparator">Separador de miles</param>
        /// <param name="expected">Valor esperado</param>
        /// <param name="kChar">Valor a usar cuando modulo11=10</param>
        [TestCase(0, "-", "", "0-0", "k", Category = "Formato", TestName = "Formato: Inferior con -")]
        [TestCase(0, "", "", "00", "k", Category = "Formato", TestName = "Formato: Inferior sin -")]

        [TestCase(4321, "-", "", "4321-4", "k", Category = "Formato", TestName = "Formato: Con - sin .")]
        [TestCase(4321, "-", ".", "4.321-4", "k", Category = "Formato", TestName = "Formato: Con - con .")]
        [TestCase(4321, "", "", "43214", "k", Category = "Formato", TestName = "Formato: Sin - sin .")]
        [TestCase(4321, "", ".", "4.3214", "k", Category = "Formato", TestName = "Formato: Sin - con .")]

        [TestCase(7654333, "-", "", "7654333-k", "k", Category = "Formato", TestName = "Formato: Con guión Sep k=k min")]
        [TestCase(7654333, "-", "", "7654333-K", "K", Category = "Formato", TestName = "Formato: Sep k=K may")]
        [TestCase(7654333, "-", ".", "7.654.333-K", "K", Category = "Formato", TestName = "Formato: Con guión con punto Sep k=K may")]
        [TestCase(7654333, "", "", "7654333k", "k", Category = "Formato", TestName = "Formato: Sin guión sin punto Sep k=K min")]

        [TestCase(7654333, "-", "", "7654333-?", "?", Category = "Formato", TestName = "Formato: Con guión Sep k=?")]
        [TestCase(7654333, "-", ".", "7.654.333-?", "?", Category = "Formato", TestName = "Formato: Con guión con puntos Sep k=?")]

        [TestCase(7654333, "-", "", "7654333-a", "a", Category = "Formato", TestName = "Formato: Con guión Sep k=a")]
        [TestCase(7654333, "-", ".", "7.654.333-a", "a", Category = "Formato", TestName = "Formato: Con guión con puntos Sep k=a")]
        [TestCase(7654333, "-", "", "7654333-A", "A", Category = "Formato", TestName = "Formato: Con guión Sep k=A")]
        [TestCase(7654333, "-", ".", "7.654.333-A", "A", Category = "Formato", TestName = "Formato: Con guión con puntos Sep k=A")]

        [TestCase(7654333, "-", ".", "7.654.333-ONCE", "ONCE", Category = "Formato", TestName = "Formato: Con guión con puntos Sep k=ONCE")]
        public void TestRecordFormatting_UseCases(long input, string hyphen, string groupSeparator, string expected, string kChar)
        {
            var m11 = new Modulo11.Modulus11(kChar);

            var rec = m11.GetModulusRecord(input, hyphen, groupSeparator);

            Assert.That<string>(rec.ToString(), Is.EqualTo(expected));
        }



        /// <summary> Varias pruebas con data por atributo TestCase
        /// </summary>
        /// <param name="input">Primer parámetro, sRut</param>
        /// <param name="expectedResult">Si es válido o inválido según def de negocios</param>
        [Test(Description = "Valida valores importantes")]
        [TestCase("0", "0", Category = "Borde", TestName = "Inferior")]
        [TestCase("1", "9", Category = "Borde", TestName = "Inferior+1")]
        [TestCase("2", "7", Category = "Borde", TestName = "Inferior+2")]
        [TestCase("99.999.999", "9", Category = "Borde", TestName = "Condiciones de Borde - 100K-1")]
        [TestCase("100.000.000", "7", Category = "Borde", TestName = "Condiciones de Borde - 100K")]
        [TestCase("100.000.001", "5", Category = "Borde", TestName = "Condiciones de Borde - 100K+1")]
        [TestCase("999.999.999", "6", Category = "Borde", TestName = "Condiciones de Borde - 1M-1")]
        [TestCase("1.000.000.000", "6", Category = "Borde", TestName = "Condiciones de Borde - 1M")]
        [TestCase("1.000.000.001", "4", Category = "Borde", TestName = "Condiciones de Borde - 1M+1")]
        public void TestDecimalOrders(string input, string expectedResult)
        {
            var m11 = new Modulo11.Modulus11("k");

            var aNumberInt = long.Parse(input.Replace(".", ""));


            var ver = m11.GetModulus(aNumberInt);

            Assert.That<string>(ver, Is.EqualTo(expectedResult));


            var rec = m11.GetModulusRecord(aNumberInt);

            Assert.That<string>(rec.Digit, Is.EqualTo(expectedResult));
            Assert.That<string>(rec.ToString(), Is.EqualTo($"{aNumberInt}-{expectedResult}"));
        }




        /// <summary> Valida distintos valores generads masivamente desde una función IEnumerable
        /// </summary>
        /// <param name="toTest">Cargado por cada ítem devuelto por la función del TestCase</param>
        [Test(Description = "Pruebas masivas"), TestCaseSource(nameof(GetTestNumbersAndModulus))]
        public void TestRandomGeneratedData((long lRut, string expected) toTest)
        {
            var m11 = new Modulo11.Modulus11("k");

            var result = m11.GetModulus(toTest.lRut);

            Assert.That(result, Is.EqualTo(toTest.expected));


            var rec = m11.GetModulusRecord(toTest.lRut);

            Assert.That<string>(rec.Digit, Is.EqualTo(toTest.expected));
        }


        /// <summary>
        /// Listado de datos de validación
        /// </summary>
        /// <remarks>Puede cargarse desde un archuvo modificando Support.cs sin tocar este código</remarks>
        /// <returns>Ienumerable (númeero, módulo)</returns>
        private static IEnumerable<(long, string)> GetTestNumbersAndModulus()
        {
            return Support.GetTestNumbersAndModulus();
        }
    }
}