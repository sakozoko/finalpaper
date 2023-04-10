import {Component, Input, OnInit} from '@angular/core';
import {VolunteerOrganization} from "../../repositories/volunteer-organization-repository.service";

@Component({
  selector: 'app-volunteer-organization',
  templateUrl: './volunteer-organization.component.html',
  styleUrls: ['./volunteer-organization.component.css']
})
export class VolunteerOrganizationComponent implements OnInit {

  @Input()
  volunteerOrganization: VolunteerOrganization = new VolunteerOrganization();

  constructor() {
  }

  ngOnInit(): void {
    console.log(this.volunteerOrganization.title)
  }


}
