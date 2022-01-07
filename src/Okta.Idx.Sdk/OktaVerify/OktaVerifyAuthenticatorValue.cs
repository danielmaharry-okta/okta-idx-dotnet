using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Idx.Sdk.OktaVerify
{
    public class OktaVerifyAuthenticatorValue : Resource, IAuthenticatorValue
    {
        protected IAuthenticatorValue AuthenticatorValue => GetProperty<IAuthenticatorValue>("value");

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName => AuthenticatorValue?.DisplayName;

        /// <summary>
        /// Gets the Id.
        /// </summary>
        public string Id => AuthenticatorValue?.Id;

        /// <summary>
        /// Gets the key.
        /// </summary>
        public string Key => AuthenticatorValue?.Key;

        public IRemediationOption Resend => AuthenticatorValue?.GetProperty<IRemediationOption>("resend");

        /// <summary>
        /// Gets the methods.
        /// </summary>
        public IList<IAuthenticatorMethod> Methods => AuthenticatorValue?.Methods;
    }
}
