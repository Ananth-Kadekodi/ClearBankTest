using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountService _accountService;
        private readonly IAccountValidator _accountValidator;

        public PaymentService(IAccountService accountService, IAccountValidator accountValidator)
        {
            _accountService = accountService;
            _accountValidator = accountValidator;
        }
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            Account account = _accountService.GetAccount(request.DebtorAccountNumber);

            var validator = _accountValidator.RetrieveInstance(request);
            var isValid = validator.IsValid(request, account);

            if (isValid)
            {
                _accountService.UpdateAccountDetails(request, account);
                return new MakePaymentResult { Success = true };
            }
            
            return new MakePaymentResult { Success = false };  
        }
    }
}
