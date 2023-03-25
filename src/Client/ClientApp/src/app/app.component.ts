import { HttpClient } from '@angular/common/http';
import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup} from '@angular/forms';
import {Router} from '@angular/router';
import {OidcSecurityService} from 'angular-auth-oidc-client';
import { environment } from './environment/environment';
import {HelpRequestRepositoryService} from './repositories/help-request-repository.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public title = 'Інформаційна Волонтерська Система';
  isAdmin = false;
  constructor(public OidcSecurityService: OidcSecurityService,
              private router: Router) {
  }
  ngOnInit(): void {
    this.OidcSecurityService.checkAuth().subscribe((isAuthenticated) => {
      if(isAuthenticated.isAuthenticated)
      isAuthenticated.userData['role'] === 'Admin' ? this.isAdmin = true : this.isAdmin = false;
    });
  }
  
}
