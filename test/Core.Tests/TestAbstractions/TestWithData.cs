using Core.Tests.Builders;

namespace Core.Tests.TestAbstractions
{
    public abstract class TestWithData : ITest
    {
        public T A<T>(IBuilder<T> builder) => builder.Build();
    }
}