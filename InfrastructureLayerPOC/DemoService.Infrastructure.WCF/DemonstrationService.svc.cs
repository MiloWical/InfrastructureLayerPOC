// ***********************************************************************
// Assembly         : DemoService.WCF
// Author           : Milo.Wical
// Created          : 02-24-2018
//
// Last Modified By : Milo.Wical
// Last Modified On : 03-11-2018
// ***********************************************************************
// <copyright file="DemonstrationService.svc.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace DemoService.WCF
{
    using Contract;
    using DemoService.Contract;
    using Microsoft.Practices.Unity.Configuration;
    using Unity;

    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    /// <summary>
    /// Class DemonstrationService.
    /// </summary>
    /// <seealso cref="DemoService.WCF.Contract.IDemonstrationServiceWcfContract" />
    public class DemonstrationService : IDemonstrationServiceWcfContract
    {
        /// <summary>
        /// The Unity container
        /// </summary>
        private static IUnityContainer _unityContainer;

        /// <summary>
        /// The demonstration service implementation
        /// </summary>
        private readonly IDemonstrationService _demonstrationServiceImplementation;

        /// <summary>
        /// Initializes a new instance of the <see cref="DemonstrationService"/> class.
        /// </summary>
        public DemonstrationService()
        {
            //Bootstrap from Unity
            if (_unityContainer == null)
                _unityContainer = new UnityContainer().LoadConfiguration(); //Uses the default Unity section "unity" (c.f. https://msdn.microsoft.com/en-us/library/ff660935(v=pandp.20).aspx)

            _demonstrationServiceImplementation = _unityContainer.Resolve<IDemonstrationService>("demonstrationServiceImpl");
        }

        /// <summary>
        /// Gets the versioned setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>System.String.</returns>
        public string GetVersionedWcfSetting(int version)
        {
            return _demonstrationServiceImplementation.GetVersionedSetting(version);
        }

        /// <summary>
        /// Gets the latest versioned setting.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetLatestVersionedWcfSetting()
        {
            return _demonstrationServiceImplementation.GetLatestVersionedSetting();
        }

        /// <summary>
        /// Updates the versioned setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="setting">The setting.</param>
        public void UpdateVersionedWcfSetting(int version, string setting)
        {
            _demonstrationServiceImplementation.UpdateVersionedSetting(version, setting);
        }

        /// <summary>
        /// Adds the versioned setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void AddVersionedWcfSetting(string setting)
        {
            _demonstrationServiceImplementation.AddVersionedSetting(setting);
        }

        /// <summary>
        /// Removes the versioned setting.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns><c>true</c> if the setting is removed, <c>false</c> otherwise.</returns>
        public bool RemoveVersionedWcfSetting(int version)
        {
            return _demonstrationServiceImplementation.RemoveVersionedSetting(version);
        }
    }
}
