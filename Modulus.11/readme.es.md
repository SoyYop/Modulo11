# Modulus11

> For a **Chilean Rut** wrapper implementation please check **Modulus 11.Rut**
>
> Para una implementaci�n del envoltorio del **Rut Chileno**, por favor revisa **Modulus 11.Rut**

 
## Prop�sito
*"Facilitar la creaci�n, validaci�n y compartici�n de n�meros con sus correspondientes sumas de verificaci�n Modulo 11."*

Esto se logra mediante:
- Crear una biblioteca f�cil de usar.
- Proporcionar personalizaci�n sencilla con par�metros predeterminados.
- Dejar aspectos de implementaci�n, como n�meros m�nimos y m�ximos, al desarrollador.
- Traer un registro independiente centrado en almacenar y dar formato al objeto m�dulo 11.

Modulo 11 es un algoritmo aritm�tico utilizado para validar n�meros enteros. Se utiliza ampliamente en criptograf�a y en la validaci�n de c�digos num�ricos de documentos.

Como ejemplo, la c�dula de identidad nacional de Chile, el RUT, utiliza un n�mero entero para identificar personas, instituciones y empresas. Para reducir errores, se agrega al final el m�dulo 11 de ese n�mero.\
\
**ID: 12345678**\
M�dulo 11 de la ID: 9\
C�dula de Identidad: 12345678-**9**\
\
**ID: 11222333**\
M�dulo 11 de la ID: 8\
C�dula de Identidad: 11222333-**8**

Si los m�dulos 11 no coinciden, sabemos con certeza que hay un error en el n�mero o en la suma de verificaci�n.\
Ayuda a verificar la integridad del n�mero, no est� destinado a corregirlo.


## El algoritmo

Proporcionar� una breve explicaci�n; por favor, busca en Wikipedia para obtener detalles.

B�sicamente, debemos multiplicar cada d�gito, de derecha a izquierda, con un b�fer circular de seis d�gitos (2,3,4,5,6,7), avanzando si tienes m�s de 6 d�gitos en tu c�digo.\
Sumamos todos ellos y obtenemos el m�dulo 11 (n�mero mod 11). Da el residuo.\
Luego, calculamos una diferencia, DIF=11-residuo:\
 Si el resultado es 10, 1\
 Si el resultado es 11, 0\
 De lo contrario, DIFF /* entre 0-9 */

En algunas implementaciones, en lugar de usar 0 cuando el residuo es 10, el c�digo de verificaci�n se reemplaza por un car�cter especial. Un ejemplo es el RUT chileno, donde 10 se reemplaza por una 'k' y no un cero.


### Ejemplo 1: 987654321

| B�fer circular	| 4	 | 3	 | 2	 | 7	 | 6	 | 5	 | 4	 | 3	 | 2	 | 
| --- 	            | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | 
| N�mero	        | 9	 | 8	 | 7	 | 6	 | 5	 | 4	 | 3	 | 2	 | 1	 | 
| Multiplicaci�n	| 4x9	 | 3x8	 | 2x7	 | 7x6	 | 6x5	 | 5x4	 | 4x3	 | 3x2	 | 2x1	 | 
| Resultado	        | 36	 | 24	 | 14	 | 42	 | 30	 | 20	 | 12	 | 6	 | 2	 | 

La suma es 186 y el residuo es 10\
DIF = 11-10=1\
**C�digo = 1**, 987654321 / 1

### Ejemplo 2: 44261539

| B�fer circular	| 4	 | 3	 | 2	 | 7	 | 6	 | 5	 | 4	 | 3	 | 2	 | 
| --- 	            | ---	| ---	| --- 	| --- 	| --- 	| --- 	| --- 	| --- 	| --- 	 | 
| N�mero	        | 	 | 4	 | 4	 | 2	 | 6	 | 1	 | 5	 | 3	 | 9	 | 
| Multiplicaci�n	| 4x	 | 3x4	 | 2x4	 | 7x2	 | 6x6	 | 5x1	 | 4x5	 | 3x3	 | 2x9	 | 
| Resultado	        | 0	 | 12	 | 8	 | 14	 | 36	 | 5	 | 20	 | 9	 | 18	 | 

La suma es 122 y el residuo es 11\
DIF = 11-1=10 // Regla: si 10 => 1\
**C�digo = 1**, 44261539 / 1

Si es un RUT chileno, entonces 10=>'k', 44261539 / k o 44261539-k


## C�mo usar Modulus 11

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
    }

```


Jorge Rojas @ 2024 under MIT license

jorge.rojasmata@outlook.com  /  [+56(9)94328521](tel:+56994328521)