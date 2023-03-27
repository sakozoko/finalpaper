import { formatDate } from '@angular/common';
import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { authConfig } from './auth/auth-config.module';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public title = 'Інформаційна Волонтерська Система';
  isAdmin = false;
  constructor(public oauthService: OAuthService,
              private router: Router) {
  }
  ngOnInit(): void {
    this.oauthService.configure(authConfig);
    this.oauthService.loadDiscoveryDocumentAndTryLogin().then(() => {
      this.isAdmin = this.oauthService.hasValidAccessToken() && this.oauthService.getIdentityClaims()['role'] === 'Admin';
    });
  }
  
}
