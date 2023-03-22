import {Component} from '@angular/core';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.css']
})
export class StatisticsComponent {
  public ticketsCount: number = 1000;
  public ticketsExecuted: number = 500;
  public volunteersCount: number = 100;
  public newsCount: number = 10;

}
