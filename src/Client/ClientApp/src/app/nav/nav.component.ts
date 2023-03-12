import {Component, Input} from '@angular/core';
import {environment} from "../../environment/environment";
import {OidcSecurityService} from "angular-auth-oidc-client";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  @Input() public title: string ;
  @Input() public isAuthorized: boolean | undefined ;
  constructor(public OidcSecurityService: OidcSecurityService, private router: Router) {
  }

  activatedRouteContains(route: string): boolean {
    return this.router.url.toString().includes(route);
  }
  activatedRouteIsIndex(): boolean {
    return this.router.url.toString() === '/';
  }

  login() {
    this.OidcSecurityService.authorize();
  }
  register(){
    window.location.href=environment.authority + '/Account/Registration'+ '?returnUrl=' + environment.clientUrl;
  }
  logout() {
    this.OidcSecurityService.logoffLocal();
    window.location.href=environment.authority + '/Account/Logout'+ '?returnUrl=' + environment.clientUrl;
  }
  profile(){
    window.location.href=environment.authority + '/Profile'+ '?returnUrl=' + environment.clientUrl;
  }
}
