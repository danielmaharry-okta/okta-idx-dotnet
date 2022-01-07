using Okta.Idx.Sdk.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Idx.Sdk.OktaVerify
{
    public class OktaVerifyAuthenticationOptions
    {
        public OktaVerifyAuthenticationOptions(AuthenticationResponse authenticationResponse, IIdxResponse idxResponse)
        {
            this.AuthenticationResponse = authenticationResponse;
            this.StateHandle = idxResponse.StateHandle;
            this.AuthenticatorVerificationDataRemediationOption = idxResponse.FindRemediationOption(RemediationType.AuthenticatorVerificationData);
            this.SelectAuthenticatorAuthenticateRemediationOption = idxResponse.FindRemediationOption(RemediationType.SelectAuthenticatorAuthenticate);
            this.CurrentAuthenticator = idxResponse.GetProperty<OktaVerifyAuthenticatorValue>("currentAuthenticator");
        }

        protected internal AuthenticationResponse AuthenticationResponse { get; set; }

        protected IdxContext IdxContext { get; }

        public string StateHandle { get; set; }

        protected IRemediationOption AuthenticatorVerificationDataRemediationOption { get; }

        protected IRemediationOption SelectAuthenticatorAuthenticateRemediationOption { get; }

        protected IRemediationOption ChallengeAuthenticatorRemediationOption { get; private set; }

        protected internal IAuthenticatorValue CurrentAuthenticator { get; }

        public async Task<AuthenticationResponse> SelectOktaVerifyMethod(string methodType)
        {
            IdxRequestPayload idxRequestPayload = new IdxRequestPayload();
            idxRequestPayload.SetProperty("authenticator", new { id = this.CurrentAuthenticator.Id, methodType = methodType });
            idxRequestPayload.SetProperty("stateHandle", StateHandle);

            var selectAuthenticatorResponse = await SelectAuthenticatorAuthenticateRemediationOption.ProceedAsync(idxRequestPayload);
            this.ChallengeAuthenticatorRemediationOption = selectAuthenticatorResponse.FindRemediationOption(RemediationType.ChallengeAuthenticator);

            var authenticationResponse = new AuthenticationResponse
            {
                IdxContext = IdxContext,
                AuthenticationStatus = AuthenticationResponse.AuthenticationStatus,
                CurrentAuthenticator = IdxResponseHelper.ConvertToAuthenticator(selectAuthenticatorResponse.Authenticators.Value, selectAuthenticatorResponse.CurrentAuthenticator.Value),
            };

            return authenticationResponse;
        }
    }
}
