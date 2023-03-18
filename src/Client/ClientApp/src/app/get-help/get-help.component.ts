import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { HelpRequestRepositoryService, HelpRequestRequestModel } from '../repositories/help-request-repository.service';

@Component({
  selector: 'app-get-help',
  templateUrl: './get-help.component.html',
  styleUrls: ['./get-help.component.css']
})
export class GetHelpComponent {

  CanSendRequest = false;
  SendingRequest = false;
  RequestResultMessage = '';
  HelpRequestForm= new FormGroup({
    title: new FormControl(''),
    description: new FormControl(''),
  });

  constructor(public OidcSecurityService : OidcSecurityService,
    private helpRequestRepository : HelpRequestRepositoryService){
    this.OidcSecurityService.getUserData().subscribe((userData) => {
      this.CanSendRequest = userData['email_verified'];
      });
  }
  onSubmit(){
    this.SendingRequest = true;
    let helpRequest = new HelpRequestRequestModel();
    helpRequest.title = this.HelpRequestForm.value.title??'';
    helpRequest.description = this.HelpRequestForm.value.description??'';
    helpRequest.emailConfirmed = this.CanSendRequest;
    this.OidcSecurityService.userData$.subscribe(userdata=>{
      helpRequest.userId = userdata.userData.sub;
      this.helpRequestRepository.createHelpRequest(helpRequest).subscribe(result => {
        this.RequestResultMessage = 'Ваша заявка відправлена. Номер вашої заявки ' + result;
        this.HelpRequestForm.reset();
        this.SendingRequest = false;
      }, error => {
        this.RequestResultMessage = 'Помилка відправки заявки. Спробуйте пізніше';
        this.SendingRequest = false;
      });
    });

  }

}
