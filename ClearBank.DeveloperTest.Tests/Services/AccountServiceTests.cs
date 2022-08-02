using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class AccountServiceTests
    {
        private Mock<IAccountDataStore> _accountDataStoreMock;
        private IAccountService _accountService;
        private Mock<IConfigurationService> _configurationServiceMock;
        private Mock<IRetrieveDataStore> _retrieveDataStoreMock;

        [SetUp]
        public void SetUp()
        {
            _accountDataStoreMock = new Mock<IAccountDataStore>();
            _configurationServiceMock = new Mock<IConfigurationService>();
            _retrieveDataStoreMock = new Mock<IRetrieveDataStore>();

            const string testDataStoreType = "live";
            _configurationServiceMock.Setup(c => c.RetrieveConfiguration("DataStoreType")).Returns(testDataStoreType);

            _retrieveDataStoreMock.Setup(d => d.RetrieveAccountDataStore(testDataStoreType)).Returns(_accountDataStoreMock.Object);

            _accountService = StartService();

        }

        public IAccountService StartService() => new AccountService(_configurationServiceMock.Object, _retrieveDataStoreMock.Object);

        [Test]
        public void ReturnCorrectAccountDetails()
        {
            const string testDebtorAccountNumber = "5678";

            _accountService.GetAccount(testDebtorAccountNumber);

            _accountDataStoreMock.Verify(a => a.GetAccount(testDebtorAccountNumber));
        }

        [TestCase("InboundPaymentsOnly")]
        [TestCase("Backup")]
        public void AssessCorrectDataStoreRetrieved(string dataStoreType)
        {
            _configurationServiceMock.Setup(x => x.RetrieveConfiguration(It.IsAny<string>())).Returns(dataStoreType);
            StartService();
            _retrieveDataStoreMock.Verify(x => x.RetrieveAccountDataStore(dataStoreType));
        }

        [TestCase(500, 300, 200)]
        [TestCase(800, 400, 400)]
        public void UpdateExistingBankAccountBalance(int initBalance, int amountDebited, int leftOverBalance)
        {
            var account = new Account();
            var makePaymentRequest = new MakePaymentRequest();

            account.Balance = initBalance;
            makePaymentRequest.Amount = amountDebited;

            _accountService.UpdateAccountDetails(makePaymentRequest, account);
            Assert.AreEqual(leftOverBalance, account.Balance);
            _accountDataStoreMock.Verify(x => x.UpdateAccount(account));
        }
    }
}
