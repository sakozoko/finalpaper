import { Component } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-get-help',
  templateUrl: './get-help.component.html',
  styleUrls: ['./get-help.component.css']
})
export class GetHelpComponent {

  public CanSendRequest = false;

  constructor(public OidcSecurityService : OidcSecurityService){
    this.OidcSecurityService.getUserData().subscribe((userData) => {
      this.CanSendRequest = userData['email_verified'] && userData['phone_number_verified'];
      });
  }

}
