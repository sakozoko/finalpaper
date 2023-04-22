import {Component, Input, OnInit, TemplateRef} from '@angular/core';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import {AnswerComponent} from './answer/answer.component';
import {HelpRequestModel} from "../../../models/help-request/help-request-model";
import {HelpRequestRepositoryService} from "../../../services/repositories/help-request-repository.service";

@Component({
  selector: 'app-help-request',
  templateUrl: './help-request.component.html',
  styleUrls: ['./help-request.component.css']
})
export class HelpRequestComponent implements OnInit {

  @Input() public helpRequest: HelpRequestModel;
  modalRef: MatDialogRef<any>;
  @Input() public onDeleted: (updatedModel: HelpRequestModel) => void;

  ngOnInit() {
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.dialog.open(template,
      {autoFocus: false});
  }

  @Input() public onAnswered: (updatedModel: HelpRequestModel) => void;

  constructor(public dialog: MatDialog,
              private helpRequestRepository: HelpRequestRepositoryService) {
  }

  openAnswerModal() {
    this.modalRef = this.dialog.open(AnswerComponent, {
      autoFocus: false,
      width: '100%',
      height: 'auto',
      data: {
        helpRequest: this.helpRequest,
        onAnswered: this.onAnsweredUpdate,
      }
    });
  }

  deleteRequest() {
    this.helpRequestRepository.deleteHelpRequest(this.helpRequest.id).subscribe(result => {
      this.helpRequest = result;
      this.onDeleted(this.helpRequest);
      this.modalRef.close();
    });
  }

  getUkrainianStatus() {
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

  private onAnsweredUpdate: () => void = () => {
    this.helpRequest.status = "Processed";
    this.onAnswered(this.helpRequest);
  }
}
