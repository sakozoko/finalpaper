import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {PublicNewsRepositoryService} from 'src/app/repositories/public-news-repository.service';
import {publicNew} from '../public-new.component';

export interface DialogData {
  publicNew: publicNew;
  onEdited: (publicNew: publicNew) => void;
}

@Component({
  selector: 'app-edit-public-new',
  templateUrl: './edit-public-new.component.html',
  styleUrls: ['./edit-public-new.component.css']
})
export class EditPublicNewComponent implements OnInit {
  form: FormGroup;

  constructor(private formBuilder: FormBuilder,
              private _publicNewsRepository: PublicNewsRepositoryService,
              public dialogRef: MatDialogRef<EditPublicNewComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData) {
  }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      title: this.data.publicNew.title,
      description: this.data.publicNew.description,
      image: this.data.publicNew.imageUrl,
      author: this.data.publicNew.author,
      date: this.data.publicNew.createdAt
    })
  }

  onSubmit() {
    this.data.publicNew.title = this.form.value.title;
    this.data.publicNew.description = this.form.value.description;
    this.data.publicNew.imageUrl = this.form.value.image;
    this.data.publicNew.author = this.form.value.author;
    this.data.publicNew.createdAt = new Date(this.form.value.date);
    this._publicNewsRepository.editPublicNew(this.data.publicNew).subscribe(result => {
      this.data.onEdited(result);
    });
  }
}
