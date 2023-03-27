import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from "@angular/router";
import { OAuthService } from "angular-oauth2-oidc";
import {Observable} from "rxjs";

@Injectable()
export class AdminGuard implements CanActivate {

  constructor(private oauthService: OAuthService, private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    if(this.oauthService.hasValidAccessToken() && this.oauthService.getIdentityClaims()['role'] === 'Admin'){
      return true;
    }
    this.router.navigate(['/sign-in-oidc']);
    return false;
  }
}
