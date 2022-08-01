using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using NUnit.Framework;
using System;

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class RetrieveDataStoreTests
    {
        private RetrieveDataStore _retrieveDataStore;

        [SetUp]
        public void Setup()
        {
            _retrieveDataStore = new RetrieveDataStore();
        }

        [TestCase("Random", typeof(AccountDataStore))]
        [TestCase("Backup", typeof(BackupAccountDataStore))]
        [TestCase(null, typeof(AccountDataStore))]
        [TestCase("",typeof(AccountDataStore))]
        public void RetrieveCorrectDataStore(string dataStoreType, Type type)
        {
            var dataStore = _retrieveDataStore.RetrieveAccountDataStore(dataStoreType);
            Assert.That(dataStore, Is.InstanceOf(type));
        }
    }
}
