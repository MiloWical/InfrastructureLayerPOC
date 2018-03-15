namespace DemoService.Test.UnitTest
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Contract.SettingProvider;
    using Implementation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rhino.Mocks;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DemonstrationServiceUnitTests
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        private ISettingProvider _settingProvider;

        [TestInitialize]
        public void Initialize()
        {
            _settingProvider = MockRepository.GenerateStrictMock<ISettingProvider>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            //_settingProvider.VerifyAllExpectations();
            _settingProvider = null;
        }

        [TestMethod]
        // ReSharper disable ObjectCreationAsStatement
        public void ConstructorUnitTest() => new DemonstrationService(_settingProvider);
        // ReSharper restore ObjectCreationAsStatement

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        // ReSharper disable ObjectCreationAsStatement
        public void ConstructorNullSettingProviderUnitTest() => new DemonstrationService(null);
        // ReSharper restore ObjectCreationAsStatement

        [TestMethod]
        public void AddVersionedSettingUnitTest()
        {
            const string setting = "TestSetting";

            _settingProvider.Expect(sp => sp.AddSetting(Arg<string>.Is.Equal(setting)));

            var service = new DemonstrationService(_settingProvider);
            service.AddVersionedSetting(setting);
        }

        [TestMethod]
        public void GetLatestVersionedSettingUnitTest()
        {
            const string setting = "TestSetting";

            _settingProvider.Expect(sp => sp.GetLatestSetting()).Return(setting);

            var service = new DemonstrationService(_settingProvider);
            var result = service.GetLatestVersionedSetting();

            Assert.AreEqual(setting, result);
        }

        [TestMethod]
        public void GetVersionedSettingUnitTest()
        {
            const string setting = "TestSetting";
            const int version = 12345;

            _settingProvider.Expect(sp => sp[Arg<int>.Is.Equal(version)]).Return(setting);

            var service = new DemonstrationService(_settingProvider);
            var result = service.GetVersionedSetting(version);

            Assert.AreEqual(setting, result);
        }

        [TestMethod]
        public void RemoveVersionedSettingUnitTest()
        {
            const int version = 12345;

            _settingProvider.Expect(sp => sp.RemoveSetting(Arg<int>.Is.Equal(version))).Return(true);

            var service = new DemonstrationService(_settingProvider);

            Assert.IsTrue(service.RemoveVersionedSetting(version));

            _settingProvider.Expect(sp => sp.RemoveSetting(Arg<int>.Is.NotEqual(version))).Return(false);

            Assert.IsFalse(service.RemoveVersionedSetting(version + 1));
        }

        [TestMethod]
        public void UpdateVersionedSettingUnitTest()
        {
            const string setting = "TestSetting";
            const int version = 12345;

            _settingProvider.Expect(sp => sp.UpdateSetting(Arg<int>.Is.Equal(version), Arg<string>.Is.Equal(setting)));

            var service = new DemonstrationService(_settingProvider);
            service.UpdateVersionedSetting(version, setting);
        }
    }
}
