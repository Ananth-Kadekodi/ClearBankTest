﻿using ClearBank.DeveloperTest.Data;

namespace ClearBank.DeveloperTest.Services
{
    public class RetrieveDataStore : IRetrieveDataStore
    {
        private const string DataStoreType = "Backup";

        public IAccountDataStore RetrieveAccountDataStore(string dataStoreType)
        {
            if (dataStoreType.Equals(DataStoreType))
            {
                return new BackupAccountDataStore();
            }

            return new AccountDataStore();
        }

    }
}
