namespace DemoService.Contract.UnitTest.SettingProvider
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Contract.SettingProvider;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DictionarySettingProviderTests
    {
        private const int TestVersion = 11235;

        private readonly string _testSetting = typeof(DictionarySettingProvider).AssemblyQualifiedName;

        [TestMethod]
        public void ConstructorTest()
        {
            // ReSharper disable ObjectCreationAsStatement
            new DictionarySettingProvider(TestVersion, _testSetting);
            // ReSharper restore ObjectCreationAsStatement
        }

        [TestMethod]
        public void GetSettingByLatestVersionTest()
        {
            ISettingProvider provider = new DictionarySettingProvider(TestVersion, _testSetting);

            Assert.AreEqual(_testSetting, provider.GetLatestSetting());
        }

        [TestMethod]
        public void GetSettingByVersionTest()
        {
            ISettingProvider provider = new DictionarySettingProvider(TestVersion, _testSetting);

            Assert.AreEqual(_testSetting, provider[TestVersion]);
        }

        //[TestMethod]
        public void GetSettingThrowsExceptionWithBadVersionTest()
        {
            var exceptionMessage =
                $"Cannot retrieve a version that is not equal to the configured version ({TestVersion}).";

            ISettingProvider provider = new DictionarySettingProvider(TestVersion, _testSetting);

            ArgumentException thrownException = null;

            try
            {
                // ReSharper disable UnusedVariable
                var version = provider[TestVersion + 1];
                // ReSharper restore UnusedVariable
            }
            catch (ArgumentException ae)
            {
                thrownException = ae;
            }

            Assert.IsNotNull(thrownException);
            Assert.AreEqual(exceptionMessage, thrownException.Message);
        }

        [TestMethod]
        public void AddSettingTest()
        {
            const string newSetting = "New Setting";
            
            ISettingProvider provider = new DictionarySettingProvider(TestVersion, _testSetting);

            provider.AddSetting(newSetting);
            
            Assert.AreEqual(newSetting, provider.GetLatestSetting());
            Assert.AreEqual(newSetting, provider[TestVersion + 1]);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void RemoveSettingTest()
        {
            ISettingProvider provider = new DictionarySettingProvider(TestVersion, _testSetting);

            provider.RemoveSetting(TestVersion);

            // ReSharper disable UnusedVariable
            var setting = provider[TestVersion]; //Used to throw the exception
            // ReSharper restore UnusedVariable
        }

        [TestMethod]
        public void UpdateSettingTest()
        {
            const string newSetting = "New Setting";
            
            ISettingProvider provider = new DictionarySettingProvider(TestVersion, _testSetting);

            provider.UpdateSetting(TestVersion, newSetting);

            var updatedSetting = provider.GetLatestSetting();

            Assert.AreEqual(newSetting, updatedSetting);
        }

    }
}
