using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class Benchmark
{
    private readonly Customer _customer = new();

    [Benchmark]
    public void DirectFieldSet()
    {
        _customer.PublicId = 42;
    }

    [Benchmark]
    public void ReflectionSet()
    {
        PropertySetters.SetWithReflection(_customer, 42);
    }

    [Benchmark]
    public void ExpressionTreeSet()
    {
        PropertySetters.SetWithExpression(_customer, 42);
    }
}