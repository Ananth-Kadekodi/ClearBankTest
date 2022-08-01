using ClearBank.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountValidator : IAccountValidator
    {
        public IPaymentValidator RetrieveInstance(MakePaymentRequest request)
        {
            switch (request.PaymentScheme)
            {
                case PaymentScheme.Bacs:
                    return new BacsValidator();
                case PaymentScheme.FasterPayments:
                    return new FasterPaymentsValidator();
                case PaymentScheme.Chaps:
                    return new ChapsValidator();
                default:
                    return new NullPaymentValidator();
            }
        }
    }
}
