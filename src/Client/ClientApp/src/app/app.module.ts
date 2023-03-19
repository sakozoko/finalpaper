import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {AlertModule} from "ngx-bootstrap/alert";
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthConfigModule } from './auth/auth-config.module';
import {RouterModule, Routes} from "@angular/router";
import { NavComponent } from './nav/nav.component';
import { FooterComponent } from './footer/footer.component';
import { HomeComponent } from './home/home.component';
import { StatisticsComponent } from './home/statistics/statistics.component';
import { PublicNewsComponent } from './public-news/public-news.component';
import { PublicNewComponent } from './public-news/public-new/public-new.component';
import { PaginationComponent } from './pagination/pagination.component';
import { NezlamnistComponent } from './nezlamnist/nezlamnist.component';
import { SearchComponent } from './search/search.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LastNewsComponent } from './last-news/last-news.component';
import { GetHelpComponent } from './get-help/get-help.component';
import { SignInOidcComponent } from './auth/sign-in-oidc/sign-in-oidc.component';
import { SignInOidcCallbackComponent } from './auth/sign-in-oidc-callback/sign-in-oidc-callback.component';
import { AuthGuard } from './auth/auth.guard';
import { LastHelpRequestsComponent } from './last-help-requests/last-help-requests.component';
import { LastHelpRequestComponent } from './last-help-requests/last-help-request/last-help-request.component';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
const routes: Routes = [
  { path: '', component: HomeComponent },
  { path:'sign-in-oidc', component: SignInOidcComponent},
  { path: 'sign-in-oidc-callback', component: SignInOidcCallbackComponent},
  { path: 'public-news', component: PublicNewsComponent },
  { path:'public-new/:id', component: PublicNewComponent },
  { path:'nezlamnist', component: NezlamnistComponent},
  { path:'last-news', component: LastNewsComponent },
  { path:'get-help', component: GetHelpComponent, canActivate: [AuthGuard]},
  { path:'help-requests', component: LastHelpRequestsComponent, canActivate: [AuthGuard]},
];
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
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AlertModule.forRoot(),
    AuthConfigModule,
    RouterModule.forRoot(routes),
    FormsModule,
    ReactiveFormsModule,
    TooltipModule.forRoot(),
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
