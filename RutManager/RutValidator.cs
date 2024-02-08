using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RutManager
{
    /// <summary>
    /// Provee la base para validar ruts
    /// </summary>
    internal class RutValidator : RutFormatValidator
    {
        private readonly Modulo11.Modulus11 m11;

        public long minLength { get; }
        public long maxLength { get; }


        public RutValidator(Modulo11.Modulus11 modulusRef, long minLength, long maxLength) 
        {
            m11 = modulusRef;

            this.minLength = minLength;            
            this.maxLength = maxLength;
        }


        /// <summary>
        /// Valida el formato de un rut con o sin puntos
        /// </summary>
        /// <remarks>Si incluye separadores DEBE usarlos como grupos de miles válidos</remarks>
        /// <param name="input">Rut</param>
        /// <returns>True si es válido</returns>
        public override bool Validate(string input)
        {
            if (String.IsNullOrWhiteSpace(input)) return false;

            // Elegir la exp reg basado en si tiene o no separador de miles
            Match match = input.Contains('.') ? modulo11ConSepRegex.Match(input) : modulo11SinSepRegex.Match(input);
            

            if (!match.Success) return false;

            string numGroup = match.Groups["mod11"].Value.ToLower();

            CaptureCollection grpsCaptures = match.Groups["grps"].Captures;

            long toNum = 0;
            foreach (var value in grpsCaptures)
            {
                toNum = toNum * 1000 + int.Parse(value.ToString());
            }

            

            string generated = m11.GetModulus(toNum);

            // Verificar si el dígito obtenido coincide con el último dígito del número
            return numGroup == generated;
        }



        /// <summary>
        /// Valida un rut forzando el uso o no uso de puntos
        /// </summary>
        /// <remarks>Si incluye separadores DEBE usarlos como grupos de miles válidos</remarks>
        /// <param name="input"></param>
        /// <param name="requireGroupSeparator">Si debe o no usar separador de grupos</param>
        /// <returns>True si es válido</returns>
        public override bool Validate(string input, bool requireGroupSeparator)
        {
            if (String.IsNullOrWhiteSpace(input)) return false;

            // Elegir la exp reg basado en si tiene o no separador de miles
            Match match = requireGroupSeparator ? modulo11ConSepRegex.Match(input) : modulo11SinSepRegex.Match(input);


            if (!match.Success) return false;

            string numGroup = match.Groups["mod11"].Value.ToLower();

            CaptureCollection grpsCaptures = match.Groups["grps"].Captures;

            long toNum = 0;
            foreach (var value in grpsCaptures)
            {
                toNum = toNum * 1000 + int.Parse(value.ToString());
            }

            if (toNum<minLength || toNum>maxLength) return false;

            string generated = m11.GetModulus(toNum);

            // Verificar si el dígito obtenido coincide con el último dígito del número
            return numGroup == generated;
        }
    }
}
