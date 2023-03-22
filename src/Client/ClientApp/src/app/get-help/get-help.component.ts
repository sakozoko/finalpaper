import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {OidcSecurityService} from 'angular-auth-oidc-client';
import {Subject} from 'rxjs';
import {HelpRequestRepositoryService, HelpRequestRequestModel} from '../repositories/help-request-repository.service';

@Component({
  selector: 'app-get-help',
  templateUrl: './get-help.component.html',
  styleUrls: ['./get-help.component.css']
})
export class GetHelpComponent implements OnInit {

  CanSendRequest = false;
  SendingRequest = false;
  RequestResultMessage = '';
  reloadForm: Subject<boolean> = new Subject<boolean>();

  HelpRequestForm = new FormGroup({
    title: new FormControl('', [
      Validators.required,
      Validators.minLength(5),
      Validators.maxLength(100)
    ]),
    description: new FormControl('', [
      Validators.required,
      Validators.minLength(50),
      Validators.maxLength(2000)
    ]),
  });

  constructor(public OidcSecurityService: OidcSecurityService,
              private helpRequestRepository: HelpRequestRepositoryService) {

  }

  ngOnInit(): void {
    this.OidcSecurityService.getUserData().subscribe((userData) => {
      this.CanSendRequest = userData['email_verified'];
    });
  }

  onSubmit() {
    if (this.SendingRequest) {
      return;
    }
    this.SendingRequest = true;
    let helpRequest = new HelpRequestRequestModel();
    helpRequest.title = this.HelpRequestForm.value.title ?? '';
    helpRequest.description = this.HelpRequestForm.value.description ?? '';
    helpRequest.emailConfirmed = this.CanSendRequest;
    this.OidcSecurityService.checkAuth().subscribe(userdata => {
      helpRequest.userId = userdata.userData.sub;
      this.helpRequestRepository.createHelpRequest(helpRequest).subscribe(result => {
        this.reloadForm.next(true);
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
