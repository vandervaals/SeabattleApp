import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { UserListComponent } from './components/users/user-list/user-list.component';
import { BattleareaComponent } from './components/battlearea/battlearea.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'users', component: UserListComponent },
            { path: 'battlearea', component: BattleareaComponent },
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];
