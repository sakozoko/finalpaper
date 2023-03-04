import {Component, Input} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {PaginationService} from "../services/pagination.service";
import {publicNew} from "./public-new/public-new.component";

@Component({
  selector: 'app-public-news',
  templateUrl: './public-news.component.html',
  styleUrls: ['./public-news.component.css']
})
export class PublicNewsComponent  {
@Input() public homeViewed : boolean = false;
  public news : publicNew[] = [
    {
      title : "Допомога військовослужбовцям",
      description : "Допомога військовослужбовцям,Допомога військовослужбовцям,Допомога військовослужбовцям,Допомога військовослужбовцям,Допомога військовослужбовцям",
      date : new Date(2019, 10, 10,11,13,15),
      author : "Адміністратор",
      image : "https://images.unsplash.com/photo-1542208371-7b3a8a0b3c2f?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80",
      id : "1"
    },
    {
      title : "Допомога військовослужбовцям",
      description : "Допомога військовослужбовцям",
      date : new Date(2019, 10, 10),
      author : "Адміністратор",
      image : "https://images.unsplash.com/photo-1542208371-7b3a8a0b3c2f?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80",
      id : "2"
    },
    {
      title : "Допомога військовослужбовцям",
      description : "Допомога військовослужбовцям",
      date : new Date(2019, 10, 10),
      author : "Адміністратор",
      image : "https://images.unsplash.com/photo-1542208371-7b3a8a0b3c2f?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80",
      id : "3"
    },
    {
      title : "Допомога військовослужбовцям",
      description : "Допомога військовослужбовцям",
      date : new Date(2019, 10, 10),
      author : "Адміністратор",
      image : "https://images.unsplash.com/photo-1542208371-7b3a8a0b3c2f?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80",
      id : "4"
    }];
  private _page: number;
  private _pageSize: number=10;
  private _totalCount: number=40;


  constructor(public activatedRoute : ActivatedRoute, router: Router, public paginationService : PaginationService){
    this._page = activatedRoute.snapshot.queryParams['page']?Number.parseInt(activatedRoute.snapshot.queryParams['page']):1;
    this.paginationService.setPagination(this._totalCount, this._pageSize, this._page);
    paginationService.currentPageChanged = (num)=>{
      this._page = num;
    }
   }

}


