import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
let AdminAuthorizationService = class AdminAuthorizationService {
    apiService;
    controller = 'adminauthorization';
    constructor(apiService) {
        this.apiService = apiService;
    }
    getAllPermissions() {
        return this.apiService.get(this.controller, 'getallpermissions');
    }
    getRolePermissions() {
        return this.apiService.get(this.controller, 'getrolepermissions');
    }
    getUserById(userId) {
        return this.apiService.get(this.controller, `getuserbyid?userId=${userId}`);
    }
    getAllUsers(request) {
        const params = new URLSearchParams();
        if (request.searchKey) {
            params.append('searchKey', request.searchKey);
        }
        params.append('paginationRequest.pageNumber', request.paginationRequest.pageNumber.toString());
        params.append('paginationRequest.pageSize', request.paginationRequest.pageSize.toString());
        if (request.paginationRequest.sortBy) {
            params.append('paginationRequest.sortBy', request.paginationRequest.sortBy);
        }
        params.append('paginationRequest.sortDescending', request.paginationRequest.sortDescending.toString());
        return this.apiService.get(this.controller, `getallusers?${params.toString()}`);
    }
    createUser(request) {
        return this.apiService.post(this.controller, 'createuser', request);
    }
    updateUser(request) {
        return this.apiService.put(this.controller, 'updateuser', request);
    }
    deleteRole(request) {
        return this.apiService.delete(this.controller, 'deleterole', request);
    }
    removeRoleFromUser(request) {
        return this.apiService.delete(this.controller, 'removerolefromuser', request);
    }
    assignRoleToUser(request) {
        return this.apiService.post(this.controller, 'assignroletouser', request);
    }
    assignPermissionsToRole(request) {
        return this.apiService.post(this.controller, 'assignpermissionstorole', request);
    }
};
AdminAuthorizationService = __decorate([
    Injectable({
        providedIn: 'root'
    })
], AdminAuthorizationService);
export { AdminAuthorizationService };
