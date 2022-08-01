using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.PaymentValidator
{
    [TestFixture]
    public class FasterPaymentsValidatorTests
    {
        FasterPaymentsValidator _fasterPaymentsValidator;
        private MakePaymentRequest _makePaymentRequest;
        private Account _account;

        [SetUp]
        public void SetUp()
        {
            _fasterPaymentsValidator = new FasterPaymentsValidator();
            _makePaymentRequest = new MakePaymentRequest();
            _account = new Account();
        }

        [Test]
        public void IsValidPaymentForFasterPayments()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;
            var validPayment = _fasterPaymentsValidator.IsValid(_makePaymentRequest, _account);
            Assert.IsTrue(validPayment);
        }

        [Test]
        public void IsValidPaymentWithPositiveAccountBalance()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;
            _account.Balance = 400;
            _makePaymentRequest.Amount = 300;
            var validPayment = _fasterPaymentsValidator.IsValid(_makePaymentRequest, _account);
            Assert.IsTrue(validPayment);
        }

        [Test]
        public void IsInValidPaymentWithPositiveAccountBalance()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;
            _account.Balance = 400;
            _makePaymentRequest.Amount = 500;
            var validPayment = _fasterPaymentsValidator.IsValid(_makePaymentRequest, _account);
            Assert.IsFalse(validPayment);
        }

        [Test]
        public void IsInValidPaymentNullAccountForFasterPayments()
        {
            var validPayment = _fasterPaymentsValidator.IsValid(_makePaymentRequest, null);
            Assert.IsFalse(validPayment);
        }

        [TestCase(AllowedPaymentSchemes.Bacs)]
        [TestCase(AllowedPaymentSchemes.Chaps)]
        public void IsInValidPaymentForNonFasterPaymentsAccount(AllowedPaymentSchemes allowedPaymentSchemes)
        {
            _account.AllowedPaymentSchemes = allowedPaymentSchemes;
            var validPayment = _fasterPaymentsValidator.IsValid(_makePaymentRequest, _account);
            Assert.IsFalse(validPayment);
        }

        [TestCase(AllowedPaymentSchemes.Bacs)]
        [TestCase(AllowedPaymentSchemes.Chaps)]
        public void IsInValidPayment_ForNonFasterPaymentsAccount_And_NegativeAccountBalance(AllowedPaymentSchemes allowedPaymentSchemes)
        {
            _account.Balance = 400;
            _makePaymentRequest.Amount = 500;
            _account.AllowedPaymentSchemes = allowedPaymentSchemes;
            var validPayment = _fasterPaymentsValidator.IsValid(_makePaymentRequest, _account);
            Assert.IsFalse(validPayment);
        }

        [TestCase(AllowedPaymentSchemes.Bacs)]
        [TestCase(AllowedPaymentSchemes.Chaps)]
        public void IsInValidPayment_ForNonFasterPaymentsAccount_And_PositiveAccountBalance(AllowedPaymentSchemes allowedPaymentSchemes)
        {
            _account.Balance = 500;
            _makePaymentRequest.Amount = 400;
            _account.AllowedPaymentSchemes = allowedPaymentSchemes;
            var validPayment = _fasterPaymentsValidator.IsValid(_makePaymentRequest, _account);
            Assert.IsFalse(validPayment);
        }
    }
}
