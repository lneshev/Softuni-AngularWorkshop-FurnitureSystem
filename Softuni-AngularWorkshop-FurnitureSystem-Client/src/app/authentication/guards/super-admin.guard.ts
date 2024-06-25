import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { inject } from '@angular/core';

export const superAdminGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const authService: AuthService = inject(AuthService);
  const user = authService.getUserFromToken();
  return (user && user.roles[0] === 'Super Admin') ? true : router.createUrlTree(['/']);
};
