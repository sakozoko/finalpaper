import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from "./pages/home/home.component";
import {SignInOidcComponent} from "./auth/sign-in-oidc/sign-in-oidc.component";
import {SignInOidcCallbackComponent} from "./auth/sign-in-oidc-callback/sign-in-oidc-callback.component";
import {PublicNewsComponent} from "./pages/public-news/public-news.component";
import {PublicNewComponent} from "./pages/public-news/public-new/public-new.component";
import {NezlamnistComponent} from "./pages/nezlamnist/nezlamnist.component";
import {LastNewsComponent} from "./pages/last-news/last-news.component";
import {GetHelpComponent} from "./pages/get-help/get-help.component";
import {AuthGuard} from "./auth/auth.guard";
import {LastHelpRequestsComponent} from "./pages/last-help-requests/last-help-requests.component";
import {AdminGuard} from "./auth/admin.guard";
import {VolunteerOrganizationsComponent} from "./pages/volunteer-organizations/volunteer-organizations.component";
import {AdminHelpRequestComponent} from "./pages/admin-help-requests/admin-help-request.component";


const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'sign-in-oidc', component: SignInOidcComponent},
  {path: 'sign-in-oidc-callback', component: SignInOidcCallbackComponent},
  {path: 'public-news', component: PublicNewsComponent},
  {path: 'public-new', component: PublicNewComponent},
  {path: 'nezlamnist', component: NezlamnistComponent},
  {path: 'last-news', component: LastNewsComponent},
  {path: 'get-help', component: GetHelpComponent, canActivate: [AuthGuard]},
  {path: 'help-requests', component: LastHelpRequestsComponent, canActivate: [AuthGuard]},
  {path: 'admin.help-requests', component: AdminHelpRequestComponent, canActivate: [AdminGuard]},
  {path: 'volunteer-organizations', component: VolunteerOrganizationsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
