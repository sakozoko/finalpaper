import {Component, Input, OnInit} from '@angular/core';
import {
  City,
  VolunteerOrganization,
  VolunteerOrganizationRepositoryService
} from "../repositories/volunteer-organization-repository.service";
import {PaginationService} from "../services/pagination.service";
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-volunteer-organizations',
  templateUrl: './volunteer-organizations.component.html',
  styleUrls: ['./volunteer-organizations.component.css']
})
export class VolunteerOrganizationsComponent implements OnInit {
  page: number = 1;
  @Input()
  pageSize: number = 10;
  formControl = new FormControl('');
  formControlSelect = new FormControl('');
  cityCtrl = new FormControl('');
  citySelectCtrl = new FormControl('');

  categories: string[] = [];
  filterCategories: string[];
  cities: City[] = []
  filteredCities: City[];
  volunteerOrganizations: VolunteerOrganization[]

  constructor(private repository: VolunteerOrganizationRepositoryService,
              public paginationService: PaginationService) {
  }

  getUkrainianCategoryName(categoryName: string): string {

    switch (categoryName) {
      case "Humanitarian":
        return "Гуманітарна допомога";
      case "BloodDonation":
        return "Донорство крові";
      case "HelpingChildren":
        return "Допомога дітям";
      case "HelpingAnimals":
        return "Допомога тваринам";
      case "HelpingElderlyPeople":
        return "Допомога старим";
      case "HelpingDisabledPeople":
        return "Допомога інвалідам";
      case "HelpingIdp":
        return "Допомога біженцям";
      case "FoodDistribution":
        return "Їжа";
      case "Evacuation":
        return "Евакуація";
      case "Medicine":
        return "Медицина";
      case "CarVolunteering":
        return "Автоволонтери";
      case "MentalHealth":
        return "Психологічна допомога";
      case "Doctor":
        return "Лікар";
      case "AccommodationOfIdp":
        return "Проживання біженців";
      case "HelpingDefenders":
        return "Допомога захисникам";
      default:
        return categoryName;
    }
  }

  filter($event: any) {
    this.filterCategories = this.categories.filter(x => this.getUkrainianCategoryName(x).toLowerCase().includes($event.toLowerCase()));
  }

  filterCities($event: any) {
    this.filteredCities = this.cities.filter(x => x.name.toLowerCase().includes($event.toLowerCase()));
  }

  click() {
    if (this.cityCtrl.value !== '' && this.formControl.value !== '') {
      let numberOfCategory = this.categories.indexOf(this.formControl.value ?? '') + 1;
      this.repository.getVolunteerOrganizationCount(Number.parseInt(this.cityCtrl.value ?? ''), numberOfCategory).subscribe(result => {
        console.log(result)
      });
    }
  }


  ngOnInit(): void {
    this.repository.getAvailableCategories().subscribe(result => {
      this.categories = result;
      this.filterCategories = result;
    });
    this.repository.getAvailableCities().subscribe(result => {
      this.cities = result;
      this.filteredCities = result;
    });
  }

}
