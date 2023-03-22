import {Component, Input} from '@angular/core';
import {PaginationService} from "../services/pagination.service";

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent {
  @Input() paginationService: PaginationService;

  constructor() {
  }
}
