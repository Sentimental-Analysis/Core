namespace Core.Tests.Builders
{
    public interface IBuilder<out T>
    {
        T Build();
    }
}