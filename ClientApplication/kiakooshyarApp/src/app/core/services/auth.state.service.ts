import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthStateService {

  private roles: string[] = [];
  private permissions: string[] = [];

  setRoles(roles: string[]): void {
    this.roles = roles;
  }

  setPermissions(permissions: string[]): void {
    this.permissions = permissions;
  }

  getRoles(): string[] {
    return this.roles;
  }

  getPermissions(): string[] {
    return this.permissions;
  }

  hasRole(role: string): boolean {
    return this.roles.includes(role);
  }

  hasPermission(permission: string): boolean {
    return this.permissions.includes(permission);
  }

  clear(): void {
    this.roles = [];
    this.permissions = [];
  }
}