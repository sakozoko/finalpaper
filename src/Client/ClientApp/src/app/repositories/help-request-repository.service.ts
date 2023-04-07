import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Subject} from 'rxjs';
import {environment} from '../environment/environment';


@Injectable({
  providedIn: 'root'
})
export class HelpRequestRepositoryService {
  constructor(private _httpClient: HttpClient) {
  }

  public createHelpRequest(helpRequest: HelpRequestRequestModel) {
    let subj = new Subject<string>();
      this._httpClient.post<string>(environment.api + '/api/helprequest', helpRequest).subscribe(result => {
        subj.next(result);
      });
    return subj.asObservable();
  }

  public getHelpRequestForUserByPage(page: number, pageSize: number) {
    let subj = new Subject<HelpRequestModel[]>();
      this._httpClient.get<HelpRequestModel[]>(environment.api + '/api/helprequest?page=' + page + '&pageSize=' + pageSize).subscribe(result => {
        subj.next(result);
      });
    return subj.asObservable();
  }

  public getHelpRequestCountForUser() {
    let subj = new Subject<number>();
      this._httpClient.get<number>(environment.api + '/api/helprequest/count').subscribe(result => {
        subj.next(result);
      });
    return subj.asObservable();
  }

  public getHelpRequestBySearchString(filter: string, page: number, pageSize: number) {
    let subj = new Subject<HelpRequestModel[]>();
      this._httpClient.get<HelpRequestModel[]>(environment.api + '/api/helprequest/search?filter=' + filter + '&page=' + page + '&pageSize=' + pageSize).subscribe(result => {
        subj.next(result);
      });
    return subj.asObservable();
  }

  public getHelpRequestBySearchStringCount(filter: string) {
    let subj = new Subject<number>();
    this._httpClient.get<number>(environment.api + '/api/helprequest/search/count?filter=' + filter).subscribe(result => {
      subj.next(result);
    });
    return subj.asObservable();
  }

  public getHelpRequests(status: string) {
    let subj = new Subject<HelpRequestModel[]>();
    this._httpClient.get<HelpRequestModel[]>(environment.api + '/api/helprequest/getall?status=' + status).subscribe(result => {
      subj.next(result);
    });
    return subj.asObservable();
  }

  public getHelpRequestCount(status: string) {
    let subj = new Subject<number>();
    this._httpClient.get<number>(environment.api + '/api/helprequest/getall/count?status=' + status).subscribe(result => {
      subj.next(result);
    });
    return subj.asObservable();
  }

  public deleteHelpRequest(id: string) {
    let subj = new Subject<HelpRequestModel>();
    this._httpClient.delete<HelpRequestModel>(environment.api + '/api/helprequest/' + id).subscribe(result => {
      subj.next(result);
    });
    return subj.asObservable();
  }

  public answerHelpRequest(id: string, answer: string) {
    let subj = new Subject<HelpRequestModel>();
    this._httpClient.put<HelpRequestModel>(environment.api + '/api/helprequest/answer', {
      'id': id,
      'answer': answer
    }).subscribe(result => {
      subj.next(result);
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
