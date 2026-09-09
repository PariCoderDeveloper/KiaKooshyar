import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiService } from './api.service';
import { ApiResponse } from '../../shared/models/api-response-model';
import { GetAllUsersRequest } from '../../shared/models/get-all-users-request.model';

@Injectable({
  providedIn: 'root'
})
export class AdminAuthorizationService {

  private readonly controller = 'adminauthorization';

  constructor(
    private readonly apiService: ApiService
  ) {}

  getAllPermissions(): Observable<ApiResponse> {
    return this.apiService.get(
      this.controller,
      'getallpermissions'
    ) as Observable<ApiResponse>;
  }

  getRolePermissions(): Observable<ApiResponse> {
    return this.apiService.get(
      this.controller,
      'getrolepermissions'
    ) as Observable<ApiResponse>;
  }

  getUserById(userId: number): Observable<ApiResponse> {
    return this.apiService.get(
      this.controller,
      `getuserbyid?userId=${userId}`
    ) as Observable<ApiResponse>;
  }

  getAllUsers(
    request: GetAllUsersRequest
  ): Observable<ApiResponse> {

    const params = new URLSearchParams();

    if (request.searchKey) {
      params.append('searchKey', request.searchKey);
    }

    params.append(
      'paginationRequest.pageNumber',
      request.paginationRequest.pageNumber.toString()
    );

    params.append(
      'paginationRequest.pageSize',
      request.paginationRequest.pageSize.toString()
    );

    if (request.paginationRequest.sortBy) {
      params.append(
        'paginationRequest.sortBy',
        request.paginationRequest.sortBy
      );
    }

    params.append(
      'paginationRequest.sortDescending',
      request.paginationRequest.sortDescending.toString()
    );

    return this.apiService.get(
      this.controller,
      `getallusers?${params.toString()}`
    ) as Observable<ApiResponse>;
  }

  createUser(
    request: any
  ): Observable<ApiResponse> {

    return this.apiService.post(
      this.controller,
      'createuser',
      request
    ) as Observable<ApiResponse>;
  }

  updateUser(
    request: any
  ): Observable<ApiResponse> {

    return this.apiService.put(
      this.controller,
      'updateuser',
      request
    ) as Observable<ApiResponse>;
  }

  deleteRole(
    request: any
  ): Observable<ApiResponse> {

    return this.apiService.delete(
      this.controller,
      'deleterole',
      request
    ) as Observable<ApiResponse>;
  }

  removeRoleFromUser(
    request: any
  ): Observable<ApiResponse> {

    return this.apiService.delete(
      this.controller,
      'removerolefromuser',
      request
    ) as Observable<ApiResponse>;
  }

  assignRoleToUser(
    request: any
  ): Observable<ApiResponse> {

    return this.apiService.post(
      this.controller,
      'assignroletouser',
      request
    ) as Observable<ApiResponse>;
  }

  assignPermissionsToRole(
    request: any
  ): Observable<ApiResponse> {

    return this.apiService.post(
      this.controller,
      'assignpermissionstorole',
      request
    ) as Observable<ApiResponse>;
  }

  hasPermission(
    userId: number,
    permission: string
  ): Observable<any> {

    const params: HasPermissionQuery = {
      userId,
      permission
    };

    return this.apiService.get<any>(
      'AdminAuthorization',
      'HasPermission',
      params
    );
  }
}

export interface HasPermissionQuery {
  userId: number;
  permission: string;
}

