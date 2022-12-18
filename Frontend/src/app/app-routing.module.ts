import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryComponent } from './Components/admin/category/category.component';
import { DashboardComponent } from './Components/admin/dashboard/dashboard.component';
import { PublisherComponent } from './Components/admin/publisher/publisher.component';
import { PublishernewComponent } from './Components/admin/publishernew/publishernew.component';
import { LoginComponent } from './Components/authentication/login/login.component';
import { RegisterComponent } from './Components/authentication/register/register.component';
import { ForbiddenComponent } from './Components/forbidden/forbidden.component';
import { HomeComponent } from './Components/Home/home.component';
import { AdminGuard } from './guards/admin.guard';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full',
  },
  {
    path: 'admin',
    component: DashboardComponent,
    // canActivate: [AdminGuard],
    canActivateChild: [AuthGuard, AdminGuard],
    children: [
      { path: 'category', component: CategoryComponent },
      { path: 'publisher', component: PublisherComponent },
      { path: 'publishernew', component: PublishernewComponent },
    ],
  },
  {
    path: 'auth/login',
    component: LoginComponent,
  },
  {
    path: 'auth/register',
    component: RegisterComponent,
  },
  {
    path: 'forbidden',
    component: ForbiddenComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
