import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
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
  formGroup : FormGroup;
  dateControl: FormControl;
  constructor(public OidcSecurityService: OidcSecurityService,
     private router : Router,
     helpRequestRepository : HelpRequestRepositoryService,
     formBuilder : FormBuilder) {
      this.dateControl = new FormControl();
    this.OidcSecurityService.checkAuth().subscribe((isAuthenticated) => {  
    });
  }
}
