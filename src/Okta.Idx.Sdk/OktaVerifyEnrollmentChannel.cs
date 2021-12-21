using Okta.Sdk.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Idx.Sdk
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
