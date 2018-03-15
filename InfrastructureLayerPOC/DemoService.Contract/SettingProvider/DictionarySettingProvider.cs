// ***********************************************************************
// Assembly         : DemoService.Contract
// Author           : Milo.Wical
// Created          : 02-25-2018
//
// Last Modified By : Milo.Wical
// Last Modified On : 02-25-2018
// ***********************************************************************
// <copyright file="DictionarySettingProvider.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace DemoService.Contract.SettingProvider
{
    using System.Collections.Generic;
    using System.Linq;

    /// <inheritdoc />
    /// <summary>
    /// Class DictionarySettingProvider.
    /// </summary>
    /// <seealso cref="T:DemoService.Contract.SettingProvider.ISettingProvider" />
    public class DictionarySettingProvider : ISettingProvider
    {
        /// <summary>
        /// The version setting dictionary
        /// </summary>
        private readonly Dictionary<int, string> _versionSettingDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionarySettingProvider"/> class.
        /// </summary>
        public DictionarySettingProvider()
        {
            _versionSettingDictionary = new Dictionary<int, string>();
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:DemoService.Contract.SettingProvider.DictionarySettingProvider" /> class.
        /// </summary>
        /// <param name="seedVersion">The seed version.</param>
        /// <param name="seedSetting">The seed setting.</param>
        public DictionarySettingProvider(int seedVersion, string seedSetting) : this()
        {
            _versionSettingDictionary.Add(seedVersion, seedSetting);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the <see cref="T:System.String" /> with the specified version.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>System.String.</returns>
        public string this[int version]
        {
            get => _versionSettingDictionary[version];
            private set => _versionSettingDictionary[version] = value;
        }


        /// <summary>
        /// Adds the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void AddSetting(string setting)
        {
            var maxValue = _versionSettingDictionary.Any() ? _versionSettingDictionary.Keys.Max() : 0;

            _versionSettingDictionary.Add(maxValue + 1, setting);
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates the setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="setting">The setting.</param>
        public void UpdateSetting(int version, string setting)
        {
            this[version] = setting;
        }

        /// <inheritdoc />
        /// <summary>
        /// Removes the setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool RemoveSetting(int version)
        {
            return _versionSettingDictionary.Remove(version);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the latest setting.
        /// </summary>
        /// <returns>System.String.</returns>
        /// <exception cref="T:System.NotImplementedException"></exception>
        public string GetLatestSetting()
        {
            if(_versionSettingDictionary.Any())
                return this[_versionSettingDictionary.Keys.Max()];

            return null;
        }
    }
}
