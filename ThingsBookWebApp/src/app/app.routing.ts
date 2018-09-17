import { RouterModule, Routes } from '@angular/router';

import { HistoryPageComponent } from './history-page/history-page.component';
import { CategoriesPageComponent } from './categories-page/categories-page.component';
import { FriendsPageComponent } from './friends-page/friends-page.component';
import { ThingsPageComponent } from './things-page/things-page.component';
import { HomePageComponent } from './home-page/home-page.component';
import { AuthGuard } from './guards';
import { LoginComponent } from './login/login.component';
import { ErrorComponent } from './error/error.component';

const appRoutes: Routes = [
    { path: '', component: HomePageComponent },
    { path: 'things', component: ThingsPageComponent, canActivate: [AuthGuard] },
    { path: 'friends', component: FriendsPageComponent, canActivate: [AuthGuard] },
    { path: 'categories', component: CategoriesPageComponent, canActivate: [AuthGuard] },
    { path: 'history', component: HistoryPageComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'error', component: ErrorComponent },
    { path: '**', redirectTo: ''}
  ];

export const routing = RouterModule.forRoot(appRoutes);
