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

Rupture performs *either* unboxing *or* up-casting depending on whether the expected type is of a reference type or a value type.  All other operations are evaluated in the above sequence as enabled in the configuration.

## How To Use It

Start by configuring an instance.  This is usually done in a static class that will host the extension method(s) that will use it.  The following example shows how one might want to configure a `ValueExtractor` instance to be used by extension methods for `SqlDataReader` and `SqlParameter`.

```csharp
private static readonly ValueExtractor _extractor = ValueExtractor.Create(config =>
{
    // Reference null testing is already covered.  This *adds* a DBNull check on top of it.
    config.UseNullTester(obj => obj is DBNull);
});
```

Then write extension methods for your needs.

```csharp
// Column<T> extension methods for SqlDataReader

public static T Column<T>(this SqlDataReader reader, int i)
{
    if (reader is null)
        throw new ArgumentNullException(nameof(reader));

    return _valueExtractor.Extract<T>(reader.GetValue(i));
}

public static T Column<T>(this SqlDataReader reader, string name)
{
    if (reader is null)
        throw new ArgumentNullException(nameof(reader));

    return _valueExtractor.Extract<T>(reader.GetValue(reader.GetOrdinal(name)));
}


// GetValue<T> extension method for SqlParameter, useful for output parameters

public static T GetValue<T>(this SqlParameter parameter)
{
    if (parameter is null)
        throw new ArgumentNullException(nameof(parameter));

    return _valueExtractor.Extract<T>(parameter.Value);
}
```

See the [wiki](wiki) for more examples of extension methods for other ADO.NET providers.

### Default Extractor

`ValueExtractor.Default` is a completely uncustomized instance.  Using that and casting from `object` are essentially the same.  It's useless for practical purposes and is mostly intended as a baseline for the unit tests.

## Etymology

As an author of an open-source library, I found naming a little challenging in this case.  Whenever possible, I like to have a brand name for the library, something that sets it apart.  I like "Rupture" because there were no NuGet packages matching that search term so it was completely unique.

On the other hand, the type name `ValueExtractor` did not sit well with me but it was the best I could do.  The library is currently in the 0.x.x version range because I'm following SemVer rules, I haven't entirely settled on that type name, and if I changed something like that after going to 1.0.0, SemVer rules would require a full version jump just for that and I want to avoid that at this point.  I would rather have that locked in before going to 1.0.0.  I'm open to suggestions.
