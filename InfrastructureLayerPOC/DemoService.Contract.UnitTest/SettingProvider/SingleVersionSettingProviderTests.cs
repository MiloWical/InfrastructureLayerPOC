namespace DemoService.Contract.UnitTest.SettingProvider
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Contract.SettingProvider;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SingleVersionSettingProviderTests
    {
        private const int TestVersion = 11235;

        private readonly string _testSetting = typeof(SingleVersionSettingProvider).AssemblyQualifiedName;

        [TestMethod]
        public void NullSettingConstructorTest()
        {
            ArgumentNullException thrownException = null;

            try
            {
                // ReSharper disable ObjectCreationAsStatement
                new SingleVersionSettingProvider(0, null);
                // ReSharper restore ObjectCreationAsStatement
            }
            catch (ArgumentNullException ane)
            {
                thrownException = ane;
            }

            Assert.IsNotNull(thrownException);
            Assert.AreEqual("injectedSetting", thrownException.ParamName);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            // ReSharper disable ObjectCreationAsStatement
            new SingleVersionSettingProvider(TestVersion, _testSetting);
            // ReSharper restore ObjectCreationAsStatement
        }

        [TestMethod]
        public void GetSettingByLatestVersionTest()
        {
            ISettingProvider provider = new SingleVersionSettingProvider(TestVersion, _testSetting);

            Assert.AreEqual(_testSetting, provider.GetLatestSetting());
        }

        [TestMethod]
        public void GetSettingByVersionTest()
        {
            ISettingProvider provider = new SingleVersionSettingProvider(TestVersion, _testSetting);

            Assert.AreEqual(_testSetting, provider[TestVersion]);
        }

        [TestMethod]
        public void GetSettingThrowsExceptionWithNoVersionTest()
        {
            var exceptionMessage =
                $"Cannot retrieve a version that is not equal to the configured version (null).";

            ISettingProvider provider = new SingleVersionSettingProvider();

            ArgumentException thrownException = null;

            try
            {
                // ReSharper disable UnusedVariable
                var version = provider[TestVersion];
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
        public void GetSettingThrowsExceptionWithBadVersionTest()
        {
            var exceptionMessage =
                $"Cannot retrieve a version that is not equal to the configured version ({TestVersion}).";

            ISettingProvider provider = new SingleVersionSettingProvider(TestVersion, _testSetting);

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
            const string originalSetting = "Original Setting";
            const string newSetting = "New Setting";
            const int version = 1;
            
            ISettingProvider provider = new SingleVersionSettingProvider();

            var setting = provider.GetLatestSetting();

            Assert.IsNull(setting);

            provider.AddSetting(originalSetting);

            setting = provider.GetLatestSetting();

            Assert.AreEqual(originalSetting, setting);

            setting = provider[version];

            Assert.AreEqual(originalSetting, setting);

            provider.AddSetting(newSetting);

            setting = provider.GetLatestSetting();

            Assert.AreEqual(newSetting, setting);

            setting = provider[version + 1];

            Assert.AreEqual(newSetting, setting);
        }

        [TestMethod]
        public void RemoveSettingTest()
        {
            const string insertedSetting = "Test Setting";
            const int version = 1;

            ISettingProvider provider = new SingleVersionSettingProvider();

            Assert.IsFalse(provider.RemoveSetting(version));

            provider.AddSetting(insertedSetting);

            Assert.IsFalse(provider.RemoveSetting(version + 1));
            Assert.IsTrue(provider.RemoveSetting(version));
            
            Assert.IsNull(provider.GetLatestSetting());
        }

        [TestMethod]
        public void UpdateSettingWithWrongVersionTest()
        {
            const string originalSetting = "Original Setting";
            const string newSetting = "New Setting";
            const int version = 1;
            
            ISettingProvider provider = new SingleVersionSettingProvider();

            provider.UpdateSetting(version, newSetting);

            Assert.IsNull(provider.GetLatestSetting());

            provider.AddSetting(originalSetting);

            Assert.AreEqual(originalSetting, provider[version]);

            provider.UpdateSetting(version + 1, newSetting);

            Assert.AreEqual(originalSetting, provider[version]);
            Assert.AreEqual(originalSetting, provider.GetLatestSetting());
        }

        [TestMethod]
        public void UpdateSettingWithProperVersionTest()
        {
            const string newSetting = "New Setting";
            
            ISettingProvider provider = new SingleVersionSettingProvider(TestVersion, _testSetting);

            provider.UpdateSetting(TestVersion, newSetting);

            var updatedSetting = provider.GetLatestSetting();

            Assert.AreEqual(newSetting, updatedSetting);
        }

    }
}
