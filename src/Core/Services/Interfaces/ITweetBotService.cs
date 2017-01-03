using Core.Models;

namespace Core.Services.Interfaces
{
    public interface ITweetBotService : IService
    {
        OperationStatus StoreNew();
    }
}