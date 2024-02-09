# Modulus 11 Rut

M�dulo 11 Es un m�todo aritm�tico usado para validar n�meros enteros. Es ampliamente usado en criptograf�a y validaci�n de entrada.
Algunas implementaciones son usadas para validar c�digos num�ricos de documentos.

Como ejemplo el ID nacional de Chile, RUT, usa un n�mero entero para identificar personas, instituciones y empresas. Para reducir errores , el m�dulo 11 de ese n�mero es agregado al final.

### ID: 12345678
#### M�dulo 11 del ID: 9
#### Rut: 12345678-**9**

## ID: 11222333
#### M�dulo 11 del ID: 8
#### Rut: 11222333-**8**


## C�mo usar Modulus 11

M�dulos 11 se compone de 2 paquetes : 
- Modulus 11, que calcula el m�dulo
- Modulus 11 Rut, que encapsula la librer�a para crear, validar Y formatear un Rut 

```
 long iNumber = 12345678;

 Modulus11 m11 = new Modulus11();

 // Para especificar un gui�n o separador de grupo, asigne los par�metros Hyphen y NumberGroupSeparator
 // cuando llame al m�todo GetModulusRecord. Por omisi�n es '-' y sin separador de grupo.
 var m = m11.GetModulusRecord(iNumber);

 // Usa la informaci�n configurada
 string formatedWithHyphen = m.ToString();

 Console.WriteLine("El RUT es {0}", formatedWithHyphen); 

```


### C�mo usar Modulus 11.Rut

#### Validar

```

 string aRut = "12.345.678-9";

 // La librer�a ha sido probada de 1 a 10 d�gitos, pero la mayor�a de las aplicaciones no lo soporta
 RutManager rm = new (6,8); // Desde 6 a 8 d�gitos, o desde 100.000 hasta 99.999.999

 // Tambi�n puede especificarse el valor para m�dulo11=10, normalmente una 'k'
 var result = rm.ValidaRut(sRut, RutManager.TipoValidacionSeparadorEnum.RequerirPuntos);

 if (! result)
 {
	throw new Exception(...)
 }

```


#### Generar
```

 long lRut = 12345678;

 // La librer�a ha sido probada de 1 a 10 d�gitos, pero la mayor�a de las aplicaciones no lo soporta
 RutManager rm = new (6,8); // Desde 6 a 8 d�gitos, o desde 100.000 hasta 99.999.999

 // Notar el par�metro opcional
 var result = rm.GeneraRutRecord(lRut, conSeparadores: true);

 // El registro es aut�nomo e inmutable
 Console.WriteLine("N�mero original   :" + result.Number); 
 Console.WriteLine("D�gito verificador:" + result.Digit);
 Console.WriteLine("Rut con formato   :" + result.ToString());

```