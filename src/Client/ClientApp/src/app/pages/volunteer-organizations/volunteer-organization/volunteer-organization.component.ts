import {Component, Input, OnInit} from '@angular/core';
import {VolunteerOrganizationModel} from "../../../models/volunteer-organization/volunteer-organization-model";

@Component({
  selector: 'app-volunteer-organization',
  templateUrl: './volunteer-organization.component.html',
  styleUrls: ['./volunteer-organization.component.css']
})
export class VolunteerOrganizationComponent implements OnInit {

  @Input()
  volunteerOrganization: VolunteerOrganizationModel = new VolunteerOrganizationModel();

  constructor() {
  }

  ngOnInit(): void {
    console.log(this.volunteerOrganization.title)
  }


}
