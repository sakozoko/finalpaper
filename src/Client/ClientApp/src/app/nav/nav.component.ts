import {Component, Input} from '@angular/core';
import {environment} from "../../environment/environment";
import {OidcSecurityService} from "angular-auth-oidc-client";

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  @Input() public title: string ;
  @Input() public isAuthorized: boolean | undefined ;
  constructor(public OidcSecurityService: OidcSecurityService) {
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
