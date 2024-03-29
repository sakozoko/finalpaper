import {Overlay} from '@angular/cdk/overlay';
import {Component, Inject, OnInit, TemplateRef} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from '@angular/material/dialog';
import {HelpRequestModel} from "../../../../models/help-request/help-request-model";
import {HelpRequestRepositoryService} from "../../../../services/repositories/help-request-repository.service";

export interface DialogData {
  helpRequest: HelpRequestModel;
  onAnswered: () => void;
}

@Component({
  selector: 'app-answer',
  templateUrl: './answer.component.html',
  styleUrls: ['./answer.component.css']
})
export class AnswerComponent implements OnInit {
  form : FormGroup;
  private dialogModal : MatDialogRef<any>;
  constructor(public dialogRef: MatDialogRef<AnswerComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private helpRequestRepository : HelpRequestRepositoryService,
    private dialog  : MatDialog,
    private overlay : Overlay) { }

  onSubmit(templateRef: TemplateRef<any>){
    this.helpRequestRepository.answerHelpRequest(this.data.helpRequest.id, this.form.value.answer).subscribe(result => {
      this.data.helpRequest = result;
      this.dialogModal = this.dialog.open(templateRef,{
        autoFocus: false,
        hasBackdrop:false,
        disableClose:true,
        enterAnimationDuration: 150,
        exitAnimationDuration: 500,
        scrollStrategy: this.overlay.scrollStrategies.noop(),
        position:{top:'10px', right:'10px'}
      });
      this.dialogModal.afterOpened().subscribe(result => {
        setTimeout(() => {
          this.dialogModal.close();
        }, 2000);
      });
      this.data.onAnswered();
    });
    this.dialogRef.close();

  }
  ngOnInit(): void {
    this.form = new FormGroup({
      answer: new FormControl('', [Validators.required, Validators.maxLength(5000), Validators.minLength(50)])
    });
  }

}
