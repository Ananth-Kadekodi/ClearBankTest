using ClearBank.DeveloperTest.Data;

namespace ClearBank.DeveloperTest.Services
{
    public class DataStoreRetriever : IRetrieveDataStore
    {
        private const string DataStoreType = "Backup";

        public IAccountDataStore RetrieveAccountDataStore(string dataStoreType)
        {
            if (dataStoreType == DataStoreType)
            {
                return new BackupAccountDataStore();
            }

            return new AccountDataStore();
        }

    }
}
