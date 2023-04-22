import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {NgbCollapseModule, NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {AuthGuard} from './auth/auth.guard';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {AuthConfigModule} from './auth/auth-config.module';
import {
  NgxMatDatetimePickerModule,
  NgxMatNativeDateModule,
  NgxMatTimepickerModule
} from '@angular-material-components/datetime-picker';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule} from '@angular/material/button';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatInputModule} from '@angular/material/input';
import {MatDialogModule} from '@angular/material/dialog';
import {AdminGuard} from './auth/admin.guard';
import {SignInOidcCallbackComponent} from './auth/sign-in-oidc-callback/sign-in-oidc-callback.component';
import {SignInOidcComponent} from './auth/sign-in-oidc/sign-in-oidc.component';
import {DatePipe} from "@angular/common";
import {MatSelectModule} from "@angular/material/select";
import {NgxMatSelectSearchModule} from "ngx-mat-select-search";
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import {FooterComponent} from "./components/footer/footer.component";
import {NavComponent} from "./components/nav/nav.component";
import {LastHelpRequestComponent} from "./pages/last-help-requests/last-help-request/last-help-request.component";
import {EditPublicNewComponent} from "./pages/public-news/public-new/edit-public-new/edit-public-new.component";
import {CreatePublicNewComponent} from "./pages/public-news/public-new/create-public-new/create-public-new.component";
import {HelpRequestComponent} from "./pages/admin-help-requests/help-request/help-request.component";
import {AnswerComponent} from "./pages/admin-help-requests/help-request/answer/answer.component";
import {GetHelpComponent} from "./pages/get-help/get-help.component";
import {LastNewsComponent} from "./pages/last-news/last-news.component";
import {NezlamnistComponent} from "./pages/nezlamnist/nezlamnist.component";
import {PublicNewComponent} from "./pages/public-news/public-new/public-new.component";
import {PublicNewsComponent} from "./pages/public-news/public-news.component";
import {LastHelpRequestsComponent} from "./pages/last-help-requests/last-help-requests.component";
import {HomeComponent} from "./pages/home/home.component";
import {AdminHelpRequestComponent} from "./pages/admin-help-requests/admin-help-request.component";
import {VolunteerOrganizationsComponent} from "./pages/volunteer-organizations/volunteer-organizations.component";
import {
  VolunteerOrganizationComponent
} from "./pages/volunteer-organizations/volunteer-organization/volunteer-organization.component";
import {SearchComponent} from "./components/search/search.component";
import {PaginationComponent} from "./components/pagination/pagination.component";

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    FooterComponent,
    PaginationComponent,
    SearchComponent,
    LastHelpRequestComponent,
    EditPublicNewComponent,
    CreatePublicNewComponent,
    HelpRequestComponent,
    AnswerComponent,
    SignInOidcComponent,
    SignInOidcCallbackComponent,
    GetHelpComponent,
    LastNewsComponent,
    NezlamnistComponent,
    PublicNewComponent,
    PublicNewsComponent,
    LastHelpRequestsComponent,
    HomeComponent,
    AdminHelpRequestComponent,
    VolunteerOrganizationsComponent,
    VolunteerOrganizationComponent
  ],
  imports: [
    BrowserModule,
    AuthConfigModule,
    NgbModule,
    NgbCollapseModule,
    FormsModule,
    ReactiveFormsModule,
    NgxMatDatetimePickerModule,
    NgxMatTimepickerModule,
    NgxMatNativeDateModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatDatepickerModule,
    MatInputModule,
    MatDialogModule,
    AppRoutingModule,
    MatSelectModule,
    NgxMatSelectSearchModule,
    MatProgressSpinnerModule
  ],
  providers: [AuthGuard, AdminGuard, DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule {
}
