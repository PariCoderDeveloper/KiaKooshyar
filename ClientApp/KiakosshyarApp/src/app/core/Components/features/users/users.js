import { __decorate } from "tslib";
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserStatus } from '../../../../shared/models/user';
let Users = class Users {
    route;
    admin_authoriz;
    admin_managment;
    users = [];
    totalCount = null;
    pageNumber = 1;
    pageSize = 20;
    constructor(route, admin_authoriz, admin_managment) {
        this.route = route;
        this.admin_authoriz = admin_authoriz;
        this.admin_managment = admin_managment;
    }
    ngOnInit() {
        this.loadUsers();
    }
    loadUsers() {
        this.route.queryParams.subscribe(params => {
            const request = {
                searchKey: params['searchKey'] || '',
                paginationRequest: {
                    pageNumber: Number(params['pageNumber']) || 1,
                    pageSize: Number(params['pageSize']) || 20,
                    sortBy: params['sortBy'] || undefined,
                    sortDescending: params['sortDescending'] === 'true'
                }
            };
            this.admin_authoriz.getAllUsers(request).subscribe({
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
    changeStatus(user) {
        let endpoint;
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
        this.admin_authoriz.updateUser(user.id).subscribe({
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
    changeRoles(user) {
        console.log('Change roles for user:', user.id, user.userName);
    }
    changePermissions(user) {
        console.log('Change permissions for user:', user.id, user.userName);
    }
    forceLogout(user) {
        if (confirm(`Are you sure you want to force logout user ${user.userName}?`)) {
            this.admin_managment
                .forceLogoutUser(user.id).subscribe({
                next: () => {
                    alert(`User ${user.userName} logged out successfully`);
                },
                error: (e) => {
                    console.error('Error forcing logout:', e);
                }
            });
        }
    }
    deleteUser(user) {
        if (confirm(`Are you sure you want to delete user ${user.userName}?`)) {
            this.admin_managment
                .deleteUser(user.id).subscribe({
                next: () => {
                    this.users = this.users.filter(u => u.id !== user.id);
                    console.log(`User ${user.userName} deleted successfully`);
                },
                error: (error) => {
                    console.error('Error deleting user:', error);
                }
            });
        }
    }
    resetPassword(user) {
        const newPassword = prompt('Enter new password:');
        if (!newPassword) {
            return;
        }
        this.admin_managment
            .resetUserPassword(user.id, newPassword)
            .subscribe({
            next: () => {
                console.log(`Password reset successfully for user ${user.userName}`);
            },
            error: (error) => {
                console.error('Error resetting password:', error);
            }
        });
    }
};
Users = __decorate([
    Component({
        imports: [CommonModule],
        selector: 'app-users',
        styleUrl: './users.css',
        templateUrl: './users.html',
    })
], Users);
export { Users };
