import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-sign-in-oidc-callback',
  templateUrl: './sign-in-oidc-callback.component.html',
  styleUrls: ['./sign-in-oidc-callback.component.css']
})
export class SignInOidcCallbackComponent implements OnInit {
  constructor(public oauthService: OAuthService, private activatedRoute: ActivatedRoute,
              private router: Router) {

  }

  ngOnInit(): void {
    this.oauthService.events.subscribe(e=>{
      if(e.type.toString()=='token_received')
      {
        this.router.navigate(['/']);
      }
    });
  }
}
