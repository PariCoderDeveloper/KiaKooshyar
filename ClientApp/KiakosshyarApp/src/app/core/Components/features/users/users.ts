import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable, forkJoin } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { GetAllUsersRequest } from '../../../../shared/models/get-all-users-request.model';
import { AdminAuthorizationService } from '../../../services/admin.authorization.service';
import { ApiResponse } from '../../../../shared/models/api-response-model';
import { User, UserStatus } from '../../../../shared/models/user';
import { AdminManagmentService } from '../../../services/admin.managment.service';


@Component({
  imports: [CommonModule],
  selector: 'app-users',
  styleUrl: './users.css',
  templateUrl: './users.html',
})
export class Users implements OnInit {
  users: User[] = [];
  totalCount  = null;
  pageNumber = 1;
  pageSize = 20;
  
  constructor(
      private route : ActivatedRoute,
      private admin_authoriz:AdminAuthorizationService   ,
      private admin_managment:AdminManagmentService
    ) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
  this.route.queryParams.subscribe(params => {

    const request: GetAllUsersRequest = {
      searchKey: params['searchKey'] || '',

      paginationRequest: {
        pageNumber: Number(params['pageNumber']) || 1,
        pageSize: Number(params['pageSize']) || 20,
        sortBy: params['sortBy'] || undefined,
        sortDescending: params['sortDescending'] === 'true'
      }
    };
    (this.admin_authoriz.getAllUsers
      ( request ) as Observable<ApiResponse>).subscribe({

      next: (response) => {
        console.log('Users loaded successfully:', response.data.items);
        this.users = response.data.items || [];
        this.totalCount = response.data.totalCount;
        this.pageNumber = response.data.pageNumber;
        this.pageSize = response.data.pageSize;
      },

      error: (error) => {
        console.error('Error loading users:', error);
      }

    });

  });
}

changeStatus(user: User): void {
  let endpoint: string;

  switch (user.status) {
    case UserStatus.Active:
      endpoint = 'disable-user';
      break;

    case UserStatus.Inactive:
    case UserStatus.Block:
    case UserStatus.Suspended:
    case UserStatus.Locked:
      endpoint = 'enable-user';
      break;

    default:
      console.warn('Status cannot be changed:', user.status);
      return;
  }

  this.admin_authoriz.updateUser(
     user.id
  ).subscribe({
    next: () => {
      user.status =
        user.status === UserStatus.Active
          ? UserStatus.Inactive
          : UserStatus.Active;

      console.log(`User ${user.id} status changed successfully`);
    },

    error: (error) => {
      console.error('Error changing user status:', error);
    }
  });
}

changeRoles(user: User): void {
  console.log('Change roles for user:', user.id, user.userName);
}

changePermissions(user: User): void {
  console.log('Change permissions for user:', user.id, user.userName);
}

forceLogout(user: User): void {
  if (
    confirm(
      `Are you sure you want to force logout user ${user.userName}?`
    )
  ) {
    this.admin_managment
      .forceLogoutUser(
         user.id
      ).subscribe({
        next: () => {
          alert(
            `User ${user.userName} logged out successfully`
          );
        },

        error: (e) => {
          console.error(
            'Error forcing logout:',e
          );
        }
      });
  }
}

deleteUser(user: User): void {
  if (
    confirm(
      `Are you sure you want to delete user ${user.userName}?`
    )
  ) {
    this.admin_managment
      .deleteUser(
        user.id
      ).subscribe({
        next: () => {
          this.users = this.users.filter(
            u => u.id !== user.id
          );

          console.log(
            `User ${user.userName} deleted successfully`
          );
        },

        error: (error) => {
          console.error(
            'Error deleting user:',
            error
          );
        }
      });
  }
}

resetPassword(user: User): void {
  const newPassword = prompt('Enter new password:');

  if (!newPassword) {
    return;
  }

  this.admin_managment
    .resetUserPassword(
      user.id,
      newPassword
    )
    .subscribe({
      next: () => {
        console.log(
          `Password reset successfully for user ${user.userName}`
        );
      },

      error: (error) => {
        console.error(
          'Error resetting password:',
          error
        );
      }
    });
}
}