import { Routes } from '@angular/router';
import { LoginComponent } from './core/Components/login/login';
import { RegisterComponent } from './core/Components/register/register';
import { AdminLayoutComponent } from './core/Components/layout/admin-layout/admin-layout';
import { Users } from './core/Components/features/users/users';
import { DashboardComponent } from './core/Components/features/dashboard/dashboard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path:'register',component:RegisterComponent},
  {
    path: 'admin',
    component: AdminLayoutComponent,
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component:DashboardComponent },
      { path: 'users', component:Users },
    ]
  },
  { path: '**', redirectTo: '/login' }
];
