import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {PaginationService} from "../services/pagination.service";
import {publicNew} from "./public-new/public-new.component";
import {PublicNewsRepositoryService} from "../repositories/public-news-repository.service";
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import {CreatePublicNewComponent} from './public-new/create-public-new/create-public-new.component';
import {AuthorizationService} from '../auth/authorization.service';

@Component({
  selector: 'app-public-news',
  templateUrl: './public-news.component.html',
  styleUrls: ['./public-news.component.css']
})
export class PublicNewsComponent implements OnInit {
  @Input() public homeViewed: boolean = false;
  public publicNews: publicNew[];
  public modalRef: MatDialogRef<CreatePublicNewComponent>;
  private _page: number;
  private _pageSize: number = 5;

  constructor(public activatedRoute: ActivatedRoute,
              public paginationService: PaginationService,
              public publicNewsRepository: PublicNewsRepositoryService,
              public dialog: MatDialog,
              public authorizationService: AuthorizationService) {

  }

  openModal() {
    this.modalRef = this.dialog.open(CreatePublicNewComponent, {
      data: {
        onCreated: (publicNew: publicNew) => {
          this.publicNews.push(publicNew);
          this.paginationService.setPagination(this.publicNewsRepository.getPublicNewsCount(), this._pageSize, this._page);
          this.paginationService.goToPage(this._page);
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
    this.paginationService.setPagination(this.publicNewsRepository.getPublicNewsCount(), this._pageSize, this._page);
    this.publicNews = this.publicNews.filter(c => c.id != id);
    this.paginationService.goToPage(this._page);
  }

  ngOnInit(): void {
    if (this.homeViewed) {
      this._page = 1;
      this._pageSize = 3;
    }
    this._page = this.activatedRoute.snapshot.queryParams['page'] ? Number.parseInt(this.activatedRoute.snapshot.queryParams['page']) : 1;
    this.publicNews = this.publicNewsRepository.getPublicNews(this._page, this._pageSize);
    console.log('updated')
    this.paginationService.setPagination(this.publicNewsRepository.getPublicNewsCount(), this._pageSize, this._page);
    this.paginationService.currentPageChanged = (num) => {
      this._page = num;
      this.publicNews = this.publicNewsRepository.getPublicNews(this._page, this._pageSize);
    }
  }

}


