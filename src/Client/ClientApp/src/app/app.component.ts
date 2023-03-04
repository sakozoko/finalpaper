import { Component } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ClientApp';

  constructor(public OidcSecurityService: OidcSecurityService) {
    OidcSecurityService.checkAuth().subscribe((auth) => {
      console.log('app authenticated', auth);
    });
  }
  login() {
    this.OidcSecurityService.authorize();
  }
  refreshSession() {
    this.OidcSecurityService.authorize();
  }
  logout() {
    this.OidcSecurityService.logoff();
  }
}
