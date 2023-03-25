import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {OidcSecurityService} from 'angular-auth-oidc-client';
import {Subject} from 'rxjs';
import {environment} from '../environment/environment';


@Injectable({
  providedIn: 'root'
})
export class HelpRequestRepositoryService {
  constructor(private _httpClient: HttpClient, private _oidcSecurityService: OidcSecurityService) {
  }

  public createHelpRequest(helpRequest: HelpRequestRequestModel) {
    let subj = new Subject<string>();
    this._oidcSecurityService.getAccessToken().subscribe(token => {
      let header = {'Authorization': 'Bearer ' + token};
      this._httpClient.post<string>(environment.api + '/api/helprequest', helpRequest, {headers: header}).subscribe(result => {
        subj.next(result);
      });
    });
    return subj.asObservable();
  }

  public getHelpRequestForUserByPage(page: number, pageSize: number) {
    let subj = new Subject<HelpRequestModel[]>();
    this._oidcSecurityService.getAccessToken().subscribe(token => {
      let header = {'Authorization': 'Bearer ' + token};
      this._httpClient.get<HelpRequestModel[]>(environment.api + '/api/helprequest?page=' + page + '&pageSize=' + pageSize, {headers: header}).subscribe(result => {
        subj.next(result);
      });
    });
    return subj.asObservable();
  }

  public getHelpRequestCountForUser() {
    let subj = new Subject<number>();
    this._oidcSecurityService.getAccessToken().subscribe(token => {
      let header = {'Authorization': 'Bearer ' + token};
      this._httpClient.get<number>(environment.api + '/api/helprequest/count', {headers: header}).subscribe(result => {
        subj.next(result);
      });
    });
    return subj.asObservable();
  }

  public getHelpRequestBySearchString(filter: string, page: number, pageSize: number) {
    let subj = new Subject<HelpRequestModel[]>();
    this._oidcSecurityService.getAccessToken().subscribe(token => {
      let header = {'Authorization': 'Bearer ' + token};
      this._httpClient.get<HelpRequestModel[]>(environment.api + '/api/helprequest/search?filter=' + filter + '&page=' + page + '&pageSize=' + pageSize, {headers: header}).subscribe(result => {
        subj.next(result);
      });
    });
    return subj.asObservable();
  }

  public getHelpRequestBySearchStringCount(filter: string) {
    let subj = new Subject<number>();
    this._oidcSecurityService.getAccessToken().subscribe(token => {
      let header = {'Authorization': 'Bearer ' + token};
      this._httpClient.get<number>(environment.api + '/api/helprequest/search/count?filter=' + filter, {headers: header}).subscribe(result => {
        subj.next(result);
      });
    });
    return subj.asObservable();
  }
public getHelpRequsts(page:number, pageSize:number){
  let subj = new Subject<HelpRequestModel[]>();
  this._oidcSecurityService.getAccessToken().subscribe(token => {
    let header = {'Authorization': 'Bearer ' + token};
    this._httpClient.get<HelpRequestModel[]>(environment.api + '/api/helprequest/getall?page=' + page + '&pageSize=' + pageSize, {headers: header}).subscribe(result => {
      subj.next(result);
    });
  });
  return subj.asObservable();
}
public getHelpRequestCount(){
  let subj = new Subject<number>();
  this._oidcSecurityService.getAccessToken().subscribe(token => {
    let header = {'Authorization': 'Bearer ' + token};
    this._httpClient.get<number>(environment.api + '/api/helprequest/getall/count', {headers: header}).subscribe(result => {
      subj.next(result);
    });
  });
  return subj.asObservable();
}
}

export class HelpRequestRequestModel {
  title: string;
  description: string;
}

export class HelpRequestModel {
  id: string;
  title: string;
  description: string;
  createdAt: Date;
  status: string;
}
