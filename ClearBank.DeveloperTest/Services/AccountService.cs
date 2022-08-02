using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountService : IAccountService
    {
        private IAccountDataStore _accountDataStore;
        public AccountService(IConfigurationService configurationService, IRetrieveDataStore retrieveDataStore)
        {
            var dataStore = configurationService.RetrieveConfiguration("DataStoreType");
            _accountDataStore = retrieveDataStore.RetrieveAccountDataStore(dataStore);
        }

        public Account GetAccount(string debtorAccountNumber)
        {
            return _accountDataStore.GetAccount(debtorAccountNumber);
        }

        public void UpdateAccountDetails(MakePaymentRequest request, Account account)
        {
            account.Balance -= request.Amount;
            _accountDataStore.UpdateAccount(account);
        }
    }


}
