import { Injectable } from '@angular/core';
import {Router} from "@angular/router";
import {pagination} from "../models/pagination";

@Injectable({
  providedIn: 'root'
})
export class PaginationService {

  private _pagination : pagination;
  public currentPageChanged = (currentPage:number): void => {
  };

  constructor(public router:Router) {
  }

  public setPagination(totalCount: number, pageSize: number, pageNumber: number){
    this._pagination = new pagination(pageNumber, pageSize, totalCount);
  }
  public getPagination(){
    return this._pagination;
  }
  public getPages() : number[]{
    let pages : number[] = [];
    for (let i = 1; i <= this._pagination.totalPages(); i++) {
      pages.push(i);
    }
    return pages;
  }

  public goToPage(pageNumber: number){
    this._pagination.pageNumber = pageNumber;
    let path = this.router.url.split('?')[0];
    this.router.navigate([path], { queryParams: { page: pageNumber } });
    this.currentPageChanged(pageNumber);
  }

  public nextPage(){
    if(this._pagination.hasNext())
      this.goToPage(this._pagination.pageNumber + 1);
  }
  public prevPage(){
    if(this._pagination.hasPrevious())
      this.goToPage(this._pagination.pageNumber - 1);
  }




}
