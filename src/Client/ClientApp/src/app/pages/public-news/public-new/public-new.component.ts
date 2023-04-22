import {Component, Input, OnInit, TemplateRef} from '@angular/core';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import {ActivatedRoute, Router} from "@angular/router";
import {OAuthService} from 'angular-oauth2-oidc';
import {EditPublicNewComponent} from './edit-public-new/edit-public-new.component';
import {publicNewModel} from "../../../models/public-new/public-new-model";
import {PublicNewsRepositoryService} from "../../../services/repositories/public-news-repository.service";

@Component({
  selector: 'app-public-new',
  templateUrl: './public-new.component.html',
  styleUrls: ['./public-new.component.css']
})
export class PublicNewComponent implements OnInit {
  @Input() public publicNew: publicNewModel = new publicNewModel();
  public truncated: boolean = true;
  @Input()
  public fullViewed: boolean = true;

  modalRef: MatDialogRef<any>;

  constructor(private _router: Router,
              private _publicNewsRepository: PublicNewsRepositoryService,
              private _activatedRoute: ActivatedRoute,
              public dialog: MatDialog,
              public oauthService : OAuthService) {

  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.dialog.open(template,
      {autoFocus: false});
  }

  ngOnInit(): void {
    if (!this._activatedRoute.snapshot.queryParams['id'] && this.fullViewed)
      this._router.navigate(['/public-news']);
    if (!this.publicNew.id)
      this._publicNewsRepository.getPublicNewById(this._activatedRoute.snapshot.queryParams['id']).subscribe(result => {
        if (!result)
          this._router.navigate(['/public-news']);
        this.publicNew = result
      });
  }

  @Input()
  onDeleted = (id: string) => {

  }

  @Input()
  onEdited = (publicNew: publicNewModel) => {
  }

  openEditModal() {
    this.modalRef = this.dialog.open(EditPublicNewComponent, {
      data: {
        publicNew: this.publicNew,
        onEdited: (publicNew: publicNewModel) => {
          this.publicNew = publicNew;
          this.onEdited(publicNew);
        },
      },
      width: '100%',
      height: 'auto',
      autoFocus: false
    });
  }

  deleteNew() {
    this._publicNewsRepository.deletePublicNewById(this.publicNew.id);
    this.onDeleted(this.publicNew.id);
  }

}
