using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.PaymentValidator
{
    [TestFixture]
    public class ChapsValidatorTests
    {
        private ChapsValidator _chapsValidator;
        private MakePaymentRequest _makePaymentRequest;
        private Account _account;

        [SetUp]
        public void Setup()
        {
            _chapsValidator = new ChapsValidator();
            _makePaymentRequest = new MakePaymentRequest();
            _account = new Account();
        }

        [Test]
        public void IsValidPaymentForChaps()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;
            _account.Status = AccountStatus.Live;
            var validPayment = _chapsValidator.IsValid(_makePaymentRequest, _account);
            Assert.IsTrue(validPayment);
        }

        [Test]
        public void IsInValidPaymentNullAccountForChaps()
        {
            var validPayment = _chapsValidator.IsValid(_makePaymentRequest, null);
            Assert.IsFalse(validPayment);
        }

        [TestCase(AllowedPaymentSchemes.Bacs)]
        [TestCase(AllowedPaymentSchemes.FasterPayments)]
        public void IsInValidPaymentForNonChapsAccount(AllowedPaymentSchemes allowedPaymentSchemes)
        {
            _account.AllowedPaymentSchemes = allowedPaymentSchemes;
            var validPayment = _chapsValidator.IsValid(_makePaymentRequest, _account);
            Assert.IsFalse(validPayment);
        }

        [TestCase(AccountStatus.Disabled)]
        [TestCase(AccountStatus.InboundPaymentsOnly)]
        public void IsInValidPaymentForNonLivsAccounts(AccountStatus accountStatus)
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;
            _account.Status = accountStatus;
            var validPayment = _chapsValidator.IsValid(_makePaymentRequest, _account);
            Assert.IsFalse(validPayment);
        }
    }
}

    
