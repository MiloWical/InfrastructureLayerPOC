// ***********************************************************************
// Assembly         : DemoService.Contract
// Author           : Milo.Wical
// Created          : 02-24-2018
//
// Last Modified By : Milo.Wical
// Last Modified On : 02-25-2018
// ***********************************************************************
// <copyright file="IDemonstrationService.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoService.Contract
{
    /// <summary>
    /// Interface IDemonstrationService
    /// </summary>
    public interface IDemonstrationService
    {
        /// <summary>
        /// Gets the versioned setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>System.String.</returns>
        string GetVersionedSetting(int version);

        /// <summary>
        /// Gets the latest versioned setting.
        /// </summary>
        /// <returns>System.String.</returns>
        string GetLatestVersionedSetting();

        /// <summary>
        /// Updates the versioned setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="setting">The setting.</param>
        void UpdateVersionedSetting(int version, string setting);

        /// <summary>
        /// Adds the versioned setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        void AddVersionedSetting(string setting);

        /// <summary>
        /// Removes the versioned setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns><c>true</c> if the setting is removed, <c>false</c> otherwise.</returns>
        bool RemoveVersionedSetting(int version);
    }
}
