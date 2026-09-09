import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiService } from './api.service';
import { GetAllUsersRequest } from '../../shared/models/get-all-users-request.model';
import { User } from '../../shared/models/user';
import { ApiResponse } from '../../shared/models/api-response-model';
import { PagedResult } from '../../shared/models/paged-result';

@Injectable({
  providedIn: 'root'
})
export class AdminManagmentService {

  private readonly controller = 'admin';

  constructor(
    private readonly apiService: ApiService
  ) {}

  getAllUsers(
    request: GetAllUsersRequest
  ): Observable<ApiResponse<PagedResult<User>>> {

    const params = new URLSearchParams();

    if (request.searchKey) {
      params.set('searchKey', request.searchKey);
    }

    params.set(
      'paginationRequest.pageNumber',
      request.paginationRequest.pageNumber.toString()
    );

    params.set(
      'paginationRequest.pageSize',
      request.paginationRequest.pageSize.toString()
    );

    if (request.paginationRequest.sortBy) {
      params.set(
        'paginationRequest.sortBy',
        request.paginationRequest.sortBy
      );
    }

    params.set(
      'paginationRequest.sortDescending',
      request.paginationRequest.sortDescending.toString()
    );

    return this.apiService.get(
      this.controller,
      `get-all-users?${params.toString()}`
    ) as Observable<ApiResponse<PagedResult<User>>>;
  }


  getUserById(
    userId: number
  ): Observable<ApiResponse<User>> {

    return this.apiService.post(
      this.controller,
      'get-user-by-id',
      { userId }
    ) as Observable<ApiResponse<User>>;
  }


  createUser(
    request: any
  ): Observable<ApiResponse<any>> {

    return this.apiService.post(
      this.controller,
      'create-user',
      request
    ) as Observable<ApiResponse<any>>;
  }


  updateUser(
    request: any
  ): Observable<ApiResponse<any>> {

    return this.apiService.put(
      this.controller,
      'update-user',
      request
    ) as Observable<ApiResponse<any>>;
  }


  deleteUser(
    userId: number
  ): Observable<ApiResponse<any>> {

    return this.apiService.delete(
      this.controller,
      'delete-user',
      { userId }
    ) as Observable<ApiResponse<any>>;
  }

  enableUser(
    userId: number
  ): Observable<ApiResponse<any>> {

    return this.apiService.put(
      this.controller,
      'enable-user',
      { userId }
    ) as Observable<ApiResponse<any>>;
  }


  disableUser(
    userId: number
  ): Observable<ApiResponse<any>> {

    return this.apiService.put(
      this.controller,
      'disable-user',
      { userId }
    ) as Observable<ApiResponse<any>>;
  }


  unblockUser(
    userId: number
  ): Observable<ApiResponse<any>> {

    return this.apiService.put(
      this.controller,
      'unblock-user',
      { userId }
    ) as Observable<ApiResponse<any>>;
  }

  forceLogoutUser(
    Id: number
  ): Observable<ApiResponse<any>> {

    return this.apiService.post(
      this.controller,
      'force-logout',
      { Id }
    ) as Observable<ApiResponse<any>>;
  }

  resetUserPassword(
    userId: number,
    password: string
  ): Observable<ApiResponse<any>> {

    return this.apiService.post(
      this.controller,
      'reset-user-password',
      {
        userid: userId,
        password: password
      }
    ) as Observable<ApiResponse<any>>;
  }
}
