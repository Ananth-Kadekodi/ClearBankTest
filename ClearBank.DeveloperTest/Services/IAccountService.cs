using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IAccountService
    {
        Account GetAccount(string debtorAccountNumber);

        void UpdateAccountDetails(MakePaymentRequest request, Account account);

    }
}