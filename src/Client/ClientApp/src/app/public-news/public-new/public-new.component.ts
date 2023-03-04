import {Component, Input} from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-public-new',
  templateUrl: './public-new.component.html',
  styleUrls: ['./public-new.component.css']
})
export class PublicNewComponent {
  @Input() public publicNew: publicNew;
  public truncated:boolean=true;
  constructor(private _router : Router) {
  }
  showFullNew(){
    this._router.navigate(['/public-news', this.publicNew.id]);
  }

}
export class publicNew{
  public title : string;
  public description : string;
  public date : Date;
  public author : string;
  public image : string;
  public id : string
}
