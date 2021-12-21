// <copyright file="RemediationOptionParameter.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Idx.Sdk
{
    /// <summary>
    /// Represents a parameter for a remediation.
    /// </summary>
    public class RemediationOptionParameter : Resource
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name => GetProperty<string>("name");

        /// <summary>
        /// Gets the label.
        /// </summary>
        public string Label => GetStringProperty("label");

        /// <summary>
        /// Gets the value.
        /// </summary>
        public string Value => GetStringProperty("value");

        public bool? IsRequired => GetBooleanProperty("required");
    }
}
