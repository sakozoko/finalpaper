import { Injectable } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {

  constructor(private oidcSecurityService : OidcSecurityService) { }

  public getAuthorizationHeaderValue() : string {
    return `${this.oidcSecurityService.getAccessToken()}`;
  }
  public isAuthenticated() : Observable<boolean> {
    let subject = new Subject<boolean>();
    this.oidcSecurityService.isAuthenticated().subscribe((isAuthenticated)=>{
      subject.next(isAuthenticated);
    });
    return subject.asObservable();
  }
  public isAdmin() : Observable<boolean> {
    return this.isAuthorized("Admin");
  }
  private isAuthorized(role:string) : Observable<boolean> {
    let subject = new Subject<boolean>();
    this.oidcSecurityService.checkAuth().subscribe((isAuthenticated)=>{
      subject.next(isAuthenticated.userData.role==role);
    });
    return subject.asObservable();
  }

}
