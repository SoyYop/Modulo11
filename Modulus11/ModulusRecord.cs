using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Modulo11
{

    /// <summary>
    /// Record of a modulus11
    /// </summary>
    /// <remarks>
    /// In short, a way to store and format a number and its modulus with a ToString() override
    /// No 
    /// </remarks>    
    /// <param name="Number">The number</param>
    /// <param name="Digit">The already calculated digit</param>
    /// <param name="Hyphen">Number and digit separator</param>
    /// <param name="UseThousandsSeparator">If to use thousands separator</param>
    public record ModulusRecord(long Number, string Digit, string Hyphen = "-", string NumberGroupSeparator = "")
    {

        /// This region holds static parameters, not included with instances of thew class
        /// Only used for initialization to be instances culture-aware
        #region Statics       

        /// <summary>
        /// Used to format the modulus, static to avoid bloatware        
        /// </summary>
        private static Dictionary<string, NumberFormatInfo> formats = new() { { CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator, CultureInfo.CurrentCulture.NumberFormat } };


        /// <summary>
        /// Returns formatting info stored within a dictionary cache
        /// </summary>
        /// <param name="ts">Group digit separator</param>
        /// <returns>Formatting info to be used with the ToString() override</returns>
        private static NumberFormatInfo GetNumberFormatInfo(string ts)
        {
            if (!formats.ContainsKey(ts))
                formats[ts] = new NumberFormatInfo() { NumberGroupSeparator = ts };
            return formats[ts];
        }

        #endregion


        /// <summary>
        /// In format Number-dv like 12.345.678-9
        /// </summary>
        /// <returns>12.345.678-9</returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(NumberGroupSeparator))
                return $"{Number}{Hyphen}{Digit}";
            else
                return $"{Number.ToString("N0", GetNumberFormatInfo(NumberGroupSeparator))}{Hyphen}{Digit}";

        }

    }

}