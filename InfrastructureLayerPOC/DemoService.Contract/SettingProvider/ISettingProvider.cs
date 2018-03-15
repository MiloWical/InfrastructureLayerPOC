// ***********************************************************************
// Assembly         : DemoService.Contract
// Author           : Milo.Wical
// Created          : 02-25-2018
//
// Last Modified By : Milo.Wical
// Last Modified On : 02-25-2018
// ***********************************************************************
// <copyright file="ISettingProvider.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace DemoService.Contract.SettingProvider
{
    /// <summary>
    /// Interface ISettingProvider
    /// </summary>
    public interface ISettingProvider
    {
        /// <summary>
        /// Gets the <see cref="System.String"/> with the specified version.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>System.String.</returns>
        string this[int version] { get; }

        /// <summary>
        /// Adds the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        void AddSetting(string setting);

        /// <summary>
        /// Updates the setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="setting">The setting.</param>
        void UpdateSetting(int version, string setting);

        /// <summary>
        /// Removes the setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns><c>true</c> if the version is removed, <c>false</c> otherwise.</returns>
        bool RemoveSetting(int version);

        /// <summary>
        /// Gets the latest setting.
        /// </summary>
        /// <returns>System.String.</returns>
        string GetLatestSetting();
    }
}
