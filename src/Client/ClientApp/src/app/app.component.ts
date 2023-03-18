import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { HelpRequestRepositoryService } from './repositories/help-request-repository.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public title = 'Інформаційна Волонтерська Система';

  constructor(public OidcSecurityService: OidcSecurityService, router : Router, helpRequestRepository : HelpRequestRepositoryService) {
    this.OidcSecurityService.checkAuth().subscribe((isAuthenticated) => {  
    });
  }
}
