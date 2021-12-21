using Okta.Idx.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace embedded_auth_with_sdk.Models
{
    public class OktaVerifyEnrollmentModel
    {
        public OktaVerifyEnrollmentModel(AuthenticationResponse enrollResponse)
        {
            this.AuthenticationResponse = enrollResponse;
            this.RefreshInterval = enrollResponse.OktaVerifyEnrollOptions?.Refresh;
            this.StateHandle = enrollResponse.OktaVerifyEnrollOptions?.StateHandle;
            this.ViewModel = new EnrollOktaVerifyAuthenticatorViewModel
            {
                QrCodeHref = enrollResponse.CurrentAuthenticator.QrCode.Href,
                SelectedChannel = enrollResponse.CurrentAuthenticator.SelectedChannel,
                RefreshInterval = this.RefreshInterval,
            };            
        }

        protected AuthenticationResponse AuthenticationResponse { get; }

        public OktaVerifyEnrollOptions OktaVerifyEnrollOptions => AuthenticationResponse.OktaVerifyEnrollOptions;

        public int? RefreshInterval { get; set; }

        public string StateHandle { get; set; }

        public EnrollOktaVerifyAuthenticatorViewModel ViewModel { get; set; }

        public async Task<PollResponse> PollAsync()
        {
            return await OktaVerifyEnrollOptions.PollOnceAsync();
        }

        public async Task<AuthenticationResponse> SelectEnrollmentChannelAsync(string enrollmentChannel)
        {
            return await OktaVerifyEnrollOptions.SelectEnrollmentChannelAsync(enrollmentChannel);
        }

        public async Task<AuthenticationResponse> SendLinkToEmailAsync(string email)
        {
            return await OktaVerifyEnrollOptions.SendLinkToEmailAsync(email);
        }

        public async Task<AuthenticationResponse> SendLinkToPhoneNumberAsync(string phoneNumber)
        {
            return await OktaVerifyEnrollOptions.SendLinkToPhoneNumberAsync(phoneNumber);
        }
    }
}