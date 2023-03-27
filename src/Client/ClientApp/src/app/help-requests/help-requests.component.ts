import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HelpRequestModel, HelpRequestRepositoryService } from '../repositories/help-request-repository.service';
import { PaginationService } from '../services/pagination.service';

@Component({
  selector: 'app-help-requests',
  templateUrl: './help-requests.component.html',
  styleUrls: ['./help-requests.component.css']
})
export class HelpRequestsComponent implements OnInit {

  page = 1;
  pageSize = 7;
  helpRequests : HelpRequestModel[] = [];
  tempHelpRequests : HelpRequestModel[] = [];
  loading : boolean = true;
  status : string;
  search : string;
  constructor(private helpRequestRepository : HelpRequestRepositoryService,
    public paginationService : PaginationService,
    private activatedRoute : ActivatedRoute,
    private router : Router) { }

    searchChanged(){
      this.router.navigate([], { queryParams: { search: this.search, page: 1, status: this.status } });
      if(this.search == ""){
        this.setPaginationIfDataGotFromServer();
        return;
      }
      this.loading = true;
      this.helpRequests = this.tempHelpRequests.filter(x => x.title.toLowerCase().includes(this.search.toLowerCase())
      || x.description.toLowerCase().includes(this.search.toLowerCase())
      || x.createdAt.toString().toLowerCase().includes(this.search.toLowerCase()));
      this.paginationService.setPagination(this.helpRequests.length, this.pageSize, this.page);
      this.paginationService.currentPageChanged= (page) => {
        this.loading = true;
        this.page = page;
        this.helpRequests = this.tempHelpRequests.filter(x => x.title
          .toLowerCase()
          .includes(this.search.toLowerCase()))
        .slice((this.page - 1) * this.pageSize, this.page * this.pageSize)
        this.loading = false;
      };
      this.loading = false;
    }
    statusChanged(){
      this.router.navigate([], { queryParams: { status: this.status, page: 1, search: this.search } });
      this.getHelpRequests();
    }
  private setPageAndStatus(){
      this.page = this.activatedRoute.snapshot.queryParams['page'] || 1;
      this.status = this.activatedRoute.snapshot.queryParams['status'] || "new";
      this.search = this.activatedRoute.snapshot.queryParams['search'] || "";
  }
  private getHelpRequests(){
    this.loading = true;
    this.helpRequestRepository.getHelpRequsts(this.page, this.pageSize, this.status).subscribe(result => {
      this.helpRequests = result;
      this.tempHelpRequests = result;
      this.loading = false;
      this.searchChanged();
    });
  }
  private setPaginationIfDataGotFromServer(){
    this.helpRequestRepository.getHelpRequestCount(this.status).subscribe(result => {
      this.paginationService.setPagination(result, this.pageSize, this.page);
      this.paginationService.currentPageChanged= (page) => {
        this.loading = true;
        this.page = page;
        this.helpRequestRepository.getHelpRequsts(this.page, this.pageSize, this.status).subscribe(result => {
          this.helpRequests = result;
          this.tempHelpRequests = result;
          this.loading = false;
        });
      }
    });
  }
  onUpdated = () : void => {
    this.helpRequests = this.tempHelpRequests.filter(x => x.status.toLowerCase() == this.status.toLowerCase());
  }

  ngOnInit() {
    this.setPageAndStatus();
    this.getHelpRequests();
  }

}
