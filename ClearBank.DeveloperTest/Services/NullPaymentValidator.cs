using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class NullPaymentValidator : IPaymentValidator
    {
        public bool IsValid(MakePaymentRequest request, Account account)
        {
            return false;
        }
    }
}