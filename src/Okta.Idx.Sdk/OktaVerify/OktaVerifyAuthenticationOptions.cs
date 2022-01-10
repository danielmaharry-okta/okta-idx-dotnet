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
            this.IdxContext = authenticationResponse.IdxContext;
            this.StateHandle = idxResponse.StateHandle;
            this.SelectAuthenticatorAuthenticateRemediationOption = idxResponse.FindRemediationOption(RemediationType.SelectAuthenticatorAuthenticate);
            this.CurrentAuthenticator = idxResponse.GetProperty<IIdxResponse>("currentAuthenticator").GetProperty<AuthenticatorValue>("value");
        }

        protected internal AuthenticationResponse AuthenticationResponse { get; set; }

        protected IIdxContext IdxContext { get; }

        public string StateHandle { get; set; }

        protected IRemediationOption SelectAuthenticatorAuthenticateRemediationOption { get; }

        protected IRemediationOption ChallengePollRemediationOption { get; private set; }

        protected IRemediationOption ChallengeAuthenticatorRemedationOption { get; private set; }

        protected internal IAuthenticatorValue CurrentAuthenticator { get; }

        protected async Task<AuthenticationResponse> SelectOktaVerifyMethodAsync(string methodType)
        {
            IdxRequestPayload idxRequestPayload = new IdxRequestPayload();
            idxRequestPayload.SetProperty("authenticator", new { id = this.CurrentAuthenticator.Id, methodType = methodType });
            idxRequestPayload.SetProperty("stateHandle", StateHandle);

            var selectAuthenticatorResponse = await SelectAuthenticatorAuthenticateRemediationOption.ProceedAsync(idxRequestPayload);
            this.ChallengePollRemediationOption = selectAuthenticatorResponse.FindRemediationOption(RemediationType.ChallengePoll);
            this.ChallengeAuthenticatorRemedationOption = selectAuthenticatorResponse.FindRemediationOption(RemediationType.ChallengeAuthenticator);

            var authenticationResponse = new AuthenticationResponse
            {
                IdxContext = IdxContext,
                AuthenticationStatus = AuthenticationResponse.AuthenticationStatus,
                CurrentAuthenticator = IdxResponseHelper.ConvertToAuthenticator(selectAuthenticatorResponse.Authenticators.Value, selectAuthenticatorResponse.CurrentAuthenticator.Value),
            };

            return authenticationResponse;
        }

        public ITokenResponse TokenInfo { get; internal set; }

        public async Task<AuthenticationResponse> SelectTotpMethodAsync()
        {
            return await SelectOktaVerifyMethodAsync("totp");
        }

        public async Task<AuthenticationResponse> SelectPushMethodAsync()
        {
            return await SelectOktaVerifyMethodAsync("push");
        }

        public async Task<AuthenticationResponse> EnterCodeAsync(string code)
        {
            if (ChallengeAuthenticatorRemedationOption.Name != RemediationType.ChallengeAuthenticator)
            {
                throw new ArgumentException($"Expected remediation option of type '{RemediationType.ChallengeAuthenticator}', the specified remediation option is of type {ChallengeAuthenticatorRemedationOption.Name}.");
            }

            IdxRequestPayload requestPayload = new IdxRequestPayload
            {
                StateHandle = StateHandle,
            };
            requestPayload.SetProperty("credentials", new { totp = code });
            try
            {
                var challengeResponse = await ChallengeAuthenticatorRemedationOption.ProceedAsync(requestPayload);
                TokenInfo = await challengeResponse.SuccessWithInteractionCode.ExchangeCodeAsync(IdxContext);
                return new AuthenticationResponse
                {
                    AuthenticationStatus = AuthenticationStatus.Success,
                    TokenInfo = TokenInfo,
                };
            }
            catch (Exception ex)
            {
                return new AuthenticationResponse
                {
                    AuthenticationStatus = AuthenticationStatus.Exception,
                    MessageToUser = ex.Message,
                };
            }
        }

        public async Task<PollResponse> PollOnceAsync()
        {
            if (ChallengePollRemediationOption.Name != RemediationType.ChallengePoll)
            {
                throw new ArgumentException($"Expected remediation option of type '{RemediationType.ChallengePoll}', the specified remediation option is of type {ChallengePollRemediationOption.Name}.");
            }

            IdxRequestPayload requestPayload = new IdxRequestPayload
            {
                StateHandle = StateHandle,
            };

            var challengeResponse = await ChallengePollRemediationOption.ProceedAsync(requestPayload);
            bool continuePolling = challengeResponse.ContainsRemediationOption(RemediationType.ChallengePoll, out IRemediationOption challengePollRemediationOption);
            if (continuePolling == false)
            {
                this.TokenInfo = await challengeResponse.SuccessWithInteractionCode.ExchangeCodeAsync(IdxContext);
            }

            return new PollResponse
            {
                Refresh = challengePollRemediationOption?.Refresh,
                ContinuePolling = continuePolling,
            };
        }
    }
}
