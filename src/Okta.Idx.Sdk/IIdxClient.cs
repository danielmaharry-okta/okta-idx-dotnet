﻿// <copyright file="IIdxClient.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;
using Okta.Idx.Sdk.Configuration;
using Okta.Sdk.Abstractions;

namespace Okta.Idx.Sdk
{
    /// <summary>
    /// A client to interact with the IDX API.
    /// </summary>
    public interface IIdxClient : IOktaClient
    {
        /// <summary>
        /// Gets the client configuration.
        /// </summary>
        IdxConfiguration Configuration { get; }

        /// <summary>
        /// Changes user's password.
        /// </summary>
        /// <param name="changePasswordOptions">The change password options</param>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> ChangePasswordAsync(ChangePasswordOptions changePasswordOptions, IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Redeems an interaction code for tokens.
        /// </summary>
        /// <param name="idxContext">The IDX context that was returned by the `interact()` call</param>
        /// <param name="interactionCode">The interaction code.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Tokens</returns>
        Task<TokenResponse> RedeemInteractionCodeAsync(IIdxContext idxContext, string interactionCode, CancellationToken cancellationToken = default);

        /// <summary>
        /// Authenticates a user with username/password credentials
        /// </summary>
        /// <param name="authenticationOptions">The authentication options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="requestContext">The request context.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationOptions authenticationOptions, CancellationToken cancellationToken = default, RequestContext requestContext = null);

        /// <summary>
        /// Initiates the password recovery flow
        /// </summary>
        /// <param name="recoverPasswordOptions">The password recovery options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="requestContext">The request context.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> RecoverPasswordAsync(RecoverPasswordOptions recoverPasswordOptions, CancellationToken cancellationToken = default, RequestContext requestContext = null);

        /// <summary>
        /// Resend Code
        /// </summary>
        /// /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> ResendCodeAsync(IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verify an authenticator
        /// </summary>
        /// <param name="verifyAuthenticatorOptions">The options to verify an authenticator.</param>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> VerifyAuthenticatorAsync(VerifyAuthenticatorOptions verifyAuthenticatorOptions, IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Poll the status of a push request. This is only applicable to Okta Verify.
        /// </summary>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<PollResponse> PollAuthenticatorPushStatusAsync(IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verify an Okta Verify authenticator.
        /// </summary>
        /// <param name="verifyAuthenticatorOptions">The options to verify an Okta Verify authenticator.</param>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> VerifyAuthenticatorAsync(OktaVerifyVerifyAuthenticatorOptions verifyAuthenticatorOptions, IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Challenge a  web authn authenticator
        /// </summary>
        /// <param name="verifyAuthenticatorOptions">The options to verify an authenticator.</param>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> ChallengeAuthenticatorAsync(ChallengeWebAuthnAuthenticatorOptions verifyAuthenticatorOptions, IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Revoke tokens. Revoking an access token doesn't revoke the associated refresh token. However, revoking a refresh token does revoke the associated access token.
        /// </summary>
        /// <see href="https://developer.okta.com/docs/guides/revoke-tokens/revokeatrt/"/>
        /// <param name="tokenType">The token type.</param>
        /// <param name="token">The token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        Task RevokeTokensAsync(TokenType tokenType, string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="userProfile">The user profile. Contains all the dynamic properties required for registration.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="requestContext">The request context.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> RegisterAsync(UserProfile userProfile, CancellationToken cancellationToken = default, RequestContext requestContext = null);

        /// <summary>
        /// Select an authenticator to start the enrollment flow.
        /// </summary>
        /// <param name="enrollAuthenticatorOptions">The options for enrollment.</param>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> SelectEnrollAuthenticatorAsync(SelectEnrollAuthenticatorOptions enrollAuthenticatorOptions, IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Select Okta Verify channel to start the enrollment flow.
        /// </summary>
        /// <param name="enrollAuthenticatorOptions">The options for enrollment.</param>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> SelectEnrollAuthenticatorAsync(EnrollOktaVerifyAuthenticatorOptions enrollAuthenticatorOptions, IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Starts the password recovery process with the recovery authenticator.
        /// </summary>
        /// <param name="selectAuthenticatorOptions">The options to choose authenticator</param>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> SelectRecoveryAuthenticatorAsync(SelectAuthenticatorOptions selectAuthenticatorOptions, IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Trigger the phone authenticator enrollment flow.
        /// </summary>
        /// <param name="enrollAuthenticatorOptions">The options for phone enrollment.</param>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> EnrollAuthenticatorAsync(EnrollPhoneAuthenticatorOptions enrollAuthenticatorOptions, IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Enroll a Web Authn authenticator
        /// </summary>
        /// <param name="verifyAuthenticatorOptions">The options to enroll an authenticator.</param>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> EnrollAuthenticatorAsync(EnrollWebAuthnAuthenticatorOptions verifyAuthenticatorOptions, IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Enroll Okta Verify authenticator.
        /// </summary>
        /// <param name="verifyAuthenticatorOptions">The options to enroll an authenticator.</param>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> EnrollAuthenticatorAsync(EnrollOktaVerifyAuthenticatorOptions verifyAuthenticatorOptions, IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Select an authenticator to continue with the challenge process.
        /// </summary>
        /// <param name="selectAuthenticatorOptions">The options for authenticator selection.</param>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> SelectChallengeAuthenticatorAsync(SelectAuthenticatorOptions selectAuthenticatorOptions, IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Select phone to continue with the challenge process.
        /// </summary>
        /// <param name="selectAuthenticatorOptions">The options for authenticator selection.</param>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> SelectChallengeAuthenticatorAsync(SelectPhoneAuthenticatorOptions selectAuthenticatorOptions, IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Selects the Okta Verify authenticator for a challenge.
        /// </summary>
        /// <param name="selectAuthenticatorOptions">The options for selecting an authenticator challenge.</param>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> SelectChallengeAuthenticatorAsync(SelectOktaVerifyAuthenticatorOptions selectAuthenticatorOptions, IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Challenge a phone authenticator.
        /// </summary>
        /// <param name="challengeAuthenticatorOptions">The options for authenticator challenge.</param>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> ChallengeAuthenticatorAsync(ChallengePhoneAuthenticatorOptions challengeAuthenticatorOptions, IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Start an interaction to be completed by the sign-in widget.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="requestContext">The request context.</param>
        /// <returns>The widget sign in response.</returns>
        Task<WidgetSignInResponse> StartWidgetSignInAsync(CancellationToken cancellationToken = default, RequestContext requestContext = null);

        /// <summary>
        /// Skips optional authenticators
        /// </summary>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> SkipAuthenticatorSelectionAsync(IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets available identity providers.
        /// </summary>
        /// <param name="state">The state handle to use or null to autogenerate.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="requestContext">The request context.</param>
        /// <returns>The identity providers response.</returns>
        Task<IdentityProvidersResponse> GetIdentityProvidersAsync(string state = null, CancellationToken cancellationToken = default, RequestContext requestContext = null);

        /// <summary>
        /// Gets available identity providers.
        /// </summary>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The identity providers response.</returns>
        Task<IdentityProvidersResponse> GetIdentityProvidersAsync(IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Poll the status of an Okta Verify enrollment.
        /// </summary>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<PollResponse> PollAuthenticatorEnrollmentStatusAsync(IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Determines if password is required.
        /// </summary>
        /// <param name="state">The state handle to use or null to autogenerate.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="requestContext">The request context.</param>
        /// <returns>The password required response.</returns>
        Task<PasswordRequiredResponse> CheckIsPasswordRequiredAsync(string state = null, CancellationToken cancellationToken = default, RequestContext requestContext = null);

        /// <summary>
        /// Determines if password is required.
        /// </summary>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The password required response.</returns>
        Task<PasswordRequiredResponse> CheckIsPasswordRequiredAsync(IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Enroll a security question.
        /// </summary>
        /// <param name="verifyAuthenticatorOptions">The options to verify a security question.</param>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> EnrollAuthenticatorAsync(EnrollSecurityQuestionAuthenticatorOptions verifyAuthenticatorOptions, IIdxContext idxContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verify a security question.
        /// </summary>
        /// <param name="verifyAuthenticatorOptions">The options to verify a security question.</param>
        /// <param name="idxContext">The IDX context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The authentication response.</returns>
        Task<AuthenticationResponse> VerifyAuthenticatorAsync(VerifySecurityQuestionAuthenticatorOptions verifyAuthenticatorOptions, IIdxContext idxContext, CancellationToken cancellationToken = default);
    }
}
