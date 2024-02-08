using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RutManager
{
    /// <summary>
    /// Provee la base para validar ruts usando expresiones regulares   
    /// </summary>    
    internal partial class RutFormatValidator
    {        

        #region Expresiones regulares

        /// <summary>
        /// RegEx para validar xx.xxx.xxx-v, puntos obligatorios
        /// </summary>
        public readonly Regex modulo11ConSepRegex = RutConSepGenRegex();

        /// <summary>
        /// RegEx precompilado
        /// </summary>        
        [GeneratedRegex(@"^(?<grps>[1-9][0-9]{0,8})-(?<mod11>[0-9,k,K])$")]
        private static partial Regex RutSinSepRegex();


        /// <summary>
        /// RegEx para validar xxxxxxxx-v, puntos no permitidos
        /// </summary>
        public readonly Regex modulo11SinSepRegex = RutSinSepRegex();

        /// <summary>
        /// RegEx precompilado
        /// </summary>
        [GeneratedRegex(@"^(?<num>(?<grps>[1-9][0-9]{0,2})(\.(?<grps>[0-9]{3})){1,2})-(?<mod11>[0-9,k,K])$")]
        private static partial Regex RutConSepGenRegex();

        #endregion


        /// <summary>
        /// Valida el formato de un rut con o sin puntos
        /// </summary>
        /// <remarks>Si incluye separadores DEBE usarlos como grupos de miles válidos</remarks>
        /// <param name="input">Rut</param>
        /// <returns>True si es válido</returns>
        public virtual bool Validate(string input)
        {
            // Elegir la exp reg basado en si tiene o no separador de miles
            Match match = input.Contains('.') ? modulo11ConSepRegex.Match(input) : modulo11SinSepRegex.Match(input);

            return match.Success;
        }


        /// <summary>
        /// Valida el formato de un rut forzando el uso o no uso de puntos
        /// </summary>
        /// <remarks>Si incluye separadores DEBE usarlos como grupos de miles válidos</remarks>
        /// <param name="input"></param>
        /// <param name="requireGroupSeparator">Si debe o no usar separador de grupos</param>
        /// <returns>True si es válido</returns>
        public virtual bool Validate(string input, bool requireGroupSeparator)
        {
            // Elegir la exp reg basado en si tiene o no separador de miles
            Match match = requireGroupSeparator ? modulo11ConSepRegex.Match(input) : modulo11SinSepRegex.Match(input);

            return match.Success;
        }

        
    }
}
