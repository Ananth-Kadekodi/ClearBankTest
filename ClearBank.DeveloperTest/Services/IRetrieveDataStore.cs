using ClearBank.DeveloperTest.Data;

namespace ClearBank.DeveloperTest.Services
{
    public interface IRetrieveDataStore
    {
        IAccountDataStore Create(string dataStoreType);
    }
}