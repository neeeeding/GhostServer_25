using System.Threading.Tasks;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace _01Script.Networking
{
    
    public enum UGSAuthState
    {
        NotAuthenticated,
        Authenticating,
        Authenticated,
        Error,
        Timeout
    }
    public class UGSAuthWrapper : MonoBehaviour
    {
        public static UGSAuthState AuthState {get; private set;} =  UGSAuthState.NotAuthenticated;

        public static async Task<UGSAuthState> DoAuthAsync(int maxTryCount = 4)
        {
            if (AuthState == UGSAuthState.Authenticated)
            {
                return AuthState;
            }
            
            AuthState = UGSAuthState.Authenticating;

            int tryCount = 0;

            while (AuthState == UGSAuthState.Authenticating && tryCount < maxTryCount)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

                if (AuthenticationService.Instance.IsSignedIn && AuthenticationService.Instance.IsAuthorized)
                {
                    AuthState = UGSAuthState.Authenticated;
                    break;
                }
                
                tryCount++;
                await Task.Delay(1000);
            }
            
            return AuthState;
        }
    }
}