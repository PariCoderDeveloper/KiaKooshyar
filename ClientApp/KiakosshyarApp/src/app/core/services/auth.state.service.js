import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
let AuthStateService = class AuthStateService {
    roles = [];
    permissions = [];
    setRoles(roles) {
        this.roles = roles;
    }
    setPermissions(permissions) {
        this.permissions = permissions;
    }
    getRoles() {
        return this.roles;
    }
    getPermissions() {
        return this.permissions;
    }
    hasRole(role) {
        return this.roles.includes(role);
    }
    hasPermission(permission) {
        return this.permissions.includes(permission);
    }
    clear() {
        this.roles = [];
        this.permissions = [];
    }
};
AuthStateService = __decorate([
    Injectable({
        providedIn: 'root'
    })
], AuthStateService);
export { AuthStateService };
