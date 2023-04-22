import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {VolunteerOrganizationModel} from "../../models/volunteer-organization/volunteer-organization-model";
import {environment} from "../../environment/environment";
import {CityModel} from "../../models/volunteer-organization/city-model";

@Injectable({
  providedIn: 'root'
})
export class VolunteerOrganizationRepositoryService {

  constructor(private httpClient: HttpClient) {
  }

  public getVolunteerOrganizations(cityId: number, categoryId: number, page: number, pageSize: number) {
    return this.httpClient.get<VolunteerOrganizationModel[]>(environment.api + '/api/VolunteerOrganization' + `?cityId=${cityId}&categoryId=${categoryId}&pageNumber=${page}&pageSize=${pageSize}`);
  }

  public getAvailableCities() {
    return this.httpClient.get<CityModel[]>(environment.api + '/api/VolunteerOrganization/availablecities');
  }

  public getAvailableCategories() {
    return this.httpClient.get<string[]>(environment.api + '/api/VolunteerOrganization/availablecategories');
  }

  public getVolunteerOrganizationCount(cityId: number, categoryId: number) {
    return this.httpClient.get<number>(environment.api + '/api/VolunteerOrganization/count' + `?cityId=${cityId}&categoryId=${categoryId}`);
  }
}
