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
    return this.httpClient.get<VolunteerOrganization[]>(environment.api + '/api/VolunteerOrganization' + `?cityId=${cityId}&categoryId=${categoryId}&pageNumber=${page}&pageSize=${pageSize}`);
  }

  public getAvailableCities() {
    return this.httpClient.get<City[]>(environment.api + '/api/VolunteerOrganization/availablecities');
  }

  public getAvailableCategories() {
    return this.httpClient.get<string[]>(environment.api + '/api/VolunteerOrganization/availablecategories');
  }

  public getVolunteerOrganizationCount(cityId: number, categoryId: number) {
    return this.httpClient.get<number>(environment.api + '/api/VolunteerOrganization/count' + `?cityId=${cityId}&categoryId=${categoryId}`);
  }
}

export class VolunteerOrganization {
  title: string;
  description: string;
  phones: string[];
  addresses: string[];
  socialNetworks: string[];
}

export class City {
  id: number;
  name: string;
}
