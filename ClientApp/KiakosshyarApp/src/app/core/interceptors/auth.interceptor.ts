import {
  HttpInterceptorFn,
  HttpHandlerFn,
  HttpRequest,
  HttpErrorResponse,
  HttpEvent
} from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import {
  catchError,
  switchMap,
  throwError,
  BehaviorSubject,
  filter,
  take,
  Observable
} from 'rxjs';
import { AuthService } from '../services/auth.service';
import { AuthStateService } from '../services/auth.state.service';

let isRefreshing = false;
const refreshTokenSubject = new BehaviorSubject<boolean | null>(null);

export const authInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
): Observable<HttpEvent<unknown>> => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const authState = inject(AuthStateService);

  if (req.url.includes('refresh-token')) {
    return next(req);
  }

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      console.log("🔴 Error caught:", error.status, error.url);

      if (error.status === 401) {
        return handle401Error(req, next, authService, router, authState);
      }

      if (error.status === 403) {
        alert("403 Forbidden: You don't have permission to access this resource.");
        authState.clear();
        router.navigate(['/login']);
      }

      return throwError(() => error);
    })
  );
};

function handle401Error(
  req: HttpRequest<unknown>,
  next: HttpHandlerFn,
  authService: AuthService,
  router: Router,
  authState: AuthStateService
): Observable<HttpEvent<unknown>> {

  if (!isRefreshing) {
    isRefreshing = true;
    refreshTokenSubject.next(false);

    return authService.refresh_token().pipe(
      switchMap(() => {
        isRefreshing = false;
        refreshTokenSubject.next(true);
        return next(req);
      }),
      catchError((err) => {
        isRefreshing = false;
        refreshTokenSubject.next(false);

        authState.clear();
        alert("Your session has expired. Please log in again.");
        router.navigate(['/login']);

        return throwError(() => err);
      })
    );
  } else {
    return refreshTokenSubject.pipe(
      filter((success) => success === true),
      take(1),
      switchMap(() => {
        return next(req);
      })
    );
  }
}