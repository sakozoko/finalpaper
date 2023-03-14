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
    this.router.navigate(['/sign-in-oidc']);
  }
  register(){
    window.location.href=environment.authority + '/Account/Registration'+ '?returnUrl=' + environment.clientUrl;
  }
  logout() {
    window.location.href=environment.authority + '/Account/Logout'+ '?returnUrl=' + environment.clientUrl;
    this.OidcSecurityService.logoffLocal();
  }
  profile(){
    window.location.href=environment.authority + '/Profile'+ '?returnUrl=' + environment.clientUrl;
  }
}
