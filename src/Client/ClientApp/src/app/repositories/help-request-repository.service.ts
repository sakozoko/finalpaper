import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { firstValueFrom, Subject } from 'rxjs';
import { environment } from 'src/environment/environment';

@Injectable({
  providedIn: 'root'
})
export class HelpRequestRepositoryService {
  constructor(private _httpClient : HttpClient, private _oidcSecurityService : OidcSecurityService) {  }

  public createHelpRequest(helpRequest : HelpRequestRequestModel) {
    let subj = new Subject<string>();
    this._oidcSecurityService.getAccessToken().subscribe(token => {
      let header = {'Authorization': 'Bearer ' + token};
      this._httpClient.post<string>(environment.api + '/api/helprequest', helpRequest, {headers: header}).subscribe(result => {
        subj.next(result);
      });
    });
    return subj.asObservable();
  }
}

export class HelpRequestRequestModel{
    title:string;
    description:string;
    userId:string;
    emailConfirmed:boolean;
}

export class HelpRequestModel{
   id:string;
    title:string;
    description:string;
    createdAt:Date;
}
