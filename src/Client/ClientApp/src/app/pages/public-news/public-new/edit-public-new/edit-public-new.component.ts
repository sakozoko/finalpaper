import {Component, Inject, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {publicNewModel} from "../../../../models/public-new/public-new-model";
import {PublicNewsRepositoryService} from "../../../../services/repositories/public-news-repository.service";

export interface DialogData {
  publicNew: publicNewModel;
  onEdited: (publicNew: publicNewModel) => void;
}

@Component({
  selector: 'app-edit-public-new',
  templateUrl: './edit-public-new.component.html',
  styleUrls: ['./edit-public-new.component.css']
})
export class EditPublicNewComponent implements OnInit {
  form: FormGroup;
  minDate = new Date(2020, 0, 1);
  maxDate = new Date(2035, 0, 1);

  constructor(private _publicNewsRepository: PublicNewsRepositoryService,
              public dialogRef: MatDialogRef<EditPublicNewComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData) {
  }

  ngOnInit(): void {
    this.form = new FormGroup({
      title: new FormControl(this.data.publicNew.title, [Validators.required, Validators.maxLength(100), Validators.minLength(10)]),
      description: new FormControl(this.data.publicNew.description, [Validators.required, Validators.maxLength(5000), Validators.minLength(50)]),
      image: new FormControl(this.data.publicNew.imageUrl, [Validators.pattern("(http)?s?:?(\/\/[^\"']*\.(?:png|jpg|jpeg|gif|png|svg))")]),
      author : new FormControl(this.data.publicNew.author, [Validators.required, Validators.maxLength(30), Validators.minLength(4)]),
      date : new FormControl(this.data.publicNew.createdAt, [Validators.required, Validators.maxLength(30), Validators.minLength(4)])
    });
  }

  onSubmit() {
    this.data.publicNew.title = this.form.value.title;
    this.data.publicNew.description = this.form.value.description;
    this.data.publicNew.imageUrl = this.form.value.image;
    this.data.publicNew.author = this.form.value.author;
    this.data.publicNew.createdAt = new Date(this.form.value.date);
    let model = {
      id: this.data.publicNew.id,
      title: this.data.publicNew.title,
      description: this.data.publicNew.description,
      imageUrl: this.data.publicNew.imageUrl,
      author: this.data.publicNew.author,
      createdAt: this.data.publicNew.createdAt
    };
    this._publicNewsRepository.editPublicNew(model).subscribe(result => {
      this.data.onEdited(result);
    });
    this.dialogRef.close();
  }
}
