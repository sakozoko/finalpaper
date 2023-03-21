import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LastHelpRequestsComponent } from './last-help-requests/last-help-requests.component';
import { LastNewsComponent } from './last-news/last-news.component';
import { GetHelpComponent } from './get-help/get-help.component';
import { SignInOidcComponent } from './auth/sign-in-oidc/sign-in-oidc.component';
import { SignInOidcCallbackComponent } from './auth/sign-in-oidc-callback/sign-in-oidc-callback.component';
import { AuthGuard } from './auth/auth.guard';
import { PublicNewsComponent } from './public-news/public-news.component';
import { PublicNewComponent } from './public-news/public-new/public-new.component';
import { NezlamnistComponent } from './nezlamnist/nezlamnist.component';
import { HomeComponent } from './home/home.component';

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
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
