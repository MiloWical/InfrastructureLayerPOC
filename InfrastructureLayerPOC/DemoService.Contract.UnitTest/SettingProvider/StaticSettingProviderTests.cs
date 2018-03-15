namespace DemoService.Contract.UnitTest.SettingProvider
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Contract.SettingProvider;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class StaticSettingProviderTests
    {
        private const int TestVersion = 11235;

        private readonly string _testSetting = typeof(StaticSettingProvider).AssemblyQualifiedName;

        [TestMethod]
        public void NullSettingConstructorTest()
        {
            ArgumentNullException thrownException = null;

            try
            {
                // ReSharper disable ObjectCreationAsStatement
                new StaticSettingProvider(0, null);
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
            new StaticSettingProvider(TestVersion, _testSetting);
            // ReSharper restore ObjectCreationAsStatement
        }

        [TestMethod]
        public void GetSettingByLatestVersionTest()
        {
            ISettingProvider provider = new StaticSettingProvider(TestVersion, _testSetting);

            Assert.AreEqual(_testSetting, provider.GetLatestSetting());
        }

        [TestMethod]
        public void GetSettingByVersionTest()
        {
            ISettingProvider provider = new StaticSettingProvider(TestVersion, _testSetting);

            Assert.AreEqual(_testSetting, provider[TestVersion]);
        }

        [TestMethod]
        public void GetSettingThrowsExceptionWithBadVersionTest()
        {
            var exceptionMessage =
                $"Cannot retrieve a version that is not equal to the configured version ({TestVersion}).";

            ISettingProvider provider = new StaticSettingProvider(TestVersion, _testSetting);

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
        public void AddSettingThrowsExceptionTest()
        {
            const string newSetting = "New Setting";
            const string exceptionMessage = "Cannot add a setting to this class.";

            ISettingProvider provider = new StaticSettingProvider(TestVersion, _testSetting);

            NotImplementedException thrownException = null;

            try
            {
                provider.AddSetting(newSetting);
            }
            catch (NotImplementedException nie)
            {
                thrownException = nie;
            }

            Assert.IsNotNull(thrownException);
            Assert.AreEqual(exceptionMessage, thrownException.Message);
        }

        [TestMethod]
        public void RemoveSettingThrowsExceptionTest()
        {
            const string exceptionMessage = "Cannot remove a setting from this class.";

            ISettingProvider provider = new StaticSettingProvider(TestVersion, _testSetting);

            NotImplementedException thrownException = null;

            try
            {
                provider.RemoveSetting(TestVersion);
            }
            catch (NotImplementedException nie)
            {
                thrownException = nie;
            }

            Assert.IsNotNull(thrownException);
            Assert.AreEqual(exceptionMessage, thrownException.Message);
        }

        [TestMethod]
        public void UpdateSettingWithWrongVersionThrowsExceptionTest()
        {
            const string newSetting = "New Setting";
            var exceptionMessage = $"Cannot update a version not equal to the configured version ({TestVersion}).";
            
            ISettingProvider provider = new StaticSettingProvider(TestVersion, _testSetting);

            ArgumentException thrownException = null;

            try
            {
                provider.UpdateSetting(TestVersion + 1, newSetting);
            }
            catch (ArgumentException ae)
            {
                thrownException = ae;
            }

            Assert.IsNotNull(thrownException);
            Assert.AreEqual(exceptionMessage, thrownException.Message);
        }

        [TestMethod]
        public void UpdateSettingWithProperVersionTest()
        {
            const string newSetting = "New Setting";
            
            ISettingProvider provider = new StaticSettingProvider(TestVersion, _testSetting);

            provider.UpdateSetting(TestVersion, newSetting);

            var updatedSetting = provider.GetLatestSetting();

            Assert.AreEqual(newSetting, updatedSetting);
        }

    }
}
