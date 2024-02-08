using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRutManager
{
    internal static class Support
    {
        /// <summary> Genera listado de datos junto con el resultado esperado (si son válidos)
        /// </summary>
        /// <returns>IEnumerable(string, bool) (Listado de valores)</returns>
        public static IEnumerable<(string, bool)> GetValidarFormatosData()
        {
            List<(string, bool)> ejemplos = new()
            {
                ("1-0", true),
                ("1-9", true),
                ("1-k", true),
                ("1-K", true),
                ("21-K", true),
                ("321-K", true),

                ("4321-K", true),
                ("4.321-K", true),
                ("4321-4", true),
                ("4.321-4", true),
                ("43.21-K", false),
                ("432.1-K", false),
                ("4.32.1-K", false),
                ("4.3.21-K", false),
                ("4.3.2.1-K", false),

                ("54321-K", true),
                ("54.321-K", true),
                ("54321-5", true),
                ("54.321-5", true),
                ("5432.1-K", false),
                ("5.4321-K", false),

                ("654321-K", true),
                ("654.321-K", true),
                ("654321-6", true),
                ("654.321-6", true),
                ("65432.1-K", false),
                ("65.4321-K", false),
                ("6.54321-K", false),

                ("7654321-K", true),
                ("7.654.321-K", true),
                ("7654321-7", true),
                ("7.654.321-7", true),
                ("7.654321-K", false),
                ("7654.321-K", false),
                ("7.6543.21-K", false),

                ("87654321-K", true),
                ("87.654.321-K", true),
                ("87654321-8", true),
                ("87.654.321-8", true),
                ("87.6543.21-K", false),
                ("8.7.654.321-K", false),
                ("8.765.4321-K", false),
                ("8.765.432.1-K", false),

                ("987654321-K", true),
                ("987.654.321-K", true),
                ("987654321-9", true),
                ("987.654.321-9", true),
                ("987654321-9", true),
                ("987.654.321-9", true),
                ("987.654321-K", false),
                ("987654.321-K", false),

                ("1987654321-K", false),
                ("1.987.654.321-K", false),
            };

            return ejemplos;
        }


        /// <summary>
        /// Retorna una lista válida de valores de módulo 11
        /// </summary>
        /// <returns>IEnumerable de (long número,string módulo) </returns>
        internal static IEnumerable<(long, string)> GetTestNumbersAndModulus()
        {
            List<(long, string)> ejemplos = new()
            {
                (7,"8"),
                (8,"6"),
                (1,"9"),
                (3,"5"),
                (2,"7"),
                (9,"4"),
                (4,"3"),
                (46,"9"),
                (28,"0"),
                (86,"8"),
                (57,"4"),
                (73,"6"),
                (68,"k"),
                (24,"8"),
                (80,"9"),
                (653,"k"),
                (851,"6"),
                (298,"4"),
                (635,"1"),
                (540,"1"),
                (235,"6"),
                (722,"6"),
                (867,"2"),
                (148,"1"),
                (612,"2"),
                (3880,"6"),
                (3329,"4"),
                (4614,"0"),
                (3809,"1"),
                (7989,"8"),
                (7291,"5"),
                (2712,"k"),
                (2683,"2"),
                (8440,"9"),
                (8346,"1"),
                (80221,"2"),
                (26904,"2"),
                (59361,"3"),
                (12794,"9"),
                (60546,"8"),
                (24615,"8"),
                (12749,"3"),
                (92124,"6"),
                (34677,"2"),
                (90423,"6"),
                (161132,"1"),
                (527947,"k"),
                (567240,"6"),
                (165694,"5"),
                (727123,"9"),
                (920090,"8"),
                (955951,"5"),
                (146008,"0"),
                (353963,"6"),
                (774030,"1"),
                (5667439,"k"),
                (6584122,"3"),
                (9260461,"6"),
                (3293091,"3"),
                (7163809,"k"),
                (8214138,"3"),
                (6884990,"k"),
                (2244291,"0"),
                (1175038,"9"),
                (3540510,"0"),
                (50107483,"7"),
                (49705169,"k"),
                (50973099,"7"),
                (50382974,"6"),
                (72932294,"6"),
                (45620592,"5"),
                (85040294,"9"),
                (99621535,"0"),
                (72120008,"6"),
                (20179820,"5"),
                (713117079,"8"),
                (724860514,"0"),
                (390711252,"6"),
                (755375286,"2"),
                (244979481,"0"),
                (874101125,"6"),
                (757655158,"9"),
                (972676492,"9"),
                (150997994,"0"),
                (210864551,"5"),
                (7320461975,"9"),
                (6037484409,"4"),
                (7347409925,"0"),
                (4591913935,"1"),
                (6334386645,"5"),
                (5520216030,"0"),
                (2952146309,"9"),
                (3341991484,"9"),
                (6920344289,"0"),
                (1557226771,"4")
            };

            return ejemplos;
        }

        static void Genera()
        {
            var m11 = new Modulo11.Modulus11("k");
            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                long aBase = (long)Math.Pow(10, i);

                for (int j = 0; j < 10; j++)
                {
                    long inRange = rnd.NextInt64(aBase, aBase * 10);
                    string aChar = m11.GetModulus(inRange);
                    TestContext.WriteLine($"({inRange},\"{aChar}\"),");
                }
            }
        }
    }
}
