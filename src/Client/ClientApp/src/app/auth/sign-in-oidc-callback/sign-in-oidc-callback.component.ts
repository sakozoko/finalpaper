import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {OidcSecurityService} from 'angular-auth-oidc-client';

@Component({
  selector: 'app-sign-in-oidc-callback',
  templateUrl: './sign-in-oidc-callback.component.html',
  styleUrls: ['./sign-in-oidc-callback.component.css']
})
export class SignInOidcCallbackComponent implements OnInit {
  constructor(public OidcSecurityService: OidcSecurityService, private activatedRoute: ActivatedRoute,
              private router: Router) {

  }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      if (!params['code'])
        this.router.navigate(['/']);
    });
  }
}
