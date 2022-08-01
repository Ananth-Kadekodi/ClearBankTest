using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class AccountValidatorTest
    {
        private AccountValidator _accountValidator;
        private IPaymentValidator _bacsValidator;
        private IPaymentValidator _fasterPaymentsValidator;
        private IPaymentValidator _chapsValidator;

        [SetUp]
        public void Setup()
        {
            _accountValidator = new AccountValidator();
        }

        [Test]
        public void TestBacsValidatorReturn()
        {
            var bacsValidator = _accountValidator.RetrieveInstance(new MakePaymentRequest { PaymentScheme = PaymentScheme.Bacs });

            Assert.IsInstanceOf<BacsValidator>(bacsValidator);
        }

        [Test]
        public void TestFasterPaymentsValidatorReturn()
        {
            var fasterPaymentsValidator = _accountValidator.RetrieveInstance(new MakePaymentRequest { PaymentScheme = PaymentScheme.FasterPayments });

            Assert.IsInstanceOf<FasterPaymentsValidator>(fasterPaymentsValidator);
        }

         [Test]
        public void TestChapsValidatorReturn()
        {
            var chapsValidator = _accountValidator.RetrieveInstance(new MakePaymentRequest { PaymentScheme = PaymentScheme.Chaps });

            Assert.IsInstanceOf<ChapsValidator>(chapsValidator);
        }
    }
}
