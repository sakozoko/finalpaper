import { NgModule } from '@angular/core';
import { AuthModule } from 'angular-auth-oidc-client';
import {environment} from "../../environment/environment";


@NgModule({
    imports: [AuthModule.forRoot({
        config: {
              authority: environment.authority,
              redirectUrl: window.location.origin,
              postLogoutRedirectUri: window.location.origin,
              clientId: 'webapp',
              scope: 'openid profile offline_access roles webapi', // 'openid profile offline_access ' + your scopes
              responseType: 'code',
              silentRenew: true,
              useRefreshToken: true,
              renewTimeBeforeTokenExpiresInSeconds: 30,
          }
      })],
    exports: [AuthModule],
})
export class AuthConfigModule {}
