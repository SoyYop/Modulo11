using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Modulo11
{

    /// <summary>
    /// If the number is negative
    /// </summary>
    public class ModulusNegativeException : System.ArgumentOutOfRangeException
    {
        public ModulusNegativeException() { }
        public ModulusNegativeException(string message = "Number cannot be negative") : base(message) { }

        public ModulusNegativeException(long number, [CallerArgumentExpression(nameof(number))] string? paramName = null)
            : base($"Number {paramName} cannot be negative: {number}") { }

        public ModulusNegativeException(string message, System.Exception inner) : base(message, inner) { }

        public ModulusNegativeException(long number, System.Exception inner, [CallerArgumentExpression(nameof(number))] string? paramName = null) 
            : base($"Number {paramName} cannot be negative: {number}", inner) { }

    }



}