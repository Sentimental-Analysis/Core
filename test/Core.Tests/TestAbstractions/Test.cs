using Core.Tests.Builders;

namespace Core.Tests.TestAbstractions
{
    public abstract class Test : ITest
    {
        public T A<T>(IBuilder<T> builder)
        {
            return builder.Build();
        }
    }
}