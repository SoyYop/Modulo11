# Modulus11

> For a **Chilean Rut** wrapper implementation please check **Modulus 11.Rut**
> Para la implementación de un wrapper del **Rut Chileno** por favor revise **Modulus 11.Rut**


## Purpose
*"To facilitate the creation, validation, and sharing of numbers with their associated Modulo 11 checksums."*

It is achieved by:
- Making an easy-to-use library
- Providing easy personalisation with default parameters
- Leaving implementation-specifics, like min and max numbers, to the developer
- Bringing an independent record focused on storing and formatting the modulo 11 object

 
Modulo 11 is an arithmetical algorithm used to validate integer numbers. It is widely used in cryptography and input validation of document numeric codes.

As an example, Chile's national ID, RUT, uses an integer number to identify people, institutions and companies. To reduce mistakes, the modulus 11 of that number is added at the end.


** ID: 12345678 **
Modulus 11 of ID: 9
National ID: 12345678-**9**

** ID: 11222333 **
Modulus 11 of ID: 8
National ID: 11222333-**8**

If the modulo 11 do not match, we know for sure there is a mistake in either the number or the checksum.
It helps to verify the integrity of the number, it is not intended to correct it.


## The algorithm

I will provide a short explanation- Please search in Wikipedia for details.
Basically, we have to multiply each digit, from right to left, with a circular buffer of six digits (2,3,4,5,6,7), rolling up if you have more than 6 digits in your code.
We sum them all and get modulus 11 (number mod 11). It gives the reminder.
Then, we calculate a difference, DIF=11-reminder:
	If  the result is 10, 1
	If  the result is 11, 0
	else, DIFF /* between 0-9 */

In some implementations, instead of using 0 when the remainder is 10, the verification code is replaced by a spacial character.
One example is the chilean rut, where 10 is replaced by a 'k' and not a zero.


### Example 1: 987654321

| Circular buffer	| 4	 | 3	 | 2	 | 7	 | 6	 | 5	 | 4	 | 3	 | 2	 | 
| --- 	            | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | 
| Number	        | 9	 | 8	 | 7	 | 6	 | 5	 | 4	 | 3	 | 2	 | 1	 | 
| Multiplication	| 4x9	 | 3x8	 | 2x7	 | 7x6	 | 6x5	 | 5x4	 | 4x3	 | 3x2	 | 2x1	 | 
| Result	        | 36	 | 24	 | 14	 | 42	 | 30	 | 20	 | 12	 | 6	 | 2	 | 


The sum is 186 and remainder is 10
DIF = 11-10=1
** Code = 1**, 987654321 / 1

### Example 2: 44261539

| Circular buffer	| 4	 | 3	 | 2	 | 7	 | 6	 | 5	 | 4	 | 3	 | 2	 | 
| --- 	            | ---	| ---	| --- 	| --- 	| --- 	| --- 	| --- 	| --- 	| --- 	 | 
| Number	        | 	 | 4	 | 4	 | 2	 | 6	 | 1	 | 5	 | 3	 | 9	 | 
| Multiplication	| 4x	 | 3x4	 | 2x4	 | 7x2	 | 6x6	 | 5x1	 | 4x5	 | 3x3	 | 2x9	 | 
| Result	        | 0	 | 12	 | 8	 | 14	 | 36	 | 5	 | 20	 | 9	 | 18	 | 


The sum is 122 and remainder is 1
DIF = 11-1=10 // Rule: if 10 => 1
**Code = 1**, 44261539 / 1 
If it is a chilean rut, then 10=>'k', 44261539 / k 0r 44261539-k

 
## How to use Modulus 11

```csharp
 long iNumber = 12345678;

 Modulus11 m11 = new Modulus11();

 // To specify hyphen/group separator, set Hyphen and NumberGroupSeparator parameters
 // when calling to GetModulusRecord. Defaults to '-' and no group separator.
 var m = m11.GetModulusRecord(iNumber);

 // Uses provided configuration
 string formattedWithHyphen = m.ToString();

 Console.WriteLine("The self-checked number is {0}", formattedWithHyphen); 

```
---
### Public record ModulusRecord
```csharp        
    /// <param name="Number">The number</param>
    /// <param name="Digit">The already calculated digit</param>
    /// <param name="Hyphen">Number and digit separator</param>
    /// <param name="UseThousandsSeparator">If to use thousands separator</param>
    public record ModulusRecord(long Number, string Digit, string Hyphen = "-", string NumberGroupSeparator = "")
    {
        /// <summary>
        /// In format Number-dv like 12.345.678-9
        /// </summary>
        /// <returns>12.345.678-9</returns>
        public override string ToString()
    }
```
---
### Public class Modulus11
```csharp        
    /// <remarks>Accepts values between 0 and max, tested up to 9 digits
    /// Returns the modulus character, using digits 0-9 plus the '10' character
    /// </remarks>
    public class Modulus11
    {
        /// <summary>
        /// Digit or character to use when the checksum is 10
        /// </summary>
        public string CharFor10Value { get; set; }

        /// <param name="charFor10Value">Character to use for 11-remainder=10, defaults to 1</param>
        public Modulus11(string charFor10Value = "1")

        /// <param name="input"></param>
        /// <returns>ModulusRecord</returns>
        public ModulusRecord GetModulusRecord(long input, string Hyphen = "-", string NumberGroupSeparator = "")


        /// <param name="input">Numeric-format ID (without checksum digit)</param>
        /// <returns>Checksum digit</returns>
        /// <exception cref="ArgumentException"></exception>
        public string GetModulus(long input)

```