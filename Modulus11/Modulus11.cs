

namespace Modulo11
{

    /// <summary>
    /// Generates the modulo 11 (checksum digit) of a sequence
    /// </summary>
    /// <remarks>Accepts values between 1 and max, tested up to 9 digits
    /// Returns the modulus character, using digits 0-9 plus the '11' character
    /// </remarks>
    public class Modulus11
    {

        /// <summary>
        /// Digit or character to use when the checksum is 11
        /// </summary>
        public string CharFor11Value { get; set; }



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="charFor11Value">Character to use for value 11</param>
        public Modulus11(string charFor11Value)
        {
            this.CharFor11Value = charFor11Value;
        }



        /// <summary>
        /// Returns a modulus record with the number, digit, and provided formatting options
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ModulusRecord GetModulusRecord(long input, string Hyphen = "-", string NumberGroupSeparator = "")
        {
            ModulusRecord mr = new(input, GetModulus(input), Hyphen, NumberGroupSeparator);

            return mr;
        }



        /// <summary>
        /// Generates the checksum digit
        /// </summary>
        /// <param name="input">Numeric-format ID (without checksum digit)</param>
        /// <returns>Checksum digit</returns>
        /// <exception cref="ArgumentException"></exception>
        public string GetModulus(long input)
        {
            // Convert to string
            string cleanedInput = input.ToString();

            // Check if the length is within the allowed range
            if (input < 0)
            {
                throw new ModulusNegativeException(input);
            }

            // Calculate modulo 11
            int sum = 0;
            int multiplier = 2;

            for (int i = cleanedInput.Length - 1; i >= 0; i--)
            {
                int digit = cleanedInput[i] - '0';
                sum += digit * multiplier;

                if (++multiplier > 7)
                {
                    multiplier = 2;
                }
            }

            int remainder = sum % 11;

            // Subtract as part of the process
            int dv = (11 - remainder) % 11; // Modulo to convert 11 to 0

            // If it is 10, return the character for modulo 11
            string result = (dv == 10) ? CharFor11Value : dv.ToString();

            // Return the checksum digit or modulo 11
            return result;
        }
    }
}
