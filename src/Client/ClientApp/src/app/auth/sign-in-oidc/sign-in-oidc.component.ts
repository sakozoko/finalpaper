import {Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-sign-in-oidc',
  templateUrl: './sign-in-oidc.component.html',
  styleUrls: ['./sign-in-oidc.component.css']
})
export class SignInOidcComponent implements OnInit {
  constructor(public oauthService: OAuthService, public router : Router) {

  }

  ngOnInit() {
    this.oauthService.initLoginFlow();
  }
}
