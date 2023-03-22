import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {publicNewCreateModel, PublicNewsRepositoryService} from 'src/app/repositories/public-news-repository.service';
import {publicNew} from '../public-new.component';

export interface DialogData {
  onCreated: (publicNew: publicNew) => void;
}

@Component({
  selector: 'app-create-public-new',
  templateUrl: './create-public-new.component.html',
  styleUrls: ['./create-public-new.component.css']
})
export class CreatePublicNewComponent implements OnInit {
  form: FormGroup;

  constructor(public modelRef: MatDialogRef<CreatePublicNewComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private formBuilder: FormBuilder,
              private publicNewRepository: PublicNewsRepositoryService) {
  }

  onSubmit() {
    let createModel = new publicNewCreateModel();
    createModel.title = this.form.value.title;
    createModel.description = this.form.value.description;
    createModel.image = this.form.value.image;
    let newPublicNew = this.publicNewRepository.createPublicNew(createModel);
    this.data.onCreated(newPublicNew);
    this.modelRef.close();
  }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      title: "",
      description: "",
      image: "",
    });
  }
}
