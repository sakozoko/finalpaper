import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, Subject} from "rxjs";
import {publicNewModel} from "../../models/public-new/public-new-model";
import {environment} from "../../environment/environment";
import {publicNewEditModel} from "../../models/public-new/public-new-edit-model";
import {publicNewCreateModel} from "../../models/public-new/public-new-create-model";

@Injectable({
  providedIn: 'root'
})
export class PublicNewsRepositoryService {

  constructor(private httpClient: HttpClient) {
  }

  getPublicNews(page: number, pageSize: number): Observable<publicNewModel[]> {
    return this.httpClient.get<publicNewModel[]>(environment.api + '/api/publicnew' + `?page=${page}&pageSize=${pageSize}`);
  }

  getPublicNewById(id: string): Observable<publicNewModel> {
    return this.httpClient.get<publicNewModel>(environment.api + '/api/publicnew/' + id);
  }

  getPublicNewsCount(): Observable<number> {
    return this.httpClient.get<number>(environment.api + '/api/publicnew/count');
  }

  deletePublicNewById(id: string): Observable<publicNewModel> {
    let subj = new Subject<publicNewModel>();

    this.httpClient.delete<publicNewModel>(environment.api + '/api/publicnew/' + id).subscribe(result => {
      subj.next(result);
    });
    return subj.asObservable();
  }

  editPublicNew(publicNew: publicNewEditModel) {
    let subj = new Subject<publicNewModel>();
    this.httpClient.put<publicNewModel>(environment.api + '/api/publicnew', publicNew).subscribe(result => {
      subj.next(result);
    });
    return subj.asObservable();
  }

  createPublicNew(createModel: publicNewCreateModel): Observable<publicNewModel> {
    let subj = new Subject<publicNewModel>();
    this.httpClient.post<publicNewModel>(environment.api + '/api/publicnew', createModel).subscribe(result => {
      subj.next(result);
    });
    return subj.asObservable();
  }
}
