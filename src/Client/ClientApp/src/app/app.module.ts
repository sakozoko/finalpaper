import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {NgbCollapseModule, NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {NavComponent} from './nav/nav.component';
import {FooterComponent} from './footer/footer.component';
import {StatisticsComponent} from './home/statistics/statistics.component';
import {LastHelpRequestComponent} from './last-help-requests/last-help-request/last-help-request.component';
import {PaginationComponent} from './pagination/pagination.component';
import {EditPublicNewComponent} from './public-news/public-new/edit-public-new/edit-public-new.component';
import {SearchComponent} from './search/search.component';
import {AuthGuard} from './auth/auth.guard';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {AuthConfigModule} from './auth/auth-config.module';
import {CreatePublicNewComponent} from './public-news/public-new/create-public-new/create-public-new.component';
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
import { HelpRequestComponent } from './help-requests/help-request/help-request.component';
import { AdminGuard } from './auth/admin.guard';
import { AnswerComponent } from './help-requests/help-request/answer/answer.component';
import { SignInOidcCallbackComponent } from './auth/sign-in-oidc-callback/sign-in-oidc-callback.component';
import { SignInOidcComponent } from './auth/sign-in-oidc/sign-in-oidc.component';
import { GetHelpComponent } from './get-help/get-help.component';
import { HelpRequestsComponent } from './help-requests/help-requests.component';
import { HomeComponent } from './home/home.component';
import { LastHelpRequestsComponent } from './last-help-requests/last-help-requests.component';
import { LastNewsComponent } from './last-news/last-news.component';
import { NezlamnistComponent } from './nezlamnist/nezlamnist.component';
import { PublicNewComponent } from './public-news/public-new/public-new.component';
import { PublicNewsComponent } from './public-news/public-news.component';
import { DefaultOAuthInterceptor } from 'angular-oauth2-oidc';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptorService } from './providers/token-interceptor.service';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    FooterComponent,
    StatisticsComponent,
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
    HelpRequestsComponent
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
    AppRoutingModule
  ],
  providers: [AuthGuard, AdminGuard],
  bootstrap: [AppComponent]
})
export class AppModule {
}
