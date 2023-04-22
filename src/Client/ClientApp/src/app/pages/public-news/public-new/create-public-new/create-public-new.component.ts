import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {publicNewModel} from "../../../../models/public-new/public-new-model";
import {publicNewCreateModel} from "../../../../models/public-new/public-new-create-model";
import {PublicNewsRepositoryService} from "../../../../services/repositories/public-news-repository.service";

export interface DialogData {
  onCreated: (publicNew: publicNewModel) => void;
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
    createModel.imageUrl = this.form.value.image;
    createModel.createdAt = new Date();
    this.publicNewRepository.createPublicNew(createModel).subscribe(result => {
      this.data.onCreated(result)
    });
    this.modelRef.close();
  }

  ngOnInit(): void {
    this.form = new FormGroup({
      title : new FormControl('', [Validators.required, Validators.maxLength(100), Validators.minLength(10)]),
      description : new FormControl('', [Validators.required, Validators.maxLength(5000), Validators.minLength(50)]),
      image : new FormControl('', [Validators.pattern("(http)?s?:?(\/\/[^\"']*\.(?:png|jpg|jpeg|gif|png|svg))")]),
    });
  }
}
