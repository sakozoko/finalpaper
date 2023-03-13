import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public title = 'Інформаційна Волонтерська Система';

  constructor(public OidcSecurityService: OidcSecurityService, activatedRoute : ActivatedRoute) {
      this.OidcSecurityService.checkAuth().subscribe((isAuthenticated) => {
      });
      activatedRoute.queryParams.subscribe(params => {
        if(params['login'] !== undefined){
          this.OidcSecurityService.authorize();
        }
      });
  }
}
