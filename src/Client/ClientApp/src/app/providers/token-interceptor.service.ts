import { HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Injectable, Optional } from '@angular/core';
import { OAuthStorage, OAuthResourceServerErrorHandler, OAuthModuleConfig } from 'angular-oauth2-oidc';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService {
  moduleConfig : OAuthModuleConfig={
    resourceServer: {
      allowedUrls: [
        environment.api+'/api/helprequest',
        environment.api+'/api/helprequests',
        environment.api+'/api/publicnew',],
      sendAccessToken: true
    }
  };
  constructor(
    private authStorage: OAuthStorage,
    private errorHandler: OAuthResourceServerErrorHandler,
) {
}

private checkUrl(url: string): boolean {
    let found = this.moduleConfig.resourceServer.allowedUrls?.find(u => url.startsWith(u));
    return !!found;
}

public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let url = req.url.toLowerCase();
    if (!this.moduleConfig) return next.handle(req);
    if (!this.moduleConfig.resourceServer) return next.handle(req);
    if (!this.moduleConfig.resourceServer.allowedUrls) return next.handle(req);
    if (!this.checkUrl(url)) return next.handle(req);

    let sendAccessToken = this.moduleConfig.resourceServer.sendAccessToken;
    
    if (sendAccessToken) {

        let token = this.authStorage.getItem('access_token');
        if(token){
          let header = 'Bearer ' + token;
          let headers = req.headers
                              .set('Authorization', header);
  
          req = req.clone({ headers });
        }

    }

    return next.handle(req);

}
}
