using Okta.Idx.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace embedded_auth_with_sdk.Models
{
    public class OktaVerifyEnrollModel
    {
        public OktaVerifyEnrollModel(OktaVerifyEnrollOptions oktaVerifyEnrollOptions, string pollEndpoint = "/OktaVerify/Poll")
        {   
            this.OktaVerifyEnrollOptions = oktaVerifyEnrollOptions;
            this.QrCode = oktaVerifyEnrollOptions.QrCode;
            this.PollEndpoint = pollEndpoint;
            this.RefreshInterval = oktaVerifyEnrollOptions.Refresh;
            this.Message = "When prompted, tap Scan a QR code, then scan the QR code below:";
        }

        public OktaVerifyEnrollOptions OktaVerifyEnrollOptions { get; }

        public string QrCode { get; set; }

        public string PollEndpoint { get; set; }

        public int? RefreshInterval { get; set; }

        public string Message { get; set; }
    }
}