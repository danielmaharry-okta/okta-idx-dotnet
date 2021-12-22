﻿using Okta.Idx.Sdk.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Idx.Sdk
{
    public class OktaVerifyEnrollOptions
    {
        public OktaVerifyEnrollOptions()
        {
        }

        public OktaVerifyEnrollOptions(AuthenticationResponse authenticationResponse, IIdxResponse idxResponse)
        {
            this.AuthenticationResponse = authenticationResponse;
            this.IdxContext = authenticationResponse.IdxContext;
            this.StateHandle = idxResponse.StateHandle;
            this.EnrollPollRemediationOption = idxResponse.FindRemediationOption(RemediationType.EnrollPoll);
            this.SelectEnrollmentChannelRemediationOption = idxResponse.FindRemediationOption(RemediationType.SelectEnrollmentChannel);
        }

        protected internal AuthenticationResponse AuthenticationResponse { get; set; }

        protected internal IIdxContext IdxContext { get; set; }

        /// <summary>
        /// Gets the enroll-poll remediation option.
        /// </summary>
        protected internal IRemediationOption EnrollPollRemediationOption { get; }

        /// <summary>
        /// Gets the select-enrollment-channel remediation option.
        /// </summary>
        protected internal IRemediationOption SelectEnrollmentChannelRemediationOption { get; }

        /// <summary>
        /// Gets the enrollment-channel-data remediation option.
        /// </summary>
        protected internal IRemediationOption EnrollmentChannelDataRemediationOption { get; private set; }

        /// <summary>
        /// Gets or sets the state handle.
        /// </summary>
        public string StateHandle { get; set; }

        /// <summary>
        /// Gets a value indicating how long to wait before the next call.
        /// </summary>
        public int? Refresh { get => EnrollPollRemediationOption.Refresh; }

        public string QrCode { get => AuthenticationResponse?.CurrentAuthenticator?.QrCode?.Href; }

        public string AuthenticatorId
        {
            get
            {
                return SelectEnrollmentChannelRemediationOption?
                  .GetArrayProperty<IIdxResponse>("value")[0]
                  .GetProperty<IIdxResponse>("value")
                  .GetProperty<IIdxResponse>("form")
                  .GetArrayProperty<IIdxResponse>("value")[0]
                  .GetProperty<string>("value");
            }
        }

        public async Task<AuthenticationResponse> SendLinkToEmailAsync(string email)
        {
            IdxRequestPayload idxRequestPayload = new IdxRequestPayload
            {
                StateHandle = StateHandle,
            };
            idxRequestPayload.SetProperty("email", email);

            return await SendEnrollmentChannelDataAsync(idxRequestPayload);
        }

        public async Task<AuthenticationResponse> SendLinkToPhoneNumberAsync(string phoneNumber)
        {
            IdxRequestPayload idxRequestPayload = new IdxRequestPayload
            {
                StateHandle = StateHandle,
            };
            idxRequestPayload.SetProperty("phoneNumber", phoneNumber);

            return await SendEnrollmentChannelDataAsync(idxRequestPayload);
        }

        protected async Task<AuthenticationResponse> SendEnrollmentChannelDataAsync(IdxRequestPayload idxRequestPayload)
        {
            var sendEnrollmentDataResponse = await EnrollmentChannelDataRemediationOption.ProceedAsync(idxRequestPayload);

            var authenticationResponse = new AuthenticationResponse
            {
                IdxContext = IdxContext,
                AuthenticationStatus = AuthenticationResponse.AuthenticationStatus,
                CurrentAuthenticator = IdxResponseHelper.ConvertToAuthenticator(sendEnrollmentDataResponse.Authenticators.Value, sendEnrollmentDataResponse.CurrentAuthenticator.Value),
            };

            return authenticationResponse;
        }

        public async Task<AuthenticationResponse> SelectEnrollmentChannelAsync(string enrollmentChannelName)
        {
            ValidateEnrollmentChannel(enrollmentChannelName);

            IdxRequestPayload idxRequestPayload = new IdxRequestPayload
            {
                StateHandle = StateHandle,
            };
            idxRequestPayload.SetProperty("authenticator", new { channel = enrollmentChannelName, id = AuthenticatorId });
            idxRequestPayload.SetProperty("stateHandle", StateHandle);

            var selectEnrollmentChannelResponse = await SelectEnrollmentChannelRemediationOption.ProceedAsync(idxRequestPayload);

            var authenticationResponse = new AuthenticationResponse
            {
                IdxContext = IdxContext,
                AuthenticationStatus = AuthenticationResponse.AuthenticationStatus,
                CurrentAuthenticator = IdxResponseHelper.ConvertToAuthenticator(selectEnrollmentChannelResponse.Authenticators.Value, selectEnrollmentChannelResponse.CurrentAuthenticator.Value),
            };

            EnrollmentChannelDataRemediationOption = selectEnrollmentChannelResponse.FindRemediationOption(RemediationType.EnrollmentChannelData);

            return authenticationResponse;
        }

        /// <summary>
        /// Throws an InvalidEnrollmentChannelException if the specified channel is not valid.
        /// </summary>
        /// <param name="enrollmentChannelName">The name of the enrollment channel to validate.</param>
        /// <exception cref="InvalidEnrollmentChannelException">The exception thrown if the specified channel is not valid.</exception>
        protected void ValidateEnrollmentChannel(string enrollmentChannelName)
        {
            IList<RemediationOptionParameter> channelOptions = GetChannelOptions();
            if (!channelOptions.Any(rop => rop.Value == enrollmentChannelName))
            {
                throw new InvalidEnrollmentChannelException(enrollmentChannelName, channelOptions.Select(co => co.Value).ToArray());
            }
        }

        public IList<RemediationOptionParameter> GetChannelOptions()
        {
            return SelectEnrollmentChannelRemediationOption?
                .GetArrayProperty<IIdxResponse>("value")[0]
                .GetProperty<IIdxResponse>("value")
                .GetProperty<IIdxResponse>("form")
                .GetArrayProperty<IIdxResponse>("value")[1]
                .GetArrayProperty<RemediationOptionParameter>("options");
        }

        public async Task<PollResponse> PollOnceAsync()
        {
            IRemediationOption remediationOption = EnrollPollRemediationOption;
            if (remediationOption.Name != RemediationType.EnrollPoll)
            {
                throw new ArgumentException(string.Format("Expected remediation option of type '{0}', the specified remediation option is of type {1}", RemediationType.EnrollPoll, remediationOption.Name));
            }

            IdxRequestPayload requestPayload = new IdxRequestPayload
            {
                StateHandle = this.StateHandle,
            };

            var idxResponse = await remediationOption.ProceedAsync(requestPayload);
            bool continuePolling = idxResponse.ContainsRemediationOption(RemediationType.EnrollPoll, out IRemediationOption enrollPollRemediationOption);

            return new PollResponse
            {
                Refresh = enrollPollRemediationOption?.Refresh,
                ContinuePolling = continuePolling,
            };
        }
    }
}
