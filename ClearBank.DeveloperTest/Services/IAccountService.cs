using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IAccountService
    {
        Account GetAccount(string debtorAccountNumber);

        void UpdateAccount(MakePaymentRequest request, Account account);

        void CalculateAccountBalance(MakePaymentRequest request, Account account);
    }
}