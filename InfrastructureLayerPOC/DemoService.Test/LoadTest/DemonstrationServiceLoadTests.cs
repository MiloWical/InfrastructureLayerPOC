namespace DemoService.Test.LoadTest
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Contract.SettingProvider;
    using Implementation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Summary description for DemonstrationServiceLoadTests
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DemonstrationServiceLoadTests
    {
        private const int KeyCount = 100;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region DictionarySettingProvider

        [TestMethod]
        public void DictionarySettingProviderConstructorLoadTest()
        {
            // ReSharper disable ObjectCreationAsStatement
            new DemonstrationService(new DictionarySettingProvider());
            // ReSharper restore ObjectCreationAsStatement
        }

        [TestMethod]
        public void DictionarySettingProviderAddVersionedSettingLoadTest()
        {
            var service = new DemonstrationService(new DictionarySettingProvider());

            for(var i = 0; i < KeyCount; i++)
                service.AddVersionedSetting(Guid.NewGuid().ToString());
        }

        [TestMethod]
        public void DictionarySettingProviderGetLatestVersionedSettingLoadTest()
        {
            var service = new DemonstrationService(new DictionarySettingProvider());

            for (var i = 0; i < KeyCount; i++)
            {
                service.AddVersionedSetting(Guid.NewGuid().ToString());
                service.GetLatestVersionedSetting();
            }
        }

        [TestMethod]
        public void DictionarySettingProviderGetVersionedSettingLoadTest()
        {
            var service = new DemonstrationService(new DictionarySettingProvider());

            for (var i = 0; i < KeyCount; i++)
            {
                service.AddVersionedSetting(Guid.NewGuid().ToString());
                service.GetVersionedSetting(i + 1);
            }
        }

        [TestMethod]
        public void DictionarySettingProviderRemoveVersionedSettingLoadTest()
        {
            var service = new DemonstrationService(new DictionarySettingProvider());

            for (var i = 0; i < KeyCount; i++)
                service.AddVersionedSetting(Guid.NewGuid().ToString()); 

            for (var i = 0; i < KeyCount; i++)
                service.RemoveVersionedSetting(i + 1);
        }

        [TestMethod]
        public void DictionarySettingProviderUpdateVersionedSettingLoadTest()
        {
            var service = new DemonstrationService(new DictionarySettingProvider());

            for (var i = 0; i < KeyCount; i++)
                service.AddVersionedSetting(Guid.NewGuid().ToString());

            for (var i = 0; i < KeyCount; i++)
                service.UpdateVersionedSetting(i + 1, Guid.NewGuid().ToString());
        }

        #endregion

        #region SingleVersionSettingProvider

        [TestMethod]
        public void SingleVersionSettingProviderConstructorLoadTest()
        {
            // ReSharper disable ObjectCreationAsStatement
            new DemonstrationService(new SingleVersionSettingProvider());
            // ReSharper restore ObjectCreationAsStatement
        }

        [TestMethod]
        public void SingleVersionSettingProviderAddVersionedSettingLoadTest()
        {
            var service = new DemonstrationService(new SingleVersionSettingProvider());

            for (var i = 0; i < KeyCount; i++)
                service.AddVersionedSetting(Guid.NewGuid().ToString());
        }

        [TestMethod]
        public void SingleVersionSettingProviderGetLatestVersionedSettingLoadTest()
        {
            var service = new DemonstrationService(new SingleVersionSettingProvider());

            for (var i = 0; i < KeyCount; i++)
            {
                service.AddVersionedSetting(Guid.NewGuid().ToString());
                service.GetLatestVersionedSetting();
            }
        }

        [TestMethod]
        public void SingleVersionSettingProviderGetVersionedSettingLoadTest()
        {
            var service = new DemonstrationService(new SingleVersionSettingProvider());

            for (var i = 0; i < KeyCount; i++)
            {
                service.AddVersionedSetting(Guid.NewGuid().ToString());
                service.GetVersionedSetting(i + 1);
            }
        }

        [TestMethod]
        public void SingleVersionSettingProviderRemoveVersionedSettingLoadTest()
        {
            var service = new DemonstrationService(new SingleVersionSettingProvider());

            for (var i = 0; i < KeyCount; i++)
                service.AddVersionedSetting(Guid.NewGuid().ToString());

            var result = service.RemoveVersionedSetting(KeyCount);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SingleVersionSettingProviderUpdateVersionedSettingLoadTest()
        {
            var service = new DemonstrationService(new SingleVersionSettingProvider());

            for (var i = 0; i < KeyCount; i++)
                service.AddVersionedSetting(Guid.NewGuid().ToString());

            for (var i = 0; i < KeyCount; i++)
                service.UpdateVersionedSetting(i + 1, Guid.NewGuid().ToString());
        }

        #endregion

        #region StaticSettingProvider

        [TestMethod]
        public void StaticSettingProviderConstructorLoadTest()
        {
            // ReSharper disable ObjectCreationAsStatement
            new DemonstrationService(new StaticSettingProvider(KeyCount, Guid.NewGuid().ToString()));
            // ReSharper restore ObjectCreationAsStatement
        }

        [TestMethod]
        public void StaticSettingProviderGetLatestVersionedSettingLoadTest()
        {
            var service = new DemonstrationService(new StaticSettingProvider(KeyCount, Guid.NewGuid().ToString()));

            for (var i = 0; i < KeyCount; i++)
                service.GetLatestVersionedSetting();
        }

        [TestMethod]
        public void StaticSettingProviderGetVersionedSettingLoadTest()
        {
            var service = new DemonstrationService(new StaticSettingProvider(KeyCount, Guid.NewGuid().ToString()));

            for (var i = 0; i < KeyCount; i++)
            {
                service.GetVersionedSetting(KeyCount);
            }
        }

        [TestMethod]
        public void StaticSettingProviderUpdateVersionedSettingLoadTest()
        {
            var service = new DemonstrationService(new StaticSettingProvider(KeyCount, Guid.NewGuid().ToString()));

            for (var i = 0; i < KeyCount; i++)
                service.UpdateVersionedSetting(KeyCount, Guid.NewGuid().ToString());
        }

        #endregion
    }
}
