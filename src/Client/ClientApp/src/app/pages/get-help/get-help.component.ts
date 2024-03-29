import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {OAuthService} from 'angular-oauth2-oidc';
import {Subject} from 'rxjs';
import {HelpRequestRequestModel} from "../../models/help-request/help-request-request-model";
import {HelpRequestRepositoryService} from "../../services/repositories/help-request-repository.service";

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

  constructor(public oauthService: OAuthService,
              private helpRequestRepository: HelpRequestRepositoryService) {
  }

  ngOnInit(): void {
    this.CanSendRequest = this.oauthService.getIdentityClaims()['email_verified'];
  }

  onSubmit() {
    if (this.SendingRequest) {
      return;
    }
    this.SendingRequest = true;
    let helpRequest = new HelpRequestRequestModel();
    helpRequest.title = this.HelpRequestForm.value.title ?? '';
    helpRequest.description = this.HelpRequestForm.value.description ?? '';
      this.helpRequestRepository.createHelpRequest(helpRequest).subscribe(result => {
        this.reloadForm.next(true);
        this.RequestResultMessage = 'Ваша заявка відправлена. Номер вашої заявки ' + result;
        this.HelpRequestForm.reset();
        this.SendingRequest = false;
      }, error => {
        this.RequestResultMessage = 'Помилка відправки заявки. Спробуйте пізніше';
        this.SendingRequest = false;
      });
  }

}
