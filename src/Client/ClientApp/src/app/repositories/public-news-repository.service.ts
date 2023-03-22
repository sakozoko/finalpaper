import {Injectable} from '@angular/core';
import {OidcSecurityService} from 'angular-auth-oidc-client';
import {publicNew} from "../public-news/public-new/public-new.component";
import {HttpClient} from "@angular/common/http";
import {Observable, Subject} from "rxjs";
import {AuthorizationService} from "../auth/authorization.service";
import {environment} from "../environment/environment";

@Injectable({
  providedIn: 'root'
})
export class PublicNewsRepositoryService {

  constructor(private authorizationService : AuthorizationService, private httpClient : HttpClient) {
  }

  getPublicNews(page: number, pageSize: number) : Observable<publicNew[]>{
      return this.httpClient.get<publicNew[]>(environment.api+'/api/publicnew'+`?page=${page}&pageSize=${pageSize}`);
  }

  getPublicNewById(id: string) : Observable<publicNew> {
    return this.httpClient.get<publicNew>(environment.api+'/api/publicnew/'+id);
  }

  getPublicNewsCount() : Observable<number> {
    return this.httpClient.get<number>(environment.api+'/api/publicnew/count');
  }

  deletePublicNewById(id: string) : Observable<publicNew> {
    let subj = new Subject<publicNew>();
    this.authorizationService.getAccessToken().subscribe(value => {
      let header = {'Authorization':'Bearer '+ value};
      this.httpClient.delete<publicNew>(environment.api+'/api/publicnew/'+id, {headers: header}).subscribe(result => {
        subj.next(result);
      });
    });
    return subj.asObservable();
  }

  editPublicNew(publicNew: publicNew) {
    let subj = new Subject<publicNew>();
    this.authorizationService.getAccessToken().subscribe(value => {
      let header = {'Authorization':'Bearer '+ value};
      this.httpClient.put<publicNew>(environment.api+'/api/publicnew', publicNew, {headers: header}).subscribe(result => {
        subj.next(result);
      });
    });
    return subj.asObservable();
  }

  createPublicNew(createModel: publicNewCreateModel): Observable<publicNew> {
    let subj = new Subject<publicNew>();
    this.authorizationService.getAccessToken().subscribe(value => {
      let header = {'Authorization': 'Bearer ' + value};
      this.httpClient.post<publicNew>(environment.api+'/api/publicnew', createModel, {headers: header}).subscribe(result => {
        subj.next(result);
      });
    });
    return subj.asObservable();
  }
}

export class publicNewCreateModel {
  title: string;
  description: string;
  image: string;
}
