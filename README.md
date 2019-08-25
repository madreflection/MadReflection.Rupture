# Rupture

Rupture is a not-so-simple library for extracting a value from an object (`System.Object`) variable, including unboxing or up-casting, null translation, unwrapping from a container, and type conversion.

## Primary Purpose

Rupture is primarily intended to be used as the internal functionality for a class of extension methods that unbox or cast values from properties and method returns of type `System.Object`.  It is designed to handle reference types, value types, and nullable value types with equal ease.

## Install The Package

```powershell
Install-Package MadReflection.Rupture
```

## Concepts / Operations

* **Unboxing** - Converting a boxed value type back to the value type (e.g. Object -> Int32).
* **Up-casting** - Converting an object reference to more derived type information (e.g. Object -> String).
* **Null translation** - Changing null-like but non-null values to null (e.g. DBNull.Value -> null).
* **Unwrapping** - Accessing a value that's held in a container object (e.g. SqlInt32.Value -> Int32).
* **Type conversion** - Changing a value to an entirely different type (e.g. Decimal -> Int64).

Rupture performs *either* unboxing *or* up-casting depending on whether the expected type is of a value type or a reference type.  All other operations are evaluated in the above sequence as enabled in the configuration.

## How To Use It

For usage details and examples, please [see the wiki](wiki).

## Etymology

As an author of an open-source library, I found naming a little challenging in this case.  Whenever possible, I like to have a brand name for the library, something that sets it apart.  I like "Rupture" because there were no NuGet packages matching that search term so it was completely unique.

On the other hand, the type name `ValueExtractor` did not sit well with me but it was the best I could do.  The library is currently in the 0.x.x version range because I'm following SemVer rules, I haven't entirely settled on that type name, and if I changed something like that after going to 1.0.0, SemVer rules would require a full version jump just for that and I want to avoid that at this point.  I would prefer to have that locked in before going to 1.0.0.  I'm open to suggestions.

***Disclaimer***: There is an NPM package called "rupture" which is unrelated.  I named my library without any knowledge of the other.  Mine is not meant to replace, supplant, or otherwise compete with the other.
