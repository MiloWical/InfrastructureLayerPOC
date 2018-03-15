// ***********************************************************************
// Assembly         : DemoService.Contract
// Author           : Milo.Wical
// Created          : 02-26-2018
//
// Last Modified By : Milo.Wical
// Last Modified On : 02-26-2018
// ***********************************************************************
// <copyright file="SingleVersionSettingProvider.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace DemoService.Contract.SettingProvider
{
    using System;

    /// <inheritdoc />
    /// <summary>
    /// Class SingleVersionSettingProvider.
    /// </summary>
    /// <seealso cref="T:DemoService.Contract.SettingProvider.ISettingProvider" />
    public class SingleVersionSettingProvider : ISettingProvider
    {
        /// <summary>
        /// The version
        /// </summary>
        private int? _version;

        /// <summary>
        /// The setting
        /// </summary>
        private string _setting;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleVersionSettingProvider"/> class.
        /// </summary>
        public SingleVersionSettingProvider()
        {
            _version = null;
            _setting = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleVersionSettingProvider"/> class.
        /// </summary>
        /// <param name="injectedVersion">The injected version.</param>
        /// <param name="injectedSetting">The injected setting.</param>
        /// <exception cref="System.ArgumentNullException">injectedSetting</exception>
        public SingleVersionSettingProvider(int injectedVersion, string injectedSetting)
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
        public string this[int version]
        {
            get
            {
                if (_version != null && version == _version)
                    return _setting;

                var versionString = _version != null ? _version.ToString() : "null";

                throw new
                    ArgumentException($"Cannot retrieve a version that is not equal to the configured version ({versionString}).");
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void AddSetting(string setting)
        {
            if (_version == null)
                _version = 0;

            _version++;
            _setting = setting;
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates the setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="setting">The setting.</param>
        public void UpdateSetting(int version, string setting)
        {
            if (_version == null || version != _version)
                return;

            _setting = setting;
        }

        /// <inheritdoc />
        /// <summary>
        /// Removes the setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns><c>true</c> if the setting is removed, <c>false</c> otherwise.</returns>
        public bool RemoveSetting(int version)
        {
            if (_version == null || version != _version)
                return false;

            _setting = null;
            return true;
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
