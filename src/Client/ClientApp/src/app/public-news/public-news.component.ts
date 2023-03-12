import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {PaginationService} from "../services/pagination.service";
import {publicNew} from "./public-new/public-new.component";
import {PublicNewsRepositoryService} from "../repositories/public-news-repository.service";

@Component({
  selector: 'app-public-news',
  templateUrl: './public-news.component.html',
  styleUrls: ['./public-news.component.css']
})
export class PublicNewsComponent implements OnInit{
@Input() public homeViewed : boolean = false;

  private _page: number;
  private _pageSize: number=5;
  public publicNews : publicNew[];

  constructor(public activatedRoute : ActivatedRoute,
              public paginationService : PaginationService,
              public publicNewsRepository : PublicNewsRepositoryService) {

   }

   public search = (searchQuery : string):void=>{

   }

   public clear = ():void=>{

   }

  ngOnInit(): void {
    if(this.homeViewed) {
      this._page = 1;
      this._pageSize = 3;
    }
    this._page = this.activatedRoute.snapshot.queryParams['page']?Number.parseInt(this.activatedRoute.snapshot.queryParams['page']):1;
    this.publicNews = this.publicNewsRepository.getPublicNews(this._page, this._pageSize);
    this.paginationService.setPagination(this.publicNewsRepository.getPublicNewsCount(), this._pageSize, this._page);
    this.paginationService.currentPageChanged = (num)=>{
      this._page = num;
      this.publicNews = this.publicNewsRepository.getPublicNews(this._page, this._pageSize);
    }
  }

}


