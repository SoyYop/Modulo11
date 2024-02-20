# Modulus11

> For a **modulo 11** library please check **Modulus 11**
>
> Para una librería de **Módulo 11**, por favor revisa **Modulus 11**

 
## Propósito
*"Facilitar la creación, validación y compartición de ruts con sus correspondientes dígitos verificadores en Modulo 11."*

Esto se logra mediante:
- Crear una biblioteca fácil de usar.
- Proporcionar personalización sencilla con parámetros predeterminados.
- Retornar tanto strings formateados como records con el rut desglosado y capaz de dar formato al objeto módulo 11.
- Proporcionar las herramientas de formato necesarias para procesar, validar y generar ruts.

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

Básicamente, debemos multiplicar cada dígito, de derecha a izquierda, con un búfer circular de seis dígitos (2,3,4,5,6,7), avanzando al inicio si tienes más de 6 dígitos en el código.\
Sumamos todos ellos y obtenemos el módulo 11 (número mod 11). Da el residuo.\
Luego, calculamos una diferencia, DIF=11-residuo:\
 Si el resultado es 10, usaremos 'K'\
 Si el resultado es 11, 0\
 De lo contrario, DIF /* entre 0-9 */

En algunas implementaciones, en lugar de usar 0 cuando el residuo es 10, el código de verificación se reemplaza por un carácter especial. Un ejemplo es el RUT chileno, donde 10 se reemplaza por una 'k' y no un cero.


### Ejemplo 1: 987654321

| Búfer circular	| 4	 | 3	 | 2	 | 7	 | 6	 | 5	 | 4	 | 3	 | 2	 | 
| --- 	            | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | 
| Número	        | 9	 | 8	 | 7	 | 6	 | 5	 | 4	 | 3	 | 2	 | 1	 | 
| Multiplicación	| 4x9	 | 3x8	 | 2x7	 | 7x6	 | 6x5	 | 5x4	 | 4x3	 | 3x2	 | 2x1	 | 
| Resultado	        | 36	 | 24	 | 14	 | 42	 | 30	 | 20	 | 12	 | 6	 | 2	 | 

La suma es 186 y el residuo es 10 (186%11 = 10)\
DIF = 11-10=1\
**Código = 1**, 987654321-1

### Ejemplo 2: 44261539

| Búfer circular	| 4	 | 3	 | 2	 | 7	 | 6	 | 5	 | 4	 | 3	 | 2	 | 
| --- 	            | ---	| ---	| --- 	| --- 	| --- 	| --- 	| --- 	| --- 	| --- 	 | 
| Número	        | 	 | 4	 | 4	 | 2	 | 6	 | 1	 | 5	 | 3	 | 9	 | 
| Multiplicación	| 4x	 | 3x4	 | 2x4	 | 7x2	 | 6x6	 | 5x1	 | 4x5	 | 3x3	 | 2x9	 | 
| Resultado	        | 0	 | 12	 | 8	 | 14	 | 36	 | 5	 | 20	 | 9	 | 18	 | 

La suma es 122 y el residuo es 1 (122%11 = 1)\
DIF = 11-1=10 // Regla: si 10 => 'k'\
**Código = 'k'**, 44261539-k


## Validación y generación

La clase RutManager sirve para generar y validar ruts. El constructor puede personalizarse de la siguiente manera:

```csharp
    /// <param name="minDigits">1 a maxDigits</param>
    /// <param name="maxDigits">Desde minDigits hasta 12</param>
    /// <param name="charForKValue">Por omisión, "k" minúscula</param>
    public RutManager(byte minDigits = 1, byte maxDigits = 8, char charForKValue = 'k')
```

Así, podremos limitar los rangos (ya no debieran quedar ruts o personas jurídicas con menos de cuatro dígitos) y si usaremos k mayúscula o minúscula (Recordar que el módulo 11 estándar usa '1' en vez de 'k')


### Validar un rut con o sin puntos
Acepta 12.345.678-5 y 12345678-5
```csharp
	var sRut = "76113195-8";

	var rm = new RutManager();
	
	var result = rm.ValidaRut(sRut, TipoValidacionSeparadorEnum.ConOSinPuntos);

	if (result)
	{
	 // ...
	}
```


### Exigir puntos
Acepta 12.345.678-5 y rechaza 12345678-5
```csharp
	var sRut = "76.113.195-8";

	var rm = new RutManager();
	
	var rm = new RutManager();

	if (result)
	{
	 // ...
	}
```


### Generar rut
Obtiene un record que mantiene parámetros de formato como guión y separador de miles
```csharp
	long number=76113195-8;

	// Entre 5 y 8 dígitos
	var rm = new RutManager(5,8);

	var sinPuntos = rm.GetRutRecord(number);
	var conPuntos = rm.GetRutRecord(number, "-", NumberGroupSeparator:".");

	Console.Write("Sin puntos para el número " sinPuntos.Number);
	Console.WriteLine(sinPuntos); // o sinPuntos.ToString()

	Console.Write("Con puntos para el número " conPuntos.Number);
	Console.WriteLine(conPuntos); // o conPuntos.ToString()
```

---

### Public enum TipoValidacionSeparadorEnum
```
/// <summary>
/// Opciones de validación de rut
/// </summary>
public enum TipoValidacionSeparadorEnum
{
    ConOSinPuntos,
    RequerirPuntos,
    DenegarPuntos,        
}
```


### Public class RutManager

```csharp
/// <summary>
/// Encapsula las operaciones necesarias para validar y generar ruts
/// </summary>
public class RutManager
{
    /// <param name="minDigits">1 a maxDigits</param>
    /// <param name="maxDigits">Desde minDigits hasta 12</param>
    /// <param name="charForKValue">Por omisión, "k" minúscula</param>
    public RutManager(byte minDigits = 1, byte maxDigits = 8, char charForKValue = 'k');


    /// <param name="rut"></param>
    /// <param name="tipo"></param>
    /// <param name="soloFormato">Sólo valida formato, no el dv ni los dígitos</param>
    /// <returns></returns>
    public bool ValidaRut(string rut, TipoValidacionSeparadorEnum tipo = TipoValidacionSeparadorEnum.ConOSinPuntos, bool soloFormato = false);
    

    /// <remarks>
    /// El objeto record retornado incluye el formato numérico definido (conSeparadores) y separador con guión
    /// </remarks>
	/// <param name="input">Número</param>
    /// <param name="conSeparadores">Puntos o no puntos</param>
    /// <returns>RutRecord con un rut válido</returns>
    /// <exception cref="RutOutOfRangeException"></exception>
    public ModulusRecord GeneraRutRecord(long input, bool conSeparadores = false);


    /// <param name="rut"></param>
    /// <param name="conSeparadores">Puntos o no puntos</param>
    /// <returns></returns>
    public string GeneraRut(ModulusRecord rut, bool conSeparadores = false);


    /// <param name="input"></param>
    /// <param name="conSeparadores">Puntos o no puntos</param>
    /// <returns></returns>
    /// <exception cref="RutOutOfRangeException"></exception>
    public string GeneraRut(long input, bool conSeparadores = false);
}
```


---


Jorge Rojas @ 2024 bajo licencia MIT

jorge.rojasmata@outlook.com  /  [+56(9)94328521](tel:+56994328521)
