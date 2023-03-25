import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from "@angular/router";
import {OidcSecurityService} from "angular-auth-oidc-client";
import {Observable} from "rxjs";

@Injectable()
export class AdminGuard implements CanActivate {

  constructor(private oidcSecureService: OidcSecurityService, private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    this.oidcSecureService.checkAuth().subscribe((isAuthenticated) => {
      if (!isAuthenticated.isAuthenticated || !isAuthenticated.userData["role"] || isAuthenticated.userData["role"] !== "Admin") {
        this.router.navigate(["/sign-in-oidc"]);
      }
    });
    return true;
  }
}
