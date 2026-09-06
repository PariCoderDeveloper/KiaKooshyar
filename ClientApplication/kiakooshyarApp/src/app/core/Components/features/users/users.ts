import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../../services/api.service';
import { Observable, forkJoin } from 'rxjs';

interface User {
  id: number;
  username: string;
  email: string;
  roles: string[];
  permissions: string[];
  isActive: boolean;
}

interface ApiResponse<T = any> {
  data?: T;
  [key: string]: any;

}
@Component({
  imports: [CommonModule],
  selector: 'app-users',
  styleUrl: './users.css',
  templateUrl: './users.html',
})
export class Users implements OnInit {
  users: User[] = [];

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    (this.apiService.get('admin', 'get-all-users') as 
      Observable<ApiResponse>).subscribe({
        next: (response) => {
          this.users = response.data || [];
        },
        error: (error: any) => {
          console.error('Error loading users:', error);
        }
    });
  }

  changeStatus(user: User): void {
    const endpoint = user.isActive ? 'disable-user' : 'enable-user';
    
    this.apiService.put('admin', endpoint, { userId: user.id }).subscribe({
      next: (response) => {
        user.isActive = !user.isActive;
        console.log(`User ${user.id} status changed successfully`);
      },
      error: (error) => {
        console.error('Error changing user status:', error);
      }
    });
  }

  changeRoles(user: User): void {
    // TODO: Implement role management dialog/modal
    console.log('Change roles for user:', user.id);
  }

  changePermissions(user: User): void {
    // TODO: Implement permission management dialog/modal
    console.log('Change permissions for user:', user.id);
  }

  forceLogout(user: User): void {
    if (confirm(`Are you sure you want to force logout user ${user.username}?`)) {
      this.apiService.post('admin', 'force-logout', { userId: user.id }).subscribe({
        next: (response) => {
          console.log(`User ${user.username} logged out successfully`);
        },
        error: (error) => {
          console.error('Error forcing logout:', error);
        }
      });
    }
  }

  deleteUser(user: User): void {
    if (confirm(`Are you sure you want to delete user ${user.username}?`)) {
      this.apiService.delete('admin', 'delete-user', { userId: user.id }).subscribe({
        next: (response) => {
          this.users = this.users.filter(u => u.id !== user.id);
          console.log(`User ${user.username} deleted successfully`);
        },
        error: (error) => {
          console.error('Error deleting user:', error);
        }
      });
    }
  }

  resetPassword(user: User): void {
    const newPassword = prompt('Enter new password:');
    if (newPassword) {
      this.apiService.post('admin', 'reset-user-password', { 
        userid: user.id, 
        password: newPassword 
      }).subscribe({
        next: (response) => {
          console.log(`Password reset successfully for user ${user.username}`);
        },
        error: (error) => {
          console.error('Error resetting password:', error);
        }
      });
    }
  }
}