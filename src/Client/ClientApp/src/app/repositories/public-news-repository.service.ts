import {Injectable} from '@angular/core';
import {publicNew} from "../public-news/public-new/public-new.component";
import {HttpClient} from "@angular/common/http";
import {Observable, Subject} from "rxjs";
import {environment} from "../environment/environment";
@Injectable({
  providedIn: 'root'
})
export class PublicNewsRepositoryService {

  constructor( private httpClient : HttpClient) {
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

      this.httpClient.delete<publicNew>(environment.api+'/api/publicnew/'+id).subscribe(result => {
        subj.next(result);
      });
    return subj.asObservable();
  }

  editPublicNew(publicNew: publicNewEditModel) {
    let subj = new Subject<publicNew>();
      this.httpClient.put<publicNew>(environment.api+'/api/publicnew', publicNew).subscribe(result => {
        subj.next(result);
      });
    return subj.asObservable();
  }

  createPublicNew(createModel: publicNewCreateModel): Observable<publicNew> {
    let subj = new Subject<publicNew>();
      this.httpClient.post<publicNew>(environment.api+'/api/publicnew', createModel).subscribe(result => {
        subj.next(result);
      });
    return subj.asObservable();
  }
}

export class publicNewCreateModel {
  title: string;
  description: string;
  imageUrl: string;
  createdAt:Date;
}
export class publicNewEditModel extends publicNewCreateModel {
  id: string;
  author: string;
}
