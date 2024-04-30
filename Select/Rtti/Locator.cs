using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;

namespace Select.Rtti;

/// <summary>
/// Utility methods to help find types in specified assemblies.
/// </summary>
public class Locator
{
    private readonly ImmutableList<Assembly> _assemblies;

    public Locator()
        : this(Assembly.GetCallingAssembly())
    {
    }

    public Locator(params Assembly[] assemblies)
        => _assemblies = new List<Assembly>(assemblies).ToImmutableList();

    /// <summary>
    /// Finds the types with the specified attribute type in the specified assemblies.
    /// </summary>
    /// <param name="strict">If true, subclasses are ignored.</param>
    public IEnumerable<(Type type, T attribute)> FindTypesBy<T>(bool strict = true)
        where T : Attribute
    {
        var attributeType = typeof(T);

        foreach (var assembly in _assemblies)
        {
            foreach (var type in assembly.GetTypes())
            {
                var attributes = type.GetCustomAttributes(attributeType, false);

                if (attributes.Length != 1) continue;

                var attribute = attributes[0];
                if (strict && attribute.GetType() != attributeType) continue;
                yield return (type, (T)attributes[0]);
            }
        }
    }

    /// <summary>
    /// Finds all the methods with the specified attribute in the specified type.
    /// </summary>
    public static IEnumerable<(MethodInfo method, T attribute)> FindMethodsBy<T>(
        Type type, BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
        where T : Attribute
    {
        var attributeType = typeof(T);

        foreach (var method in type.GetMethods(flags))
        {
            var attributes = method.GetCustomAttributes(attributeType, false);

            if (attributes.Length == 1)
            {
                yield return (method, (T)attributes[0]);
            }
        }
    }

    /// <summary>
    /// Finds the first occurrence of a type with the specified fully qualified name
    /// in the specified assemblies.
    /// </summary>
    //public Result<Type> FindType(string fullname)
    //{
    //    foreach (var assembly in _assemblies)
    //    {
    //        var type = assembly.GetType(fullname);
    //        if (type != null) return Result.Ok(type);
    //    }

    //    return Result.Fail<Type>($"Unable to locate type: {fullname}");
    //}
}
