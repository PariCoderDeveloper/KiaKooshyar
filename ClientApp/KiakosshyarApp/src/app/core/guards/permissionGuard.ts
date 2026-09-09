import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AdminAuthorizationService } from '../services/admin.authorization.service';
import { catchError, map, of } from 'rxjs';

export const permissionGuard = (
  userId:number,
  permission: string
): CanActivateFn => {

  return () => {

    const adminAuthorization = inject(AdminAuthorizationService);
    const router = inject(Router);

       return adminAuthorization
      .hasPermission(userId, permission)
      .pipe(

        map(result => {

          if (result.success) {
            return true;
          }

          return router.createUrlTree([
            '/forbidden'
          ]);
        }),

        catchError(() => {

          return of(
            router.createUrlTree([
              '/forbidden'
            ])
          );

        })

      );

  };
};