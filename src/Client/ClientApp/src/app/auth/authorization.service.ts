import { Injectable } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  private isAuthenticate : boolean=false;
  private isAuthorize : boolean=false;
  private lastRole : string | null=null;
  constructor(private oidcSecurityService : OidcSecurityService) {
    this.oidcSecurityService.checkAuth().subscribe((isAuthenticated)=>{
      this.isAuthenticate = isAuthenticated.isAuthenticated;
    });
    this.oidcSecurityService.checkSessionChanged$.subscribe((isAuthenticated)=>{
      this.oidcSecurityService.checkAuth().subscribe((isAuthenticated)=>{
        this.isAuthenticate = isAuthenticated.isAuthenticated;
        this.isAuthorize = isAuthenticated.userData?.role==this.lastRole;
      });
    });
   }

  public getAuthorizationHeaderValue() : string {
    return `${this.oidcSecurityService.getAccessToken()}`;
  }
  public isAuthenticated=() : boolean => {
    return this.isAuthenticate;
  }
  public isAdmin=():boolean=> {
    return this.isAuthorizedFunc("Admin");
  }
  private isAuthorizedFunc=(role:string) : boolean =>{
    if(this.lastRole!=role){
      this.oidcSecurityService.getUserData().subscribe((userData)=>{
        this.isAuthorize = userData.role==role;
      });
    }
    this.lastRole = role;
    return this.isAuthorize;
  }

}
