using Core.Tests.Builders;

namespace Core.Tests.TestAbstractions
{
    public interface ITest
    {
        T A<T>(IBuilder<T> builder);
    }
}