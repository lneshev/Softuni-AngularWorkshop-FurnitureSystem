import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { SigninComponent } from './authentication/signin/signin.component';
import { SignupComponent } from './authentication/signup/signup.component';
import { CreateEditFurnitureComponent } from './furniture/create-edit-furniture/create-edit-furniture.component';
import { AuthGuard } from './authentication/guards/auth.guard';
import { FurnitureAllComponent } from './furniture/furniture-all/furniture-all.component';
import { FurnitureDetailsComponent } from './furniture/furniture-details/furniture-details.component';
import { FurnitureUserComponent } from './furniture/furniture-user/furniture-user.component';
import { superAdminGuard } from './authentication/guards/super-admin.guard';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'home' },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'signin', component: SigninComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'furniture/all', component: FurnitureAllComponent, canActivate: [AuthGuard] },
  { path: 'furniture/user', component: FurnitureUserComponent, canActivate: [AuthGuard] },
  { path: 'furniture/create', component: CreateEditFurnitureComponent, canActivate: [AuthGuard] },
  { path: 'furniture/edit/:id', component: CreateEditFurnitureComponent, canActivate: [AuthGuard, superAdminGuard] },
  { path: 'furniture/details/:id', component: FurnitureDetailsComponent, canActivate: [AuthGuard] }
];
