import {Component, Input, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {environment} from '../environment/environment';
import {OAuthService} from 'angular-oauth2-oidc';
import {HelpRequestRepositoryService} from "../repositories/help-request-repository.service";

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  @Input() public title: string;
  public isAuthorized: boolean = false;
  public isAdmin: boolean = false;
  collapsed = true;
  collapsedPc = true;
  helpRequestCount: number = 0;

  constructor(public oauthService: OAuthService,
              private router: Router,
              private helpRequestRepository: HelpRequestRepositoryService) {

  }

  ngOnInit(): void {
    this.isAuthorized = this.oauthService.hasValidAccessToken();
    this.isAdmin = this.isAuthorized && this.oauthService.getIdentityClaims()['role'] === 'Admin';
    if (this.isAdmin) {
      this.helpRequestRepository.getHelpRequestCount('new').subscribe(count => {
        this.helpRequestCount = count;
      });
      setInterval(() => {
        this.helpRequestRepository.getHelpRequestCount('new').subscribe(count => {
          this.helpRequestCount = count;
        });
      }, 10000);
    }
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
    this.oauthService.logOut(true);
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
