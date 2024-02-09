# Modulus 11 Rut

Módulo 11 Es un método aritmético usado para validar números enteros. Es ampliamente usado en criptografía y validación de entrada.
Algunas implementaciones son usadas para validar códigos numéricos de documentos.

Como ejemplo el ID nacional de Chile, RUT, usa un número entero para identificar personas, instituciones y empresas. Para reducir errores , el módulo 11 de ese número es agregado al final.

### ID: 12345678
#### Módulo 11 del ID: 9
#### Rut: 12345678-**9**

## ID: 11222333
#### Módulo 11 del ID: 8
#### Rut: 11222333-**8**


## Cómo usar Modulus 11

Módulos 11 se compone de 2 paquetes : 
- Modulus 11, que calcula el módulo
- Modulus 11 Rut, que encapsula la librería para crear, validar Y formatear un Rut 

```
 long iNumber = 12345678;

 Modulus11 m11 = new Modulus11();

 // Para especificar un guión o separador de grupo, asigne los parámetros Hyphen y NumberGroupSeparator
 // cuando llame al método GetModulusRecord. Por omisión es '-' y sin separador de grupo.
 var m = m11.GetModulusRecord(iNumber);

 // Usa la información configurada
 string formatedWithHyphen = m.ToString();

 Console.WriteLine("El RUT es {0}", formatedWithHyphen); 

```


### Cómo usar Modulus 11.Rut

#### Validar

```

 string aRut = "12.345.678-9";

 // La librería ha sido probada de 1 a 10 dígitos, pero la mayoría de las aplicaciones no lo soporta
 RutManager rm = new (6,8); // Desde 6 a 8 dígitos, o desde 100.000 hasta 99.999.999

 // También puede especificarse el valor para módulo11=10, normalmente una 'k'
 var result = rm.ValidaRut(sRut, RutManager.TipoValidacionSeparadorEnum.RequerirPuntos);

 if (! result)
 {
	throw new Exception(...)
 }

```


#### Generar
```

 long lRut = 12345678;

 // La librería ha sido probada de 1 a 10 dígitos, pero la mayoría de las aplicaciones no lo soporta
 RutManager rm = new (6,8); // Desde 6 a 8 dígitos, o desde 100.000 hasta 99.999.999

 // Notar el parámetro opcional
 var result = rm.GeneraRutRecord(lRut, conSeparadores: true);

 // El registro es autónomo e inmutable
 Console.WriteLine("Número original   :" + result.Number); 
 Console.WriteLine("Dígito verificador:" + result.Digit);
 Console.WriteLine("Rut con formato   :" + result.ToString());

```