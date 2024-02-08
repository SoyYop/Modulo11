using System.Reflection;
using Modulo11;

namespace RutManager
{
    /// <summary>
    /// Encapsula las operaciones necesarias para validar y generar ruts
    /// </summary>
    public class RutManager
    {

        /// <summary>
        /// En caché para procesar los cálculos
        /// </summary>
        internal Modulo11.Modulus11 m11 = new("k");
        // private double maxLength;

        /// <summary>
        /// Dígito verificador, usualmente 'k'
        /// </summary>
        public char KValue { get { return m11.CharFor11Value[0]; } }

        /// <summary>
        /// Mínimo de dígitos
        /// </summary>        
        private readonly long minValue;

        /// <summary>
        /// Máximo de dígitos
        /// </summary>
        private readonly long maxValue;



        /// <summary>
        /// Inicializa el administrador
        /// </summary>
        /// <param name="minDigits">1 a maxDigits</param>
        /// <param name="maxDigits">Desde minDigits hasta 12</param>
        /// <param name="charForKValue">Por omisión, "k" minúscula</param>
        public RutManager(byte minDigits = 1, byte maxDigits = 8, char charForKValue = 'k')
        {
            this.minValue = (long)Math.Pow(10, minDigits - 1) - 1;
            this.maxValue = (long)Math.Pow(10, maxDigits) - 1;

            m11 = new(charForKValue.ToString());
        }



        /// <summary>
        /// Valida un rut basado en los parámetros definidos
        /// </summary>
        /// <param name="rut"></param>
        /// <param name="tipo"></param>
        /// <param name="soloFormato">Sólo valida formato, no el dv ni los dígitos</param>
        /// <returns></returns>
        public bool ValidaRut(string rut, TipoValidacionSeparadorEnum tipo = TipoValidacionSeparadorEnum.ConOSinPuntos, bool soloFormato = false)
        {
            RutFormatValidator validator = soloFormato ? new RutFormatValidator() : new RutValidator(m11, minValue, maxValue);

            bool result = false;

            switch (tipo)
            {
                case TipoValidacionSeparadorEnum.ConOSinPuntos: result = validator.Validate(rut); break;
                case TipoValidacionSeparadorEnum.RequerirPuntos: result = validator.Validate(rut, true); break;
                case TipoValidacionSeparadorEnum.DenegarPuntos: result = validator.Validate(rut, false); break;
            }

            return result;
        }



        /// <summary>
        /// Genera un record con la información del rut a partir del número
        /// </summary>
        /// <remarks>
        /// El objeto record retornado incluye el formato numérico definido (conSeparadores) y separador con guión
        /// </remarks>
        /// <param name="input">Número</param>
        /// <param name="conSeparadores">Puntos o no puntos</param>
        /// <returns>RutRecord con un rut válido</returns>
        /// <exception cref="RutOutOfRangeException"></exception>
        public ModulusRecord GeneraRutRecord(long input, bool conSeparadores = false)
        {
            if (input < minValue || input > maxValue)
                throw new RutOutOfRangeException(minValue, maxValue, input);

            ModulusRecord rut = m11.GetModulusRecord(input, NumberGroupSeparator: conSeparadores ? "." : string.Empty);

            return rut;
        }



        /// <summary>
        /// Genera un rut con formato
        /// </summary>
        /// <param name="rut"></param>
        /// <param name="conSeparadores">Puntos o no puntos</param>
        /// <returns></returns>
        public string GeneraRut(ModulusRecord rut, bool conSeparadores = false)
        {
            return GeneraRut(rut.Number, conSeparadores);
        }



        /// <summary>
        /// Genera un rut con formato
        /// </summary>
        /// <param name="input"></param>
        /// <param name="conSeparadores">Puntos o no puntos</param>
        /// <returns></returns>
        /// <exception cref="RutOutOfRangeException"></exception>
        public string GeneraRut(long input, bool conSeparadores = false)
        {
            if (input < minValue || input > maxValue)
                throw new RutOutOfRangeException(minValue, maxValue, input);

            ModulusRecord rut = m11.GetModulusRecord(input, NumberGroupSeparator: conSeparadores ? "." : string.Empty);

            return rut.ToString();
        }

    }
}
