# Modulus 11

Modulus 11 is an aritmetical method used to validate integer numbers. It is widely used in criptography and validation. Some implementations are used in document validation.

As an example, Chile's national ID has a long integer number to identify people, institutions and companies. To reduce mistakes, the modulus 11 of that number is added at the end.

### ID: 12345678
#### Modulus 11 of ID: 9
#### National ID: 12345678-**9**

## ID: 11222333
#### Modulus 11 of ID: 8
#### National ID: 11222333-**8**

If the modulus 11 do not match, we know for sure there is a mistake in either the number or the checksum.
It helps to verify the integrity of the number, it is not intended to correct it.


## How to use it


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