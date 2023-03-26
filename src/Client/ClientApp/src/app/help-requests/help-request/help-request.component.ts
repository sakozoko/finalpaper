import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { HelpRequestModel, HelpRequestRepositoryService } from 'src/app/repositories/help-request-repository.service';
import { AnswerComponent } from './answer/answer.component';

@Component({
  selector: 'app-help-request',
  templateUrl: './help-request.component.html',
  styleUrls: ['./help-request.component.css']
})
export class HelpRequestComponent implements OnInit {

@Input() public helpRequest : HelpRequestModel;
modalRef: MatDialogRef<any>;
  constructor(public dialog: MatDialog,
    private helpRequestRepository : HelpRequestRepositoryService) { }

  ngOnInit() {
  }
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.dialog.open(template,
      {autoFocus: false});
  }
  openAnswerModal() {
    this.modalRef=this.dialog.open(AnswerComponent, {
      autoFocus: false,
      width: '100%',
      height: 'auto',
      data: {
        helpRequest: this.helpRequest,
        onAnswered: this.onAnsweredUpdate,
      }
    });
  }
  private onAnsweredUpdate : () => void = () => {
    this.helpRequest.status = "Processed";
    this.onAnswered();
  }

  deleteRequest(){
    this.helpRequestRepository.deleteHelpRequest(this.helpRequest.id).subscribe(result => {
      this.helpRequest = result;
      this.onDeleted();
      this.modalRef.close();
    });
  }
  @Input() public onDeleted: () => void;
  @Input() public onAnswered: () => void;
  
  getUkranianStatus() {
    switch (this.helpRequest.status) {
      case "New":
        return "Новий";
      case "Processed":
        return "Оброблєний";
      case "Closed":
        return "Відмінений";
      case "Removed":
        return "Видалений";
      default:
        return "Невідомий";
    }
  }
}
