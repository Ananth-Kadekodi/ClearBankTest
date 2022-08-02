using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private PaymentService _paymentService;
        private Mock<IAccountService> _accountServiceMock;
        private Mock<IAccountValidator> _accountValidatorMock;
        private Mock<IPaymentValidator> _paymentValidatorMock;

        [SetUp]
        public void SetUp()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _accountValidatorMock = new Mock<IAccountValidator>();
            _paymentValidatorMock = new Mock<IPaymentValidator>();
            _paymentService = new PaymentService(_accountServiceMock.Object, _accountValidatorMock.Object);
        }

        [Test]
        public void AccountNotUpdatedWhenInvalid()
        {
            _paymentValidatorMock.Setup(x => x.IsValid(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>())).Returns(false);

            _accountValidatorMock.Setup(x => x.RetrieveInstance(It.IsAny<MakePaymentRequest>())).Returns(_paymentValidatorMock.Object);

            var paymentResult = _paymentService.MakePayment(new MakePaymentRequest());

            _accountServiceMock.Verify(x => x.UpdateAccountDetails(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>()), Times.Never);
            Assert.IsFalse(paymentResult.Success);
        }

        [Test]
        public void AccountUpdatedWhenValid()
        {
            _paymentValidatorMock.Setup(x => x.IsValid(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>())).Returns(true);

            _accountValidatorMock.Setup(x => x.RetrieveInstance(It.IsAny<MakePaymentRequest>())).Returns(_paymentValidatorMock.Object);

            var paymentResult = _paymentService.MakePayment(new MakePaymentRequest());

            _accountServiceMock.Verify(x => x.UpdateAccountDetails(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>()), Times.Once);
            Assert.IsTrue(paymentResult.Success);
        }

        [Test]
        public void AccountNotUpdatedWithInvalidAccount()
        {
            var makePaymentTest = new MakePaymentRequest { DebtorAccountNumber = "ABC" };

            var paymentResult = _paymentService.MakePayment(makePaymentTest);

            _accountServiceMock.Verify(x => x.UpdateAccountDetails(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>()), Times.Never);
            Assert.IsFalse(paymentResult.Success);
        }

    }

    
}
