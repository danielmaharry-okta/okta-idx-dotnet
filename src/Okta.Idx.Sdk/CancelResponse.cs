﻿// <copyright file="CancelResponse.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace Okta.Idx.Sdk
{
    public class CancelResponse : Resource, ICancelResponse
    {
        public IList<string> Rel => GetArrayProperty<string>("rel");

        public string Name => GetStringProperty("name");

        public string Method => GetStringProperty("method");

        public string Href => GetStringProperty("href");

        public string Accepts => GetStringProperty("accepts");
    }
}
