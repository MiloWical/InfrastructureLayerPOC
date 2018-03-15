// ***********************************************************************
// Assembly         : DemoService.Contract
// Author           : Milo.Wical
// Created          : 02-26-2018
//
// Last Modified By : Milo.Wical
// Last Modified On : 02-26-2018
// ***********************************************************************
// <copyright file="StaticSettingProvider.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace DemoService.Contract.SettingProvider
{
    using System;

    /// <inheritdoc />
    /// <summary>
    /// Class StaticSettingProvider.
    /// </summary>
    /// <seealso cref="T:DemoService.Contract.SettingProvider.ISettingProvider" />
    public class StaticSettingProvider : ISettingProvider
    {
        /// <summary>
        /// The version
        /// </summary>
        private readonly int _version;

        /// <summary>
        /// The setting
        /// </summary>
        private string _setting;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticSettingProvider"/> class.
        /// </summary>
        /// <param name="injectedVersion">The injected version.</param>
        /// <param name="injectedSetting">The injected setting.</param>
        /// <exception cref="System.ArgumentNullException">injectedSetting</exception>
        public StaticSettingProvider(int injectedVersion, string injectedSetting)
        {
            _version = injectedVersion;
            _setting = injectedSetting ?? throw new ArgumentNullException(nameof(injectedSetting));
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the <see cref="T:System.String" /> with the specified version.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="T:System.ArgumentException"></exception>
        public string this[int version] => version == _version
                                               ? _setting
                                               : throw new
                                                     ArgumentException($"Cannot retrieve a version that is not equal to the configured version ({_version}).");

        /// <inheritdoc />
        /// <summary>
        /// Adds the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <exception cref="T:System.NotImplementedException">Cannot add a setting to this class.</exception>
        public void AddSetting(string setting)
        {
            throw new NotImplementedException("Cannot add a setting to this class.");
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates the setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="setting">The setting.</param>
        /// <exception cref="T:System.ArgumentException"></exception>
        public void UpdateSetting(int version, string setting)
        {
            if (version == _version)
                _setting = setting;
            else
                throw new ArgumentException($"Cannot update a version not equal to the configured version ({_version}).");
        }

        /// <inheritdoc />
        /// <summary>
        /// Removes the setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="T:System.NotImplementedException">Cannot remove a setting from this class.</exception>
        public bool RemoveSetting(int version)
        {
            throw new NotImplementedException("Cannot remove a setting from this class.");
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the latest setting.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetLatestSetting()
        {
            return _setting;
        }
    }
}
