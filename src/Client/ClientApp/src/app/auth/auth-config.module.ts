import {NgModule} from '@angular/core';
import {AuthModule} from 'angular-auth-oidc-client';
import {SignInOidcComponent} from './sign-in-oidc/sign-in-oidc.component';
import {SignInOidcCallbackComponent} from './sign-in-oidc-callback/sign-in-oidc-callback.component';
import {environment} from '../environment/environment';


@NgModule({
  imports: [AuthModule.forRoot({
    config: {
      authority: environment.authority,
      redirectUrl: environment.clientUrl + '/sign-in-oidc-callback',
      postLogoutRedirectUri: environment.clientUrl + '/sign-out-oidc-callback',
      clientId: 'webapp',
      scope: 'openid profile offline_access roles webapi email phoneNumber', // 'openid profile offline_access ' + your scopes
      responseType: 'code',
      silentRenew: true,
      useRefreshToken: true,
      renewTimeBeforeTokenExpiresInSeconds: 60,
    }
  })],
  exports: [AuthModule],
  declarations: [
    SignInOidcComponent,
    SignInOidcCallbackComponent
  ],
})
export class AuthConfigModule {
}
