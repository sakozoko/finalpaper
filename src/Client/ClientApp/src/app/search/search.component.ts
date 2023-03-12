import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {
public searchQuery : string ;
public lastSearchQuery : string;
public searched = false;
  constructor() { }
  @Input()
  public search = (searchQuery : string):void=>{

  }
  public privateSearch=():void=>{
    if(this.searchQuery == this.lastSearchQuery || !this.searchQuery)
      return;
    this.lastSearchQuery = this.searchQuery;
    this.search(this.searchQuery);
    this.searched = true;
  }

  public privateClear = ():void=> {
    this.searchQuery = "";
    this.searched = false;
    this.clear();
  }

  @Input()
  public clear = ():void=>{
    this.searchQuery = "";
  }
}
