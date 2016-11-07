using Core.Models;

namespace Core.Services.Interfaces
{
    public interface IApiRepository<in TParameter, out TGet>
    {
        TGet Get(TParameter parameter);
    }
}