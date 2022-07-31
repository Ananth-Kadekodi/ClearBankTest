using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountService
    {
        private IAccountDataStore _accountDataStore;
        public AccountService(IConfigurationService configurationService, IRetrieveDataStore retrieveDataStore)
        {
            var dataStore = configurationService.RetrieveConfiguration("DataStoreType");
            _accountDataStore = retrieveDataStore.Create(dataStore);
        }

        public Account GetAccount(string debtorAccountNumber)
        {
            return _accountDataStore.GetAccount(debtorAccountNumber);
        }
    }

    
}
