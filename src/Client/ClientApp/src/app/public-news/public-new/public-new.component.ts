import {Component, Input} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {PublicNewsRepositoryService} from "../../repositories/public-news-repository.service";

@Component({
  selector: 'app-public-new',
  templateUrl: './public-new.component.html',
  styleUrls: ['./public-new.component.css']
})
export class PublicNewComponent {
  @Input() public publicNew: publicNew;
  public truncated:boolean=true;
  @Input()
  public fullViewed:boolean=true;
  constructor(private _router : Router,
              private _publicNewsRepository : PublicNewsRepositoryService,
              private _activatedRoute: ActivatedRoute) {
  if(!this.publicNew){
    this.publicNew = _publicNewsRepository.getPublicNewById(_activatedRoute.snapshot.params['id']) ?? new publicNew();
  }
  }
  showFullNew(){
    this._router.navigate(['/public-new', this.publicNew.id]);
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
