import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client'
@Component({
  selector: 'app-sign-in-oidc',
  templateUrl: './sign-in-oidc.component.html',
  styleUrls: ['./sign-in-oidc.component.css']
})
export class SignInOidcComponent implements OnInit{
constructor(public OidcSecurityService: OidcSecurityService){
 
}
ngOnInit(){
  this.OidcSecurityService.authorize();
}
}
