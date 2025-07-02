using IdentityModel.OidcClient;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Microsoft.Maui.Storage;

namespace Acme.BookStore.MauiClient.Auth
{
    public class AuthService : ITransientDependency
    {
        private readonly OidcClient _oidcClient;
        private string _accessToken;

        public AuthService(OidcClient oidcClient)
        {
            _oidcClient = oidcClient;
        }

        public async Task<bool> LoginAsync()
        {
            try
            {
                var loginResult = await _oidcClient.LoginAsync(new LoginRequest());
                if (loginResult.IsError)
                {
                    // TODO: Log error (loginResult.Error)
                    return false;
                }

                _accessToken = loginResult.AccessToken;
                await SecureStorage.Default.SetAsync(OidcConsts.AccessTokenKeyName, _accessToken);
                // TODO: Store refresh token if available and handle token refresh logic

                return true;
            }
            catch (System.Exception ex)
            {
                // TODO: Log exception
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            // TODO: Implement OIDC logout if endpoint is available
            _accessToken = null;
            SecureStorage.Default.Remove(OidcConsts.AccessTokenKeyName);
            SecureStorage.Default.Remove(OidcConsts.RefreshTokenKeyName); // If you store refresh token

            // TODO: await _oidcClient.LogoutAsync(new LogoutRequest { IdTokenHint = idToken });
            // This requires the IdToken and a proper OIDC logout endpoint.
            // For now, we are just clearing local tokens.
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                _accessToken = await SecureStorage.Default.GetAsync(OidcConsts.AccessTokenKeyName);
            }
            // TODO: Implement token refresh logic if access token is expired
            return _accessToken;
        }

        public async Task<bool> IsUserLoggedInAsync()
        {
            var token = await GetAccessTokenAsync();
            return !string.IsNullOrEmpty(token);
            // TODO: Add more robust check, e.g., token validation or calling a user info endpoint
        }
    }
}
