import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { UserListComponent } from './components/users/user-list/user-list.component';
import { BattleareaComponent } from './components/battlearea/battlearea.component';

export const appRoutes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'users', component: UserListComponent },
    { path: 'battlearea', component: BattleareaComponent },
];
