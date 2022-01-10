using Okta.Idx.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace embedded_auth_with_sdk.Models
{
    public class OktaVerifySelectEnrollmentChannelOptionModel
    {
        static OktaVerifySelectEnrollmentChannelOptionModel()
        {
            LabelTexts = new Dictionary<string, string>
            {
                { "qrcode", "Scan a QR code" },
                { "email", "Email me a setup link" },
                { "sms", "Text me a setup link" }
            };
        }
        public static Dictionary<string, string> LabelTexts { get; }

        public OktaVerifySelectEnrollmentChannelOptionModel()
        { 
        }

        public OktaVerifySelectEnrollmentChannelOptionModel(RemediationOptionParameter remediationOptionParameter)
        {
            this.Value = remediationOptionParameter?.Value;
        }

        public string LabelText => LabelTexts[Value];

        public string Value { get; }

        public string Name => "SelectedChannel";

    }
}
