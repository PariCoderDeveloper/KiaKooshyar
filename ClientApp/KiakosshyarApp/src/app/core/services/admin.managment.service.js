import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
let AdminManagmentService = class AdminManagmentService {
    apiService;
    controller = 'admin';
    constructor(apiService) {
        this.apiService = apiService;
    }
    getAllUsers(request) {
        const params = new URLSearchParams();
        if (request.searchKey) {
            params.set('searchKey', request.searchKey);
        }
        params.set('paginationRequest.pageNumber', request.paginationRequest.pageNumber.toString());
        params.set('paginationRequest.pageSize', request.paginationRequest.pageSize.toString());
        if (request.paginationRequest.sortBy) {
            params.set('paginationRequest.sortBy', request.paginationRequest.sortBy);
        }
        params.set('paginationRequest.sortDescending', request.paginationRequest.sortDescending.toString());
        return this.apiService.get(this.controller, `get-all-users?${params.toString()}`);
    }
    getUserById(userId) {
        return this.apiService.post(this.controller, 'get-user-by-id', { userId });
    }
    createUser(request) {
        return this.apiService.post(this.controller, 'create-user', request);
    }
    updateUser(request) {
        return this.apiService.put(this.controller, 'update-user', request);
    }
    deleteUser(userId) {
        return this.apiService.delete(this.controller, 'delete-user', { userId });
    }
    enableUser(userId) {
        return this.apiService.put(this.controller, 'enable-user', { userId });
    }
    disableUser(userId) {
        return this.apiService.put(this.controller, 'disable-user', { userId });
    }
    unblockUser(userId) {
        return this.apiService.put(this.controller, 'unblock-user', { userId });
    }
    forceLogoutUser(Id) {
        return this.apiService.post(this.controller, 'force-logout', { Id });
    }
    resetUserPassword(userId, password) {
        return this.apiService.post(this.controller, 'reset-user-password', {
            userid: userId,
            password: password
        });
    }
};
AdminManagmentService = __decorate([
    Injectable({
        providedIn: 'root'
    })
], AdminManagmentService);
export { AdminManagmentService };
