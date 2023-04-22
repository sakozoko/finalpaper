import {Component, Input} from '@angular/core';
import {Clipboard} from '@angular/cdk/clipboard';
import {HelpRequestModel} from "../../../models/help-request/help-request-model";

@Component({
  selector: 'app-last-help-request',
  templateUrl: './last-help-request.component.html',
  styleUrls: ['./last-help-request.component.css'],

})
export class LastHelpRequestComponent {
  @Input()
  request: HelpRequestModel = new HelpRequestModel();
  truncated = true;

  constructor(private clipboard: Clipboard) {
  }

  copyId() {
    this.clipboard.copy(this.request.id);
  }

  getUkranianStatus() {
    switch (this.request.status) {
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
