import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public title = 'Інформаційна Волонтерська Система';

  constructor(public OidcSecurityService: OidcSecurityService, router : Router) {
    this.OidcSecurityService.checkAuth().subscribe((isAuthenticated) => {
    });
  }
}
