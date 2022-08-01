using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.DeveloperTest.Tests.PaymentValidator
{
    [TestFixture]
    public class BacsValidatorTests
    {
        private BacsValidator _bacsValidator;
        private MakePaymentRequest _makePaymentRequest;
        private Account _account;

        [SetUp]
        public void Setup()
        {
            _bacsValidator = new BacsValidator();
            _makePaymentRequest = new MakePaymentRequest();
            _account = new Account();
        }

        [Test]
        public void IsValidPaymentForBacs()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs;
            var validPayment = _bacsValidator.IsValid(_makePaymentRequest, _account);
            Assert.IsTrue(validPayment);
        }

        [Test]
        public void IsNotValidPaymentNullAccountForBacs()
        {
            var validPayment = _bacsValidator.IsValid(_makePaymentRequest, null);
            Assert.IsFalse(validPayment);
        }

        [TestCase(AllowedPaymentSchemes.Chaps)]
        [TestCase(AllowedPaymentSchemes.FasterPayments)]
        public void IsNotValidPaymentForNonBacsAccount(AllowedPaymentSchemes allowedPaymentSchemes)
        {
            _account.AllowedPaymentSchemes = allowedPaymentSchemes;
            var validPayment = _bacsValidator.IsValid(_makePaymentRequest, _account);
            Assert.IsFalse(validPayment);
        }
    }
}
