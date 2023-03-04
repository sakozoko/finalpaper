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
const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'public-news',
    component: PublicNewsComponent,
  },
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
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AlertModule.forRoot(),
    AuthConfigModule,
    RouterModule.forRoot(routes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
