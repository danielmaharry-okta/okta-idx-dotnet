// <copyright file="OktaVerifyEnrollmentChannel.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Sdk.Abstractions;

namespace Okta.Idx.Sdk.OktaVerify
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "N/A")]
    public sealed class OktaVerifyEnrollmentChannel : StringEnum
    {
        public static OktaVerifyEnrollmentChannel QrCode = new OktaVerifyEnrollmentChannel("qrcode");

        public static OktaVerifyEnrollmentChannel Email = new OktaVerifyEnrollmentChannel("email");

        public static OktaVerifyEnrollmentChannel Sms = new OktaVerifyEnrollmentChannel("sms");

        public OktaVerifyEnrollmentChannel(string value) : base(value)
        {
        }
    }
}
