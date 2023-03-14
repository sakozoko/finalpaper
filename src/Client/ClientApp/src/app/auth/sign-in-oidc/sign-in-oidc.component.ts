import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { environment } from 'src/environment/environment';
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
