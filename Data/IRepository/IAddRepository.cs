using System.Threading.Tasks;
using Krunsave.Data.Repository;
using Krunsave.DTO;
using Krunsave.Model;

namespace Krunsave.Data.IRepository
{
    public interface IAddRepository
    {
        Task<bool> Addstore(StoreForRegisterDto store);
        Task<bool> CheckstoreID(int userID, int storeID);
        Task<bool> AddAvailableFood(FoodForRegisterDto foodForRegisterDto);
    }
}