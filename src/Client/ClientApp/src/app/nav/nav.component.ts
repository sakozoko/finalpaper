import {Component, Input} from '@angular/core';
import {OidcSecurityService} from "angular-auth-oidc-client";
import {Router} from "@angular/router";
import {environment} from '../environment/environment';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  @Input() public title: string;
  @Input() public isAuthorized: boolean | undefined;
  @Input() public isAdmin: boolean | undefined;
  collapsed = true;
  collapsedPc = true;

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

  register() {
    window.location.href = environment.authority + '/Account/Registration' + '?returnUrl=' + environment.clientUrl;
  }

  logout() {
    window.location.href = environment.authority + '/Account/Logout' + '?returnUrl=' + environment.clientUrl;
    this.OidcSecurityService.logoffLocal();
  }

  profile() {
    window.location.href = environment.authority + '/Profile' + '?returnUrl=' + environment.clientUrl;
  }

  closePcMenu() {
    this.collapsedPc = true;
    let fullmenu = document.getElementById("fullmenu");
    if (fullmenu) {
      fullmenu.style.width = "0px";
      fullmenu.style.padding = "0px";
    }
    let menu = document.getElementById("menu");
    if (menu) {
      menu.style.height = "0px";
      menu.style.width = "0px";
      menu.style.padding = "0px";
    }
  }

  openPcMenu() {
    this.collapsedPc = false;
    let fullmenu = document.getElementById("fullmenu");
    if (fullmenu) {
      fullmenu.style.width = "";
      fullmenu.style.padding = "";
    }
    setTimeout(() => {
      let menu = document.getElementById("menu");
      if (menu) {
        menu.style.width = "";
        menu.style.height = "";
        menu.style.padding = "";
      }
    }, 50);
  }

}
