import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import {CreatePublicNewComponent} from './public-new/create-public-new/create-public-new.component';
import {OAuthService} from 'angular-oauth2-oidc';
import {publicNewModel} from "../../models/public-new/public-new-model";
import {PaginationService} from "../../components/pagination/pagination.service";
import {PublicNewsRepositoryService} from "../../services/repositories/public-news-repository.service";

@Component({
  selector: 'app-public-news',
  templateUrl: './public-news.component.html',
  styleUrls: ['./public-news.component.css']
})
export class PublicNewsComponent implements OnInit {
  @Input() public homeViewed: boolean = false;
  public publicNews: publicNewModel[] = [];
  public modalRef: MatDialogRef<CreatePublicNewComponent>;
  private _page: number;
  private _pageSize: number = 5;
  loading : boolean = true;
  constructor(public activatedRoute: ActivatedRoute,
              public paginationService: PaginationService,
              public publicNewsRepository: PublicNewsRepositoryService,
              public dialog: MatDialog,
              public oauthService : OAuthService) {

  }

  openModal() {
    this.modalRef = this.dialog.open(CreatePublicNewComponent, {
      data: {
        onCreated: (publicNew: publicNewModel) => {
          this.publicNews.push(publicNew);
          this.publicNewsRepository.getPublicNewsCount().subscribe(count => {
            this.paginationService.setPagination(count, this._pageSize, this._page);
            this.paginationService.goToPage(this._page);
          });
        }
      },
      width: '100%',
      height: 'auto',
      autoFocus: false
    });
  }

  public search = (searchQuery: string): void => {

  }

  public clear = (): void => {

  }

  onDeleted = (id: string) => {
    this.publicNewsRepository.getPublicNewsCount().subscribe(count => {

      this.paginationService.setPagination(count, this._pageSize, this._page);
      this.publicNews = this.publicNews.filter(c => c.id != id);
      this.paginationService.goToPage(this._page);
    });
  }

  ngOnInit(): void {
    if (this.homeViewed) {
      this._page = 1;
      this._pageSize = 3;
    }
    this._page = this.activatedRoute.snapshot.queryParams['page'] ? Number.parseInt(this.activatedRoute.snapshot.queryParams['page']) : 1;

    this.publicNewsRepository.getPublicNews(this._page, this._pageSize).subscribe(result => {
      this.publicNews = result;
      this.publicNewsRepository.getPublicNewsCount().subscribe(count => {
        this.paginationService.setPagination(count, this._pageSize, this._page);
        this.loading = false;
        this.paginationService.currentPageChanged = (num) => {
          this.loading = true;
          this._page = num;
          this.publicNewsRepository.getPublicNews(num, this._pageSize).subscribe(result => {
            this.publicNews = result;
            this.loading = false;
          });
        }
      });
    });
  }

}


