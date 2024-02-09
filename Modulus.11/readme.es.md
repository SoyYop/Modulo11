# Modulus11

> For a **Chilean Rut** wrapper implementation please check **Modulus 11.Rut**
>
> Para una implementación del envoltorio del **Rut Chileno**, por favor revisa **Modulus 11.Rut**

 
## Propósito
*"Facilitar la creación, validación y compartición de números con sus correspondientes sumas de verificación Modulo 11."*

Esto se logra mediante:
- Crear una biblioteca fácil de usar.
- Proporcionar personalización sencilla con parámetros predeterminados.
- Dejar aspectos de implementación, como números mínimos y máximos, al desarrollador.
- Traer un registro independiente centrado en almacenar y dar formato al objeto módulo 11.

Modulo 11 es un algoritmo aritmético utilizado para validar números enteros. Se utiliza ampliamente en criptografía y en la validación de códigos numéricos de documentos.

Como ejemplo, la cédula de identidad nacional de Chile, el RUT, utiliza un número entero para identificar personas, instituciones y empresas. Para reducir errores, se agrega al final el módulo 11 de ese número.\
\
**ID: 12345678**\
Módulo 11 de la ID: 9\
Cédula de Identidad: 12345678-**9**\
\
**ID: 11222333**\
Módulo 11 de la ID: 8\
Cédula de Identidad: 11222333-**8**

Si los módulos 11 no coinciden, sabemos con certeza que hay un error en el número o en la suma de verificación.\
Ayuda a verificar la integridad del número, no está destinado a corregirlo.


## El algoritmo

Proporcionaré una breve explicación; por favor, busca en Wikipedia para obtener detalles.

Básicamente, debemos multiplicar cada dígito, de derecha a izquierda, con un búfer circular de seis dígitos (2,3,4,5,6,7), avanzando si tienes más de 6 dígitos en tu código.\
Sumamos todos ellos y obtenemos el módulo 11 (número mod 11). Da el residuo.\
Luego, calculamos una diferencia, DIF=11-residuo:\
 Si el resultado es 10, 1\
 Si el resultado es 11, 0\
 De lo contrario, DIFF /* entre 0-9 */

En algunas implementaciones, en lugar de usar 0 cuando el residuo es 10, el código de verificación se reemplaza por un carácter especial. Un ejemplo es el RUT chileno, donde 10 se reemplaza por una 'k' y no un cero.


### Ejemplo 1: 987654321

| Búfer circular	| 4	 | 3	 | 2	 | 7	 | 6	 | 5	 | 4	 | 3	 | 2	 | 
| --- 	            | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | 
| Número	        | 9	 | 8	 | 7	 | 6	 | 5	 | 4	 | 3	 | 2	 | 1	 | 
| Multiplicación	| 4x9	 | 3x8	 | 2x7	 | 7x6	 | 6x5	 | 5x4	 | 4x3	 | 3x2	 | 2x1	 | 
| Resultado	        | 36	 | 24	 | 14	 | 42	 | 30	 | 20	 | 12	 | 6	 | 2	 | 

La suma es 186 y el residuo es 10\
DIF = 11-10=1\
**Código = 1**, 987654321 / 1

### Ejemplo 2: 44261539

| Búfer circular	| 4	 | 3	 | 2	 | 7	 | 6	 | 5	 | 4	 | 3	 | 2	 | 
| --- 	            | ---	| ---	| --- 	| --- 	| --- 	| --- 	| --- 	| --- 	| --- 	 | 
| Número	        | 	 | 4	 | 4	 | 2	 | 6	 | 1	 | 5	 | 3	 | 9	 | 
| Multiplicación	| 4x	 | 3x4	 | 2x4	 | 7x2	 | 6x6	 | 5x1	 | 4x5	 | 3x3	 | 2x9	 | 
| Resultado	        | 0	 | 12	 | 8	 | 14	 | 36	 | 5	 | 20	 | 9	 | 18	 | 

La suma es 122 y el residuo es 11\
DIF = 11-1=10 // Regla: si 10 => 1\
**Código = 1**, 44261539 / 1

Si es un RUT chileno, entonces 10=>'k', 44261539 / k o 44261539-k


## Cómo usar Modulus 11

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