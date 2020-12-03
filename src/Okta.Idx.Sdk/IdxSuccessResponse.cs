﻿using Okta.Sdk.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Okta.Idx.Sdk
{
    public class IdxSuccessResponse : Resource, IIdxSuccessResponse
    {
        public IList<string> Rel => GetArrayProperty<string>("rel");

        public string Name => GetStringProperty("name");

        public string Method => GetStringProperty("method");

        public string Href => GetStringProperty("href");

        public string Accepts => GetStringProperty("accepts");

        private string GetInteractionCode()
        {
            var successWithInteractionCodeFormValues = this.GetArrayProperty<FormValue>("value");

            var interactionCodeFormValue = successWithInteractionCodeFormValues.FirstOrDefault(x => x.Name == "interaction_code");

            return interactionCodeFormValue.GetProperty<string>("value");
        }

        public async Task<ITokenResponse> ExchangeCodeAsync(CancellationToken cancellationToken = default)
        {
            var client = GetClient();
            var payload = new Dictionary<string, string>();
            payload.Add("interaction_code", GetInteractionCode());
            payload.Add("grant_type", "interaction_code");


            // Add PKCE params
            payload.Add("code_verifier", client.Context.CodeVerifier);
            payload.Add("client_id", client.Configuration.ClientId);

            if (client.Configuration.IsConfidentialClient)
            {
                payload.Add("client_secret", client.Configuration.ClientSecret);
            }

            var headers = new Dictionary<string, string>();
            headers.Add("Content-Type", HttpRequestContentBuilder.ContentTypeFormUrlEncoded);

            // TODO: Use href when https://oktainc.atlassian.net/browse/OKTA-349894 is fixed

            var uri = $"{UrlHelper.EnsureTrailingSlash(client.Configuration.Issuer)}v1/token";
            var request = new HttpRequest
            {
                Uri = uri,
                Payload = payload,
                Headers = headers,
            };

            var httpVerb = (HttpVerb)Enum.Parse(typeof(HttpVerb), Method, true);

            return await client.SendAsync<TokenResponse>(request, httpVerb, cancellationToken).ConfigureAwait(false);
        }
    }
}
