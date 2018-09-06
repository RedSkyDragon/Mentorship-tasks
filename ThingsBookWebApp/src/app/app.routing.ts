import { RouterModule, Routes } from '@angular/router';

import { HistoryPageComponent } from './history-page/history-page.component';
import { CategoriesPageComponent } from './categories-page/categories-page.component';
import { FriendsPageComponent } from './friends-page/friends-page.component';
import { ThingsPageComponent } from './things-page/things-page.component';
import { HomePageComponent } from './home-page/home-page.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { AuthGuard } from './_guards';

const appRoutes: Routes = [
    { path: '', component: HomePageComponent },
    { path: 'things', component: ThingsPageComponent },
    { path: 'friends', component: FriendsPageComponent, canActivate: [AuthGuard] },
    { path: 'categories', component: CategoriesPageComponent },
    { path: 'history', component: HistoryPageComponent },
    { path: 'unauthorized', component: UnauthorizedComponent},
    { path: '**', redirectTo: ''}
  ];

export const routing = RouterModule.forRoot(appRoutes);
