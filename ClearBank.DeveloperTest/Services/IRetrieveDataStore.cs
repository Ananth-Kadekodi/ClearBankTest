using ClearBank.DeveloperTest.Data;

namespace ClearBank.DeveloperTest.Services
{
    public interface IRetrieveDataStore
    {
        IAccountDataStore RetrieveAccountDataStore(string dataStoreType);
    }
}