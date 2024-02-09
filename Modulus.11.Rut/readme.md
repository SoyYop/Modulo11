# Modulus11

> For a **modulo 11** library please check **Modulus 11**
>
> Para una librer�a de **M�dulo 11**, por favor revisa **Modulus 11**

 
## Prop�sito
*"Facilitar la creaci�n, validaci�n y compartici�n de ruts con sus correspondientes sumas de verificaci�n Modulo 11."*

Esto se logra mediante:
- Crear una biblioteca f�cil de usar.
- Proporcionar personalizaci�n sencilla con par�metros predeterminados.
- Retornar tanto strings formateados como records con el rut desglosado y capaz de dar formato al objeto m�dulo 11.
- Proporcionar las herramientas de formato necesarias para procesar, validar y generar ruts.

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

B�sicamente, debemos multiplicar cada d�gito, de derecha a izquierda, con un b�fer circular de seis d�gitos (2,3,4,5,6,7), avanzando al inicio si tienes m�s de 6 d�gitos en el c�digo.\
Sumamos todos ellos y obtenemos el m�dulo 11 (n�mero mod 11). Da el residuo.\
Luego, calculamos una diferencia, DIF=11-residuo:\
 Si el resultado es 10, usaremos 'K'\
 Si el resultado es 11, 0\
 De lo contrario, DIF /* entre 0-9 */

En algunas implementaciones, en lugar de usar 0 cuando el residuo es 10, el c�digo de verificaci�n se reemplaza por un car�cter especial. Un ejemplo es el RUT chileno, donde 10 se reemplaza por una 'k' y no un cero.


### Ejemplo 1: 987654321

| B�fer circular	| 4	 | 3	 | 2	 | 7	 | 6	 | 5	 | 4	 | 3	 | 2	 | 
| --- 	            | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | --- 	 | 
| N�mero	        | 9	 | 8	 | 7	 | 6	 | 5	 | 4	 | 3	 | 2	 | 1	 | 
| Multiplicaci�n	| 4x9	 | 3x8	 | 2x7	 | 7x6	 | 6x5	 | 5x4	 | 4x3	 | 3x2	 | 2x1	 | 
| Resultado	        | 36	 | 24	 | 14	 | 42	 | 30	 | 20	 | 12	 | 6	 | 2	 | 

La suma es 186 y el residuo es 10\
DIF = 11-10=1\
**C�digo = 1**, 987654321-1

### Ejemplo 2: 44261539

| B�fer circular	| 4	 | 3	 | 2	 | 7	 | 6	 | 5	 | 4	 | 3	 | 2	 | 
| --- 	            | ---	| ---	| --- 	| --- 	| --- 	| --- 	| --- 	| --- 	| --- 	 | 
| N�mero	        | 	 | 4	 | 4	 | 2	 | 6	 | 1	 | 5	 | 3	 | 9	 | 
| Multiplicaci�n	| 4x	 | 3x4	 | 2x4	 | 7x2	 | 6x6	 | 5x1	 | 4x5	 | 3x3	 | 2x9	 | 
| Resultado	        | 0	 | 12	 | 8	 | 14	 | 36	 | 5	 | 20	 | 9	 | 18	 | 

La suma es 122 y el residuo es 11\
DIF = 11-1=10 // Regla: si 10 => 'k'\
**C�digo = 'k'**, 44261539-k


## Validaci�n y generaci�n

La clase RutManager sirve para generar y validar ruts. El constructor puede personalizarse de la siguiente manera:

```csharp
	/// <param name="minDigits">1 a maxDigits</param>
    /// <param name="maxDigits">Desde minDigits hasta 12</param>
    /// <param name="charForKValue">Por omisi�n, "k" min�scula</param>
    public RutManager(byte minDigits = 1, byte maxDigits = 8, char charForKValue = 'k')
```

As�, podremos limitar los rangos (ya no debieran quedar ruts o personas jur�dicas con menos de cuatro d�gitos) y si usaremos k may�scula o min�scula (Recordar que el m�dulo 11 est�ndar usa '1' en vez de 'k')


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
Obtiene un record que mantiene par�metros de formato como gui�n y separador de miles
```csharp
	long number=76113195-8;

	// Entre 5 y 8 d�gitos
	var rm = new RutManager(5,8);

	var sinPuntos = rm.GetRutRecord(number);
	var conPuntos = rm.GetRutRecord(number, "-", NumberGroupSeparator:".");

	Console.Write("Sin puntos para el n�mero " sinPuntos.Number);
	Console.WriteLine(sinPuntos); // o sinPuntos.ToString()

	Console.Write("Con puntos para el n�mero " conPuntos.Number);
	Console.WriteLine(conPuntos); // o conPuntos.ToString()
```

---

### Public enum TipoValidacionSeparadorEnum
```
/// <summary>
/// Opciones de validaci�n de rut
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
	/// <param name="minDigits">1 a maxDigits</param>
    /// <param name="maxDigits">Desde minDigits hasta 12</param>
    /// <param name="charForKValue">Por omisi�n, "k" min�scula</param>
    public RutManager(byte minDigits = 1, byte maxDigits = 8, char charForKValue = 'k');


	/// <param name="rut"></param>
    /// <param name="tipo"></param>
    /// <param name="soloFormato">S�lo valida formato, no el dv ni los d�gitos</param>
    /// <returns></returns>
    public bool ValidaRut(string rut, TipoValidacionSeparadorEnum tipo = TipoValidacionSeparadorEnum.ConOSinPuntos, bool soloFormato = false);
    

	/// <remarks>
    /// El objeto record retornado incluye el formato num�rico definido (conSeparadores) y separador con gui�n
    /// </remarks>
	/// <param name="input">N�mero</param>
    /// <param name="conSeparadores">Puntos o no puntos</param>
    /// <returns>RutRecord con un rut v�lido</returns>
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
```


Jorge Rojas @ 2024 bajo licencia MIT

jorge.rojasmata@outlook.com  /  [+56(9)94328521](tel:+56994328521)
