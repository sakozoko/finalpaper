import {NgModule} from '@angular/core';
import {environment} from '../environment/environment';
import { HttpClientModule, HTTP_INTERCEPTORS, provideHttpClient } from '@angular/common/http';
import { OAuthModule, provideOAuthClient } from 'angular-oauth2-oidc';
import { AuthConfig } from 'angular-oauth2-oidc/auth.config';
import { TokenInterceptorService } from '../providers/token-interceptor.service';

export const authConfig : AuthConfig = 
  {
    issuer: environment.authority,
    redirectUri: environment.clientUrl + '/sign-in-oidc-callback',
    postLogoutRedirectUri: environment.clientUrl + '/sign-out-oidc-callback',
    clientId: 'webapp',
    scope: 'openid profile offline_access roles webapi email phoneNumber', // 'openid profile offline_access ' + your scopes
    responseType: 'code'
  };

@NgModule({
  imports: 
  [HttpClientModule,
    OAuthModule.forRoot()],
  providers: [
    provideHttpClient(),
    provideOAuthClient(),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true,
    }]
})
export class AuthConfigModule {
}
