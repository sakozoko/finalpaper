import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { HelpRequestModel, HelpRequestRepositoryService } from '../repositories/help-request-repository.service';
import { PaginationService } from '../services/pagination.service';

@Component({
  selector: 'app-last-help-requests',
  templateUrl: './last-help-requests.component.html',
  styleUrls: ['./last-help-requests.component.css']
})
export class LastHelpRequestsComponent implements OnInit {
  _page: number;
  @Input()
  pageSize = 5;
  @Input()
  pagination = true;
  @Input()
  resetForm : Observable<boolean> = new Observable<boolean>();
  loading = true;
  lastRequests: HelpRequestModel[] = [];

  constructor(private helpRequestRepository: HelpRequestRepositoryService,
     public paginationService : PaginationService,
     private activatedRoute: ActivatedRoute) {
      
  }
  ngOnInit(): void {
    this._page = this.activatedRoute.snapshot.queryParams['page'] ? Number.parseInt(this.activatedRoute.snapshot.queryParams['page']) : 1;
      this.clearLoading();
      this.resetForm.subscribe(data => {
        if(data){
          this.clearLoading();
        }
      }
    );
  }

  private clearLoading = () => {
    this.loading = true;
    this.lastRequests = [];
    this.helpRequestRepository.getHelpRequestCountForUser().subscribe(data => {
      this.paginationService.setPagination(data, this.pageSize, this._page);
      this.paginationService.currentPageChanged = (num) => {
        this._page = num;
        this.loading = true;
        this.lastRequests = [];
        this.helpRequestRepository.getHelpRequestForUserByPage(this._page, this.pageSize).subscribe(data => {
          this.lastRequests = data
          this.loading = false;
        });
      }
      this.helpRequestRepository.getHelpRequestForUserByPage(this._page, this.pageSize).subscribe(data => {
        this.lastRequests = data
        this.loading = false;
      });

    });
  }

  search=(filter: string) =>{
    this.loading = true;
    this.lastRequests = [];
    this.helpRequestRepository.getHelpRequestBySearchString(filter, this._page, this.pageSize).subscribe(data => {
      this.lastRequests = data;
      this.paginationService.setPagination(this.lastRequests.length, this.pageSize, this._page);
      this.loading = false;
    });
    this.paginationService.currentPageChanged = (num) => {
      this._page = num;
        this.loading = true;
        this.lastRequests = [];
        this.helpRequestRepository.getHelpRequestBySearchString(filter, this._page, this.pageSize).subscribe(data => {
          this.lastRequests = data
          this.loading = false;
        });
    }
    this.paginationService.goToPage(1);
  }


  clear=()=>{
    this.paginationService.goToPage(1);
    this.clearLoading();
  }
}
