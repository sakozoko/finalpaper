import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {DatePipe} from "@angular/common";
import {HelpRequestModel} from "../../models/help-request/help-request-model";
import {HelpRequestRepositoryService} from "../../services/repositories/help-request-repository.service";
import {PaginationService} from "../../components/pagination/pagination.service";

@Component({
  selector: 'app-help-requests',
  templateUrl: './admin-help-request.component.html',
  styleUrls: ['./admin-help-request.component.css']
})
export class AdminHelpRequestComponent implements OnInit {

  page = 1;
  pageSize = 7;
  helpRequests: HelpRequestModel[] = [];
  tempHelpRequests: HelpRequestModel[] = [];
  loading: boolean = true;
  status: string;
  search: string;

  constructor(private helpRequestRepository: HelpRequestRepositoryService,
              public paginationService: PaginationService,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private datePipe: DatePipe) {
  }

  searchChanged() {
    this.page = 1;
    this.router.navigate([], {queryParams: {search: this.search, page: this.page, status: this.status}});
    if (this.search == "") {
      this.helpRequests = this.tempHelpRequests.slice((this.page - 1) * this.pageSize, this.page * this.pageSize);
      this.setPaginationIfDataGotFromServer();
      return;
    }
    this.loading = true;

    let searchResult = this.tempHelpRequests.filter(x =>
      x.title.toLowerCase().includes(this.search.toLowerCase())
      || x.description.toLowerCase().includes(this.search.toLowerCase())
      || this.datePipe.transform(x.createdAt, 'dd.MM.yyyy hh:mm:ss')?.toLowerCase().includes(this.search.toLowerCase())
      || x.id.toLowerCase().includes(this.search.toLowerCase()));
    this.helpRequests = searchResult.slice((this.page - 1) * this.pageSize, this.page * this.pageSize);
    this.paginationService.setPagination(searchResult.length, this.pageSize, this.page);
    this.paginationService.currentPageChanged = (page) => {
      this.loading = true;
      this.page = page;
      this.helpRequests = searchResult
        .slice((this.page - 1) * this.pageSize, this.page * this.pageSize);
      this.loading = false;
    };
    this.loading = false;
  }

  statusChanged() {
    this.router.navigate([], {queryParams: {status: this.status, page: 1, search: this.search}});
    this.getHelpRequests();
  }

  onUpdated = (updatedHelpRequest: HelpRequestModel): void => {
    this.tempHelpRequests = this.tempHelpRequests.map(x => x.id == updatedHelpRequest.id ? updatedHelpRequest : x);
    this.helpRequests = this.tempHelpRequests.filter(x =>
      x.status.toLowerCase() == this.status.toLowerCase())
      .slice((this.page - 1) * this.pageSize, this.page * this.pageSize);
  }

  private setPageAndStatus() {
    this.page = this.activatedRoute.snapshot.queryParams['page'] || 1;
    this.status = this.activatedRoute.snapshot.queryParams['status'] || "new";
    this.search = this.activatedRoute.snapshot.queryParams['search'] || "";
  }

  private getHelpRequests() {
    this.loading = true;
    this.helpRequestRepository.getHelpRequests(this.status).subscribe(result => {
      this.tempHelpRequests = result;
      this.helpRequests = this.tempHelpRequests.slice((this.page - 1) * this.pageSize, this.page * this.pageSize);
      this.searchChanged();
    });
  }

  private setPaginationIfDataGotFromServer() {
    this.helpRequestRepository.getHelpRequestCount(this.status).subscribe(result => {
      this.paginationService.setPagination(result, this.pageSize, this.page);
      this.loading = false;
      this.paginationService.currentPageChanged = (page) => {
        this.loading = true;
        this.page = page;
        this.helpRequests = this.tempHelpRequests.slice((this.page - 1) * this.pageSize, this.page * this.pageSize);
        this.loading = false;
      }
    });
  }

  ngOnInit() {
    this.setPageAndStatus();
    this.getHelpRequests();
  }

}
