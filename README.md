# Rupture

Rupture is a not-so-simple library for extracting a value from an object (`System.Object`) variable, including unboxing, casting, unwrapping from a container, and conversion.

## Install The Package

```powershell
Install-Package MadReflection.Rupture
```

## How To Use It

```csharp
readonly ValueExtractor _extractor = ValueExtractor.Create(config =>
{
    // Reference null is already convered.  This *adds* a DBNull check on top of it.
    config.UseNullTester(obj => obj is DBNull);

    config.UseUnwrapper(
        // Tests System.Data.SqlClient.INullable; if it is, it's a value wrapped in a Sql* type.
        obj => obj is INullable,

        // Sql* all have a public Value property, just not on INullable.  Use a little reflection to get it.  
        obj => obj.GetType().GetProperty("Value").GetValue(obj));

    config.UseHandler<bool>(
        // obj will never be null or anything null-ish.
        (obj, ctx) =>
        {
            if (obj is bool b)
            {
                ctx.Result = b;
                return;
            }

            if (obj is string s)
            {
                ctx.Result = s == "Y";
                return;
            }

            ctx.ThrowInvalidCastException(typeof(string));
        });

    config.UseHandler<string>(
        obj => obj.ToString().TrimEnd());
});
```

