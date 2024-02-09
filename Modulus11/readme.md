# Modulus 11

Modulo 11 is an aritmetical method used to validate integer numbers. It is widely used in criptography and input validation. Some implementations are used in validating document numeric codes.

As an example, Chile's national ID, RUT, uses an integer number to identify people, institutions and companies. To reduce mistakes, the modulus 11 of that number is added at the end.

> For a **Chilean Rut** wrapper implementation please check **Modulus 11.Rut**
> Para la implementación de un wrapper del **Rut Chileno** por favor revise **Modulus 11.Rut**

### ID: 12345678
#### Modulus 11 of ID: 9
#### National ID: 12345678-**9**

## ID: 11222333
#### Modulus 11 of ID: 8
#### National ID: 11222333-**8**

If the modulo 11 do not match, we know for sure there is a mistake in either the number or the checksum.
It helps to verify the integrity of the number, it is not intended to correct it.



## How to use Modulus 11


```
 long iNumber = 12345678;

 Modulus11 m11 = new Modulus11();

 // To specify hyphen/group separator, set Hyphen and NumberGroupSeparator parameters
 // when calling to GetModulusRecord. Defaults to '-' and no group separator.
 var m = m11.GetModulusRecord(iNumber);

 // Uses provided configuration
 string formatedWithHyphen = m.ToString();

 Console.WriteLine("The self-checeked number is {0}", formatedWithHyphen); 

```

