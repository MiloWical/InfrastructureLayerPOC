// ***********************************************************************
// Assembly         : DemoService.Implementation
// Author           : Milo.Wical
// Created          : 02-24-2018
//
// Last Modified By : Milo.Wical
// Last Modified On : 02-25-2018
// ***********************************************************************
// <copyright file="DemonstrationService.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace DemoService.Implementation
{
    using System;
    using Contract;
    using Contract.SettingProvider;

    /// <inheritdoc />
    /// <summary>
    /// Class DemonstrationService.
    /// </summary>
    /// <seealso cref="T:DemoService.Contract.IDemonstrationService" />
    public class DemonstrationService : IDemonstrationService
    {
        /// <summary>
        /// The setting provider
        /// </summary>
        private readonly ISettingProvider _settingProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="DemonstrationService"/> class.
        /// </summary>
        /// <param name="injectedSettingProvider">The injected setting provider.</param>
        /// <exception cref="ArgumentNullException">injectedSettingProvider</exception>
        public DemonstrationService(ISettingProvider injectedSettingProvider)
        {
            _settingProvider = injectedSettingProvider ?? throw new ArgumentNullException(nameof(injectedSettingProvider));
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the versioned setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>System.String.</returns>
        public string GetVersionedSetting(int version)
        {
            return _settingProvider[version];
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the latest versioned setting.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetLatestVersionedSetting()
        {
            return _settingProvider.GetLatestSetting();
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates the versioned setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="setting">The setting.</param>
        public void UpdateVersionedSetting(int version, string setting)
        {
            _settingProvider.UpdateSetting(version, setting);
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds the versioned setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void AddVersionedSetting(string setting)
        {
            _settingProvider.AddSetting(setting);
        }

        /// <inheritdoc />
        /// <summary>
        /// Removes the versioned setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns><c>true</c> if the setting is removed, <c>false</c> otherwise.</returns>
        public bool RemoveVersionedSetting(int version)
        {
            return _settingProvider.RemoveSetting(version);
        }
    }
}
