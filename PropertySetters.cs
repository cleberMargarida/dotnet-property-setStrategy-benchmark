using System;
using System.Linq.Expressions;
using System.Reflection;

public static class PropertySetters
{
    public static void SetWithReflection(Customer customer, int value)
    {
        var prop = typeof(Customer).GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        prop?.SetValue(customer, value);
    }

    private static readonly Action<Customer, int> SetId = CreateCompiledSetter();
    private static Action<Customer, int> CreateCompiledSetter()
    {
        var paramCustomer = Expression.Parameter(typeof(Customer), "c");
        var paramValue = Expression.Parameter(typeof(int), "v");
        var property = typeof(Customer).GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        var call = Expression.Call(paramCustomer, property!.SetMethod!, paramValue);
        return Expression.Lambda<Action<Customer, int>>(call, paramCustomer, paramValue).Compile();
    }

    public static void SetWithExpression(Customer customer, int value)
    {
        SetId(customer, value);
    }

    
}