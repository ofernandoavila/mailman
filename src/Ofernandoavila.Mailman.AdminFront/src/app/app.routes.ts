import { Routes } from '@angular/router';
import { authGuard } from '../guard/auth.guard';
import { LoginComponent } from './views/login/login.component';
import { UsersComponent } from './views/users/users.component';
import { LogsComponent } from './views/logs/logs.component';

export const routes: Routes = [
    { path: 'users', component: UsersComponent, canActivate: [ authGuard ] },
    { path: 'logs', component: LogsComponent, canActivate: [ authGuard ] },
    { path: 'auth/login', component: LoginComponent , canActivate: [] },
    { path: '', redirectTo: 'users', pathMatch: 'full' }
];
