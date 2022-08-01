using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IAccountValidator
    {
        IPaymentValidator RetrieveInstance(MakePaymentRequest request);
    }
}