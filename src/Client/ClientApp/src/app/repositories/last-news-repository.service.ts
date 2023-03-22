import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {LastNew} from "../last-news/last-news.component";
import {environment} from '../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class LastNewsRepositoryService {

  constructor(private _httpClient: HttpClient) {

  }

  getLatestNews(page: number, pageSize: number) {
    return this._httpClient.get<LastNew[]>(environment.api + '/api/latestnews', {
      params: {
        page: page.toString(),
        pageSize: pageSize.toString()
      }
    });
  }

  getLatestNewsCount() {
    return this._httpClient.get<number>(environment.api + '/api/latestnews/count');
  }

  getLatestNewsBySearchString(searchString: string) {
    return this._httpClient.get<LastNew[]>(environment.api + '/api/latestnews/filter', {params: {filter: searchString}});
  }

}
