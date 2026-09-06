import { Routes } from '@angular/router';
import { AdminLayoutComponent } from './core/Components/layout/admin-layout/admin-layout';
import { Dashboard } from './core/Components/features/dashboard/dashboard';
import { Users } from './core/Components/features/users/users';
import { RegisterComponent } from './core/Components/register/register';
import { LoginComponent } from './core/Components/login/login';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path:'register',component:RegisterComponent},
  {
    path: 'admin',
    component: AdminLayoutComponent,
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component:Dashboard },
      { path: 'users', component:Users },
    ]
  },
  { path: '**', redirectTo: '/login' }
];
