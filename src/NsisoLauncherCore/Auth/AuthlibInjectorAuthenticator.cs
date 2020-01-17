using System;
using System.IO;
using System.Threading.Tasks;
using NsisoLauncherCore.Net.MojangApi.Api;
using NsisoLauncherCore.Net.MojangApi.Endpoints;
using NsisoLauncherCore.Net.MojangApi.Responses;
using NsisoLauncherCore.Util;

namespace NsisoLauncherCore.Auth
{
    public class AuthlibInjectorAuthenticator : YggdrasilAuthenticator
    {
        public string ServerRootAddress { get; set; }
        public bool CheckMd5 { get; set; }
        public AuthlibInjectorAuthenticator(string serverRootAddr, Credentials credentials, bool checkMd5 = false) : base(credentials)
        {
            this.ServerRootAddress = serverRootAddr;
            this.ProxyAuthServerAddress = ServerRootAddress + "/authserver";
            CheckMd5 = checkMd5;
        }

        public override async Task<AuthenticateResult> DoAuthenticateAsync()
        {
            try
            {
                Authenticate authenticate = new Authenticate(Credentials);
                if (CheckMd5)
                {
                    string modsDir = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, ".minecraft", "mods");
                    string modsMd5 = FileHelper.GetDirectoryMd5(modsDir);
                    authenticate.Md5List = modsMd5;
                }
                if (ProxyAuthServerAddress != null)
                {
                    authenticate.Address = new Uri(ProxyAuthServerAddress + "/authenticate");
                }
                if (AuthArgs != null && AuthArgs.Count != 0)
                {
                    authenticate.Arguments = AuthArgs;
                }
                var result = await authenticate.PerformRequestAsync();
                if (result.IsSuccess)
                {
                    return new AuthenticateResult(AuthState.SUCCESS)
                    {
                        AccessToken = result.AccessToken,
                        SelectedProfileUUID = result.SelectedProfile,
                        UserData = result.User,
                        Profiles = result.AvailableProfiles
                    };
                }
                else
                {
                    AuthState errState;

                    if (result.Code == System.Net.HttpStatusCode.Forbidden)
                    { errState = AuthState.ERR_INVALID_CRDL; }
                    else if (result.Code == System.Net.HttpStatusCode.NotFound)
                    { errState = AuthState.ERR_NOTFOUND; }
                    else if(result.Error.ErrorTag == "md5")
                    {
                        errState = AuthState.ERR_MD5CHECK;
                    }
                    else
                    { errState = AuthState.ERR_OTHER; }

                    return new AuthenticateResult(errState) { Error = result.Error };
                }
            }
            catch (Exception ex)
            {
                return new AuthenticateResult(AuthState.ERR_INSIDE) { Error = new Net.MojangApi.Error() { ErrorMessage = ex.Message, Exception = ex } };
            }
        }
    }

    public class AuthlibInjectorTokenAuthenticator : YggdrasilTokenAuthenticator
    {
        public string ServerRootAddress { get; set; }
        public bool CheckMd5 { get; set; }
        public AuthlibInjectorTokenAuthenticator(string serverRootAddr, string token, Uuid selectedProfileUUID, AuthenticateResponse.UserData userData, bool checkMd5 = false) : base(token, selectedProfileUUID, userData)
        {
            this.ServerRootAddress = serverRootAddr;
            this.ProxyAuthServerAddress = ServerRootAddress + "/authserver";
            CheckMd5 = checkMd5;
        }

        public override async Task<AuthenticateResult> DoAuthenticateAsync()
        {
            try
            {
                Validate validate = new Validate(AccessToken);
                if (CheckMd5)
                {
                    string modsDir = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, ".minecraft", "mods");
                    string modsMd5 = FileHelper.GetDirectoryMd5(modsDir);
                    validate.Md5List = modsMd5;
                }
                if (ProxyAuthServerAddress != null)
                {
                    validate.Address = new Uri(ProxyAuthServerAddress + "/validate");
                }
                if (AuthArgs != null && AuthArgs.Count != 0)
                {
                    validate.Arguments = AuthArgs;
                }
                var result = await validate.PerformRequestAsync();
                if (result.IsSuccess)
                {
                    return new AuthenticateResult(AuthState.SUCCESS) { AccessToken = this.AccessToken, UserData = this.UserData, SelectedProfileUUID = this.SelectedProfileUUID };
                }
                else
                {
                    AuthState state;
                    Refresh refresh = new Refresh(AccessToken);
                    if (ProxyAuthServerAddress != null)
                    {
                        validate.Address = new Uri(ProxyAuthServerAddress + "/refresh");
                    }
                    if (AuthArgs != null && AuthArgs.Count != 0)
                    {
                        refresh.Arguments = AuthArgs;
                    }
                    var refreshResult = await refresh.PerformRequestAsync();
                    if (refreshResult.IsSuccess)
                    {
                        this.AccessToken = refreshResult.AccessToken;
                        state = AuthState.SUCCESS;
                    }
                    else
                    {
                        state = AuthState.REQ_LOGIN;
                        if (refreshResult.Code == System.Net.HttpStatusCode.NotFound)
                        { state = AuthState.ERR_NOTFOUND; }
                    }
                    return new AuthenticateResult(state)
                    {
                        AccessToken = AccessToken = this.AccessToken,
                        UserData = this.UserData,
                        SelectedProfileUUID = this.SelectedProfileUUID,
                        Error = refreshResult.Error
                    };
                }
            }
            catch (Exception ex)
            {
                return new AuthenticateResult(AuthState.ERR_INSIDE)
                {
                    Error = new Net.MojangApi.Error() { ErrorMessage = ex.Message, Exception = ex },
                    AccessToken = AccessToken = this.AccessToken,
                    UserData = this.UserData,
                    SelectedProfileUUID = this.SelectedProfileUUID
                };
            }
        }

        //public override async Task<AuthenticateResult> DoAuthenticateAsync()
        //{
        //    try
        //    {
        //        Validate validate = new Validate(AccessToken);
        //        if (CheckMd5)
        //        {
        //            string modsDir = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, ".minecraft", "mods");
        //            string modsMd5 = FileHelper.GetDirectoryMd5(modsDir);
        //            validate.Md5List = modsMd5;
        //        }
        //        if (ProxyAuthServerAddress != null)
        //        {
        //            validate.Address = new Uri(ProxyAuthServerAddress + "/validate");
        //        }
        //        if (AuthArgs != null && AuthArgs.Count != 0)
        //        {
        //            validate.Arguments = AuthArgs;
        //        }
        //        var resultTask = validate.PerformRequestAsync();
        //        var result = resultTask.Result;
        //        if (result.IsSuccess)
        //        {
        //            return new AuthenticateResult(AuthState.SUCCESS)
        //            {
        //                AccessToken = this.AccessToken,
        //                SelectedProfileUUID = this.SelectedProfileUUID,
        //                UserData = this.UserData
        //            };
        //        }
        //        else
        //        {
        //            AuthState state;
        //            Refresh refresh = new Refresh(AccessToken);
        //            if (ProxyAuthServerAddress != null)
        //            {
        //                validate.Address = new Uri(ProxyAuthServerAddress + "/refresh");
        //            }
        //            if (AuthArgs != null && AuthArgs.Count != 0)
        //            {
        //                refresh.Arguments = AuthArgs;
        //            }
        //            var refreshResultTask = refresh.PerformRequestAsync();
        //            var refreshResult = refreshResultTask.Result;
        //            if (refreshResult.IsSuccess)
        //            {
        //                this.AccessToken = refreshResult.AccessToken;
        //                state = AuthState.SUCCESS;
        //            }
        //            else
        //            {
        //                state = AuthState.REQ_LOGIN;
        //                if (refreshResult.Code == System.Net.HttpStatusCode.NotFound)
        //                { state = AuthState.ERR_NOTFOUND; }
        //            }
        //            return new AuthenticateResult(state)
        //            {
        //                AccessToken = AccessToken = this.AccessToken,
        //                SelectedProfileUUID = this.SelectedProfileUUID,
        //                UserData = this.UserData,
        //                Error = refreshResult.Error
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new AuthenticateResult(AuthState.ERR_INSIDE)
        //        {
        //            Error = new Net.MojangApi.Error() { ErrorMessage = ex.Message, Exception = ex },
        //            AccessToken = AccessToken = this.AccessToken,
        //            UserData = this.UserData,
        //            SelectedProfileUUID = this.SelectedProfileUUID
        //        };
        //    }
        //}
    }
}
