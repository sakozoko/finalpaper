import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { HelpRequestModel } from 'src/app/repositories/help-request-repository.service';

@Component({
  selector: 'app-help-request',
  templateUrl: './help-request.component.html',
  styleUrls: ['./help-request.component.css']
})
export class HelpRequestComponent implements OnInit {

@Input() public helpRequest : HelpRequestModel;
modalRef: MatDialogRef<any>;
  constructor(public dialog: MatDialog) { }

  ngOnInit() {
  }
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.dialog.open(template,
      {autoFocus: false});
  }

  deleteRequest(){
    
  }

  
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
