using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IPaymentValidator
    {
        bool IsValid(MakePaymentRequest request, Account account);
    }
}