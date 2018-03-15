// ***********************************************************************
// Assembly         : DemoService.Infrastructure.WebAPI
// Author           : Milo.Wical
// Created          : 03-11-2018
//
// Last Modified By : Milo.Wical
// Last Modified On : 03-11-2018
// ***********************************************************************
// <copyright file="VersionedSetting.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoService.Infrastructure.WebAPI.Models
{
    /// <summary>
    /// Class VersionedSetting.
    /// </summary>
    public class VersionedSetting
    {
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the setting.
        /// </summary>
        /// <value>The setting.</value>
        public string Setting { get; set; }
    }
}