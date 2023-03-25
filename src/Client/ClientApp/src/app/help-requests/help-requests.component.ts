import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HelpRequestModel, HelpRequestRepositoryService } from '../repositories/help-request-repository.service';
import { PaginationService } from '../services/pagination.service';

@Component({
  selector: 'app-help-requests',
  templateUrl: './help-requests.component.html',
  styleUrls: ['./help-requests.component.css']
})
export class HelpRequestsComponent implements OnInit {

  page = 1;
  pageSize = 10;
  helpRequests : HelpRequestModel[] = [];
  loading : boolean = true;
  constructor(private helpRequestRepository : HelpRequestRepositoryService,
    public paginationService : PaginationService,
    private activatedRoute : ActivatedRoute) { }

  ngOnInit() {
    this.page = this.activatedRoute.snapshot.queryParams['page'] || 1;
    this.helpRequestRepository.getHelpRequsts(this.page, this.pageSize).subscribe(result => {
      this.helpRequests = result;
      this.loading = false;
    });
    this.helpRequestRepository.getHelpRequestCount().subscribe(result => {
      this.paginationService.setPagination(result, this.pageSize, this.page);
      this.paginationService.currentPageChanged= (page) => {
        this.loading = true;
        this.page = page;
        this.helpRequestRepository.getHelpRequsts(this.page, this.pageSize).subscribe(result => {
          this.helpRequests = result;
          this.loading = false;
        });
      }
    });
    
  }

}
