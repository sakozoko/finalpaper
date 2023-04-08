import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../environment/environment";

@Injectable({
  providedIn: 'root'
})
export class VolunteerOrganizationRepositoryService {

  constructor(private httpClient: HttpClient) {
  }

  public getVolunteerOrganizations(cityId: number, categoryId: number, page: number, pageSize: number) {
    return this.httpClient.get(environment.api + '/api/volunteerorganization' + `?cityId=${cityId}&categoryId=${categoryId}&page=${page}&pageSize=${pageSize}`);
  }

  public getAvailableCities() {
    return this.httpClient.get(environment.api + '/api/volunteerorganization/availablecities');
  }

  public getAvailableCategories() {
    return this.httpClient.get(environment.api + '/api/volunteerorganization/availablecategories');
  }

  public getVolunteerOrganizationCount(cityId: number, categoryId: number) {
    return this.httpClient.get(environment.api + '/api/volunteerorganization/count' + `?cityId=${cityId}&categoryId=${categoryId}`);
  }
}
