// ***********************************************************************
// Assembly         : DemoService.WCF
// Author           : Milo.Wical
// Created          : 03-10-2018
//
// Last Modified By : Milo.Wical
// Last Modified On : 03-11-2018
// ***********************************************************************
// <copyright file="IDemonstrationServiceWcfContract.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace DemoService.WCF.Contract
{
    using System.ServiceModel;

    /// <summary>
    /// Interface IDemonstrationServiceWcfContract
    /// </summary>
    [ServiceContract]
    public interface IDemonstrationServiceWcfContract
    {
        /// <summary>
        /// Gets the versioned setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string GetVersionedWcfSetting(int version);

        /// <summary>
        /// Gets the latest versioned setting.
        /// </summary>
        /// <returns>System.String.</returns>
        [OperationContract]
        string GetLatestVersionedWcfSetting();

        /// <summary>
        /// Updates the versioned setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="setting">The setting.</param>
        [OperationContract]
        void UpdateVersionedWcfSetting(int version, string setting);

        /// <summary>
        /// Adds the versioned setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        [OperationContract]
        void AddVersionedWcfSetting(string setting);

        /// <summary>
        /// Removes the versioned setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns><c>true</c> if the setting is removed, <c>false</c> otherwise.</returns>
        [OperationContract]
        bool RemoveVersionedWcfSetting(int version);
    }
}