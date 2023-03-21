import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModalConfig, NgbModalModule, NgbModalRef, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NavComponent } from './nav/nav.component';
import { FooterComponent } from './footer/footer.component';
import { GetHelpComponent } from './get-help/get-help.component';
import { HomeComponent } from './home/home.component';
import { StatisticsComponent } from './home/statistics/statistics.component';
import { LastHelpRequestComponent } from './last-help-requests/last-help-request/last-help-request.component';
import { LastHelpRequestsComponent } from './last-help-requests/last-help-requests.component';
import { LastNewsComponent } from './last-news/last-news.component';
import { NezlamnistComponent } from './nezlamnist/nezlamnist.component';
import { PaginationComponent } from './pagination/pagination.component';
import { EditPublicNewComponent } from './public-news/public-new/edit-public-new/edit-public-new.component';
import { PublicNewComponent } from './public-news/public-new/public-new.component';
import { PublicNewsComponent } from './public-news/public-news.component';
import { SearchComponent } from './search/search.component';
import { AuthGuard } from './auth/auth.guard';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthConfigModule } from './auth/auth-config.module';
import { CreatePublicNewComponent } from './public-news/public-new/create-public-new/create-public-new.component';
import { NgxMatDatetimePickerModule, NgxMatNativeDateModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import {MatDialogModule} from '@angular/material/dialog';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    FooterComponent,
    HomeComponent,
    StatisticsComponent,
    PublicNewsComponent,
    PublicNewComponent,
    PaginationComponent,
    NezlamnistComponent,
    SearchComponent,
    LastNewsComponent,
    GetHelpComponent,
    LastHelpRequestsComponent,
    LastHelpRequestComponent,
    EditPublicNewComponent,
    CreatePublicNewComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    NgbCollapseModule,
    FormsModule,
    ReactiveFormsModule,
    AuthConfigModule,
    NgxMatDatetimePickerModule,
    NgxMatTimepickerModule,
    NgxMatNativeDateModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatDatepickerModule,
    MatInputModule,
    MatDialogModule
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
