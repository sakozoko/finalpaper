import {Component, OnInit} from '@angular/core';
import {LastNewsRepositoryService} from "../repositories/last-news-repository.service";
import {ActivatedRoute} from "@angular/router";
import {PaginationService} from "../services/pagination.service";

@Component({
  selector: 'app-last-news',
  templateUrl: './last-news.component.html',
  styleUrls: ['./last-news.component.css']
})
export class LastNewsComponent implements OnInit {
  public lastNews: LastNew[] = [];
  public tLastNews: LastNew[] = [];
  public _page: number;
  public loading = true;
  private pageSize = 10;

  constructor(private lastNewsRepository: LastNewsRepositoryService,
              private activatedRoute: ActivatedRoute,
              public paginationService: PaginationService) {

  }

  ngOnInit(): void {
    this._page = this.activatedRoute.snapshot.queryParams['page'] ? Number.parseInt(this.activatedRoute.snapshot.queryParams['page']) : 1;
    this.clearLoading();
  }

  search = (filter: string) => {
    this.loading = true;
    this.lastNews = [];
    this.lastNewsRepository.getLatestNewsBySearchString(filter).subscribe(data => {
      this.tLastNews = data
      this.lastNews = this.tLastNews.slice(0, this.pageSize);
      this.paginationService.setPagination(this.tLastNews.length, this.pageSize, this._page);
      this.loading = false;
    });
    this.paginationService.currentPageChanged = (num) => {
      this._page = num;
      this.lastNews = this.tLastNews.slice((this._page - 1) * this.pageSize, this._page * this.pageSize);
    }
    this.paginationService.goToPage(1);
  }

  clear = () => {
    this.paginationService.goToPage(1);
    this.clearLoading();
  }

  private clearLoading = () => {
    this.loading = true;
    this.lastNews = [];
    this.lastNewsRepository.getLatestNewsCount().subscribe(data => {
      this.paginationService.setPagination(data, this.pageSize, this._page);
      this.paginationService.currentPageChanged = (num) => {
        this._page = num;
        this.loading = true;
        this.lastNews = [];
        this.lastNewsRepository.getLatestNews(this._page, this.pageSize).subscribe(data => {
          this.lastNews = data
          this.loading = false;
        });
      }
      this.lastNewsRepository.getLatestNews(this._page, this.pageSize).subscribe(data => {
        this.lastNews = data
        this.loading = false;
      });

    });
  }

}

export class LastNew {
  title: string;
  dateTime: string;
  link: string;
}

