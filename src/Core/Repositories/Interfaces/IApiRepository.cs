namespace Core.Repositories.Interfaces
{
    public interface IApiRepository<in TParameter, out TGet>
    {
        TGet Get(TParameter parameter);
    }
}